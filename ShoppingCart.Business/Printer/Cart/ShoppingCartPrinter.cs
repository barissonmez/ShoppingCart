using System.Linq;
using ConsoleTables;

namespace ShoppingCart.Business.Printer.Cart
{
    public class ShoppingCartPrinter : IConsolePrinter<Domains.ShoppingCart>
    {
        public void Print(Domains.ShoppingCart cart)
        {
            var table = new ConsoleTable("CategoryName", "ProductName", "Quantity", "UnitPrice");
            
            var categories = cart.LineItems.GroupBy(a => a.Product.Category);

            foreach (var category in categories)
            {
                foreach (var lineItem in category.Select(a=> a).ToList())
                {
                    table
                        .AddRow(category.Key.Title, lineItem.Product.Title, lineItem.Quantity, lineItem.Product.UnitPrice);
                }
            }

            table
                .AddRow(string.Empty, string.Empty, string.Empty,string.Empty )
                .AddRow(string.Empty, string.Empty, "TotalPrice:", cart.LineItems.Sum(a => a.Product.UnitPrice * a.Quantity))
                .AddRow(string.Empty, string.Empty, $"TotalDiscount[campaign({cart.GetCampaignDiscount()}), coupon({cart.GetCouponDiscount()})]:", cart.GetCampaignDiscount() + cart.GetCouponDiscount())
                .AddRow(string.Empty, string.Empty, "TotalAmount:", cart.GetTotalAmountAfterDiscounts())
                .AddRow(string.Empty, string.Empty, "DeliveryCost:", cart.GetDeliveryCost());

            table.Write();


        }
    }
}
