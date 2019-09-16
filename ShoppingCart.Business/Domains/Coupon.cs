using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Domains
{
    public class Coupon
    {
        public double MinimumPurchaseAmount { get; }
        public double AmountOfDiscount { get; }
        public DiscountType DiscountType { get; }

        public Coupon(double minimumPurchaseAmount, double amountOfDiscount, DiscountType discountType)
        {
            MinimumPurchaseAmount = minimumPurchaseAmount;
            AmountOfDiscount = amountOfDiscount;
            DiscountType = discountType;
        }
    }
}
