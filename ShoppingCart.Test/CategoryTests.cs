using FluentAssertions;
using ShoppingCart.Business.Domains;
using Xunit;

namespace ShoppingCart.Test
{
    public class CategoryTests
    {

        [Fact]
        public void Should_create_a_new_instance_with_parameters()
        {
            var categoryTitle = "Electronic";

            var result = new Category(categoryTitle);

            result.Title.Should().Be(categoryTitle);
            result.ParentCategory.Should().BeNull();

        }

        [Fact]
        public void Should_create_a_new_instance_with_a_parent()
        {
            var parentCategoryTitle = "Electronic";
            var categoryTitle = "SmartPhone";

            var result = new Category(categoryTitle, new Category(parentCategoryTitle));

            result.Title.Should().Be(categoryTitle);
            result.ParentCategory.Should().NotBeNull();
            result.ParentCategory.Title.Should().Be(parentCategoryTitle);

        }
    }
}
