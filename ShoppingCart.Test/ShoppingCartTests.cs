using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ShoppingCart.Business.Domains;
using ShoppingCart.Business.Enum;
using Xunit;

namespace ShoppingCart.Test
{
    public class ShoppingCartTests
    {
        private const int Quantity = 1;
        private const int LineItemCount = 3;
        private const int AppliedCampaignCount = 4;
        private const double AmountOfDiscount = 10.0;
        private const int MinimumItemQuantity = 1;
        private const double MinimumPurchaseAmount = 1000.0;

        [Fact]
        public void Should_print_details_for_cart()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 1000.0, productCategory);

            var coupon = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Amount);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);
            sut.ApplyCoupon(coupon);

            sut.Print();

            var result = sut;

            result.Should().NotBeOfType<Exception>();
        }

        [Fact]
        public void Should_return_total_amount_after_discounts_cost_for_cart()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 1000.0, productCategory);

            var coupon = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Amount);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);
            sut.ApplyCoupon(coupon);

            var result = sut.GetTotalAmountAfterDiscounts();

            result.Should().NotBe(default(double));
        }

        [Fact]
        public void Should_return_delivery_cost_for_line_items()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 1000.0, productCategory);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);

            var result = sut.GetDeliveryCost();

            result.Should().NotBe(default(double));
        }


        [Fact]
        public void Should_return_discount_if_there_is_an_applicable_coupon()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 1000.0, productCategory);

            var coupon = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Amount);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);
            sut.ApplyCoupon(coupon);

            var result = sut.GetCouponDiscount();

            result.Should().NotBe(default(double));
            result.Should().Be(AmountOfDiscount);
        }

        [Fact]
        public void Should_return_zero_discount_if_there_is_no_applicable_coupon()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 100.0, productCategory);

            var coupon = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Amount);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);
            sut.ApplyCoupon(coupon);

            var result = sut.GetCouponDiscount();

            result.Should().Be(default(double));

        }

        [Fact]
        public void Should_return_zero_discount_if_there_is_no_coupon()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 100.0, productCategory);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);

            var result = sut.GetCouponDiscount();

            result.Should().Be(default(double));

        }


        [Fact]
        public void Should_return_discount_if_there_is_an_applicable_campaign()
        {
            var category = new Category("Electronic");
            var notebook = new Product("Notebook", 2000.0, category);
            var smartPhone = new Product("SmartPhone", 1000.0, category);

            var campaign = new Campaign(category, AmountOfDiscount, MinimumItemQuantity, DiscountType.Rate);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(notebook, Quantity);
            sut.AddItem(smartPhone, Quantity);
            sut.ApplyCampaigns(campaign);

            var result = sut.GetCampaignDiscount();

            var total = sut.LineItems.Where(a => a.Product.Category.Title.Equals(category.Title)).Sum(a => a.Quantity * a.Product.UnitPrice);

            result.Should().NotBe(default(double));
            result.Should().Be(total * AmountOfDiscount / 100);

        }

        [Fact]
        public void Should_return_zero_discount_if_there_is_no_applicable_campaign()
        {
            var productCategory = new Category("Electronic");
            var product = new Product("Notebook", 1000.0 , productCategory);

            var campaignCategory = new Category("Food");
            var campaign = new Campaign(campaignCategory, AmountOfDiscount, MinimumItemQuantity, DiscountType.Amount);

            var sut = new Business.Domains.ShoppingCart();
            sut.AddItem(product, Quantity);
            sut.ApplyCampaigns(campaign);

            var result = sut.GetCampaignDiscount();

            result.Should().Be(default(double));

        }

        [Fact]
        public void Should_return_zero_discount_if_there_is_no_applied_campaign()
        {
            var sut = new Business.Domains.ShoppingCart();

            var result = sut.GetCampaignDiscount();

            result.Should().Be(default(double));
            
        }

        [Fact]
        public void Should_apply_coupon()
        {
            var coupon = new Coupon(MinimumPurchaseAmount, AmountOfDiscount, DiscountType.Rate);

            var sut = new Business.Domains.ShoppingCart();
            sut.ApplyCoupon(coupon);

            var result = sut;

            result.AppliedCoupon.Should().Be(coupon);
            result.AppliedCoupon.AmountOfDiscount.Should().Be(coupon.AmountOfDiscount);
            result.AppliedCoupon.MinimumPurchaseAmount.Should().Be(coupon.MinimumPurchaseAmount);
            result.AppliedCoupon.DiscountType.Should().Be(coupon.DiscountType);

        }

        [Fact]
        public void Should_apply_campaigns()
        {
            var category = new Category("Electronic");
            var campaigns = new List<Campaign>();

            var sut = new Business.Domains.ShoppingCart();

            for (int i = 1; i < AppliedCampaignCount + 1; i++)
            {
                var campaign = new Campaign(category, AmountOfDiscount, MinimumItemQuantity, DiscountType.Amount);
                campaigns.Add(campaign);
            }

            sut.ApplyCampaigns(campaigns.ToArray());

            var result = sut;

            result.AppliedCampaigns.Should().HaveCount(campaigns.Count);

        }

        [Fact]
        public void Should_create_line_item_by_adding_product_with_quantity()
        {
            var category = new Category("Electronic");
            var products = new List<Product>();

            var sut = new Business.Domains.ShoppingCart();

            for (int i = 1; i < LineItemCount+1; i++)
            {
                var product = new Product($"Notebook-{i}", 1000.0 * i, category);
                sut.AddItem(product, Quantity);
                products.Add(product);
            }

            var result = sut;

            result.LineItems.Should().HaveCount(LineItemCount);
            result.LineItems.Select(a => a.Product).Count().Should().Be(products.Count);
            result.LineItems.Sum(a => a.Quantity).Should().Be(LineItemCount * Quantity);

        }
    }
}
