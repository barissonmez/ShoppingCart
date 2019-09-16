using FluentAssertions;
using ShoppingCart.Business.Domains;
using ShoppingCart.Business.Enum;
using Xunit;

namespace ShoppingCart.Test
{
    public class CouponTests
    {
        private const double MinimumPurchaseAmount = 10.0;
        private const double AmountOfDiscount = 2.0;

        [Fact]
        public void Should_create_a_new_instance_with_parameters()
        {
            var result = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Amount);

            result.MinimumPurchaseAmount.Should().Be(MinimumPurchaseAmount);
            result.AmountOfDiscount.Should().Be(AmountOfDiscount);
            result.DiscountType.Should().Be(DiscountType.Amount);
        }
    }
}
