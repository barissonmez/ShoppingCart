namespace ShoppingCart.Business.Calculators.Discount
{
    public interface IDiscountCalculator<in T> : ICalculator
    {
        double CalculateFor(T item, double totalAmount);
    }
}
