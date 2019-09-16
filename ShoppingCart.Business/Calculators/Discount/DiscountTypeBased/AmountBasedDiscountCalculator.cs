using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Calculators.Discount.DiscountTypeBased
{
    public class AmountBasedDiscountCalculator : IDiscountTypeBasedCalculator
    {
        public DiscountType DiscountType => DiscountType.Amount;

        public double AmountOfDiscount { get; }

        public AmountBasedDiscountCalculator(double amountOfDiscount)
        {
            AmountOfDiscount = amountOfDiscount;
        }

        public double Calculate()
        {
            return AmountOfDiscount;
        }
    }
}
