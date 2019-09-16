using FluentAssertions;
using ShoppingCart.Business.Domains;
using ShoppingCart.Business.Enum;
using Xunit;

namespace ShoppingCart.Test
{
    public class CampaignTests
    {
        private const double AmountOfDiscount = 10.0;
        private const int MinimumItemQuantity = 1;

        [Fact]
        public void Should_create_a_new_instance_with_parameters()
        {
            var category = new Category("Electronic");

            var result = new Campaign(category, AmountOfDiscount, MinimumItemQuantity, DiscountType.Amount);

            result.AmountOfDiscount.Should().Be(AmountOfDiscount);
            result.MinimumItemQuantity.Should().Be(MinimumItemQuantity);
            result.DiscountType.Should().Be(DiscountType.Amount);
            result.Category.Title.Should().Be(category.Title);
            result.Category.ParentCategory.Should().Be(category.ParentCategory);
        }
    }
}
