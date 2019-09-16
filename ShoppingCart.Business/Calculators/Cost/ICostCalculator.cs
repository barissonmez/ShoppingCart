namespace ShoppingCart.Business.Calculators.Cost
{
    public interface ICostCalculator<in T> : ICalculator
    {
        double CalculateFor(T item);
    }
}
