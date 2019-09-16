using ShoppingCart.Business.Enum;

namespace ShoppingCart.Business.Domains
{
    public class Campaign
    {
        public Category Category { get; }  
        public double AmountOfDiscount { get; }  
        public int MinimumItemQuantity { get; }  
        public DiscountType DiscountType { get; }

        public Campaign(Category category, double amountOfDiscount, int minimumItemQuantity, DiscountType discountType)
        {
            Category = category;
            AmountOfDiscount = amountOfDiscount;
            MinimumItemQuantity = minimumItemQuantity;
            DiscountType = discountType;
        }
    }
}
