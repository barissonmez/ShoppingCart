namespace ShoppingCart.Business.Domains
{
    public class LineItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
