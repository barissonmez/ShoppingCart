using ShoppingCart.Business.Calculators.Cost;
using ShoppingCart.Business.Calculators.Discount;
using ShoppingCart.Business.Printer;
using ShoppingCart.Business.Printer.Cart;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Business.Domains
{
    public class ShoppingCart
    {
        public const double CostPerDelivery = 1.99;
        public const double CostPerProduct = 0.99;
        public const double FixedCost = 2.99;

        private readonly ICostCalculator<ShoppingCart> _deliveryCostCalculator = new DeliveryCostCalculator(CostPerDelivery, CostPerProduct, FixedCost);
        private readonly IDiscountCalculator<Campaign> _campaignCalculator = new CampaignCalculator();
        private readonly IDiscountCalculator<Coupon> _couponCalculator = new CouponCalculator();
        private readonly IConsolePrinter<ShoppingCart> _printer = new ShoppingCartPrinter();

        public List<LineItem> LineItems { get; }
        public List<Campaign> AppliedCampaigns { get; }
        public Coupon AppliedCoupon { get; private set; }

        public ShoppingCart()
        {
            LineItems = new List<LineItem>();
            AppliedCampaigns = new List<Campaign>();
        }

        public void AddItem(Product product, int quantity)
        {
            LineItems.Add(new LineItem(product, quantity));
        }

        public void ApplyCampaigns(params Campaign[] campaigns)
        {
            AppliedCampaigns.AddRange(campaigns);
        }

        public void ApplyCoupon(Coupon coupon)
        {
            AppliedCoupon = coupon;
        }

        public double GetCouponDiscount()
        {
            if (AppliedCoupon == null) return default(double);

            var cartTotal = LineItems.Sum(a => a.Product.UnitPrice * a.Quantity) - GetCampaignDiscount();

            return _couponCalculator.CalculateFor(AppliedCoupon, cartTotal);

        }

        public double GetCampaignDiscount()
        {
            if (AppliedCampaigns.Any())
            {
                foreach (var appliedCampaign in AppliedCampaigns.OrderByDescending(a => a.AmountOfDiscount))
                {
                    var applicableLineItems = LineItems.Where(a => a.Product.Category.Equals(appliedCampaign.Category)).ToList();

                    if (applicableLineItems.Count <= appliedCampaign.MinimumItemQuantity) continue;

                    var applicableLineItemsCartTotal = applicableLineItems.Sum(a => a.Product.UnitPrice * a.Quantity);

                    return _campaignCalculator.CalculateFor(appliedCampaign, applicableLineItemsCartTotal);
                }
            }

            return default(double);
        }

        public double GetDeliveryCost()
        {
            return _deliveryCostCalculator.CalculateFor(this);
        }

        public double GetTotalAmountAfterDiscounts()
        {
            var cartTotal = LineItems.Sum(a => a.Product.UnitPrice * a.Quantity);

            var discountTotal = GetCampaignDiscount() + GetCouponDiscount();

            return cartTotal - discountTotal;
        }

        public void Print()
        {
            _printer.Print(this);
        }
    }
}
