namespace ShoppingCart.Business.Domains
{
    public class Product
    {
        public string Title { get; }
        public double UnitPrice { get; }
        public Category Category { get; }

        public Product(string title, double unitPrice, Category category)
        {
            Title = title;
            UnitPrice = unitPrice;
            Category = category;
        }
    }
}