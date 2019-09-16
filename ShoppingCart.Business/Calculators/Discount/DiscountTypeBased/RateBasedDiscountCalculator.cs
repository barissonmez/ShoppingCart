using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Calculators.Discount.DiscountTypeBased
{
    public class RateBasedDiscountCalculator : IDiscountTypeBasedCalculator
    {
        public DiscountType DiscountType => DiscountType.Rate;
        public double TotalPurchaseAmount { get; }
        public double AmountOfDiscount { get; }

        public RateBasedDiscountCalculator(double totalPurchaseAmount, double amountOfDiscount)
        {
            TotalPurchaseAmount = totalPurchaseAmount;
            AmountOfDiscount = amountOfDiscount;
        }

        public double Calculate()
        {
            return TotalPurchaseAmount * AmountOfDiscount / 100.0;
        }
    }
}
