using System.Linq;

namespace ShoppingCart.Business.Calculators.Cost
{
    public class DeliveryCostCalculator : ICostCalculator<Business.Domains.ShoppingCart>
    {
        public double CostPerDelivery { get; }
        public double CostPerProduct { get; }
        public double FixedCost { get; }

        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }

        public double CalculateFor(Business.Domains.ShoppingCart cart)
        {
            var numberOfDeliveries = cart.LineItems.Select(i => i.Product).Select(a => a.Category).GroupBy(a => a.Title).Count();

            var numberOfProducts = cart.LineItems.Select(i => i.Product).GroupBy(a => a.Title).Count();

            return CostPerDelivery * numberOfDeliveries + CostPerProduct * numberOfProducts + FixedCost;
        }

        
    }
}
