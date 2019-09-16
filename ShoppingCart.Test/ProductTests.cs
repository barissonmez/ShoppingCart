using FluentAssertions;
using ShoppingCart.Business.Domains;
using Xunit;

namespace ShoppingCart.Test
{
    public class ProductTests
    {
        private const string ProductTitle = "Notebook";
        private const double UnitPrice = 1000.0;

        [Fact]
        public void Should_create_a_new_instance_with_parameters()
        {
            var category = new Category("Electronic");
            var result = new Product(ProductTitle, UnitPrice, category);

            result.Title.Should().Be(ProductTitle);
            result.UnitPrice.Should().Be(UnitPrice);
            result.Category.Title.Should().Be(category.Title);
            result.Category.ParentCategory.Should().Be(category.ParentCategory);

        }
    }
}
