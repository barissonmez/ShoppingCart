using FluentAssertions;
using ShoppingCart.Business.Domains;
using Xunit;

namespace ShoppingCart.Test
{
    public class LineItemTests
    {
        private const int Quantity = 1;

        [Fact]
        public void Should_create_a_new_instance_with_parameters()
        {
            var category = new Category("Electronic");
            var product = new Product("Notebook", 1000.0, category);

            var result = new LineItem(product, Quantity);

            result.Quantity.Should().Be(Quantity);
            result.Product.Title.Should().Be(product.Title);
            result.Product.UnitPrice.Should().Be(product.UnitPrice);
            result.Product.Category.Title.Should().Be(product.Category.Title);
            result.Product.Category.ParentCategory.Should().Be(product.Category.ParentCategory);

        }
    }
}
