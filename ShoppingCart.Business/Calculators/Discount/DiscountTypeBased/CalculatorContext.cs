using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Calculators.Discount.DiscountTypeBased
{
    public class CalculatorContext
    {
        private readonly IList<IDiscountTypeBasedCalculator> _discountCalculators;
        public DiscountType DiscountType { get; }
        public double TotalPurchaseAmount { get; }
        public double AmountOfDiscount { get; }

        public CalculatorContext(DiscountType discountType, double amountOfDiscount, double totalPurchaseAmount)
        {
            DiscountType = discountType;
            TotalPurchaseAmount = totalPurchaseAmount;
            AmountOfDiscount = amountOfDiscount;

            _discountCalculators = new List<IDiscountTypeBasedCalculator>
            {
                new AmountBasedDiscountCalculator(AmountOfDiscount),
                new RateBasedDiscountCalculator(TotalPurchaseAmount, AmountOfDiscount)
            };
        }

        public double Calculate()
        {
            var calculator = _discountCalculators.FirstOrDefault(a => a.DiscountType.Equals(DiscountType));

            return calculator.Calculate();
        }
    }
}
