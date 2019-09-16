namespace ShoppingCart.Business.Domains
{
    public class Category
    {
        public string Title { get; }
        public Category ParentCategory { get; }

        public Category(string title, Category parentCategory = null)
        {
            Title = title;
            ParentCategory = parentCategory;
        }
    }
}