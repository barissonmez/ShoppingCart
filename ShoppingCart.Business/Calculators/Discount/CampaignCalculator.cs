using ShoppingCart.Business.Calculators.Discount.DiscountTypeBased;
using ShoppingCart.Business.Domains;

namespace ShoppingCart.Business.Calculators.Discount
{
    public class CampaignCalculator : IDiscountCalculator<Campaign>
    {
        public double CalculateFor(Campaign campaign, double totalAmount)
        {
            var discountCalculatorContext = new CalculatorContext(campaign.DiscountType, campaign.AmountOfDiscount, totalAmount);

            return discountCalculatorContext.Calculate();
        }
    }
}
