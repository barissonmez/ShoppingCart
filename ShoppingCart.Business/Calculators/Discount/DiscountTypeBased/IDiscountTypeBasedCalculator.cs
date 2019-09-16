using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Calculators.Discount.DiscountTypeBased
{
    public interface IDiscountTypeBasedCalculator
    {
        DiscountType DiscountType { get; }

        double Calculate();
    }
}
