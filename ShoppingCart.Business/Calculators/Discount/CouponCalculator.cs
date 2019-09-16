using ShoppingCart.Business.Calculators.Discount.DiscountTypeBased;
using ShoppingCart.Business.Domains;

namespace ShoppingCart.Business.Calculators.Discount
{
    public class CouponCalculator : IDiscountCalculator<Coupon>
    {
        public double CalculateFor(Coupon coupon, double totalAmount)
        {
            if (totalAmount < coupon.MinimumPurchaseAmount) return default(double);

            var discountCalculatorContext = new CalculatorContext(coupon.DiscountType, coupon.AmountOfDiscount, totalAmount);

            return discountCalculatorContext.Calculate();
        }
    }
}
