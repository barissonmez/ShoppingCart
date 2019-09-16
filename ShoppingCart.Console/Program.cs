using System;
using ShoppingCart.Business.Domains;
using ShoppingCart.Business.Enum;

namespace ShoppingCart.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var food = new Category("Food");
            var electronic = new Category("Electronic");

            var apple = new Product("Apple", 100.0, food);
            var banana = new Product("Banana", 200.0, food);
            var mobilePhone = new Product("Mobile Phone", 1000.0, electronic);
            var notebook = new Product("Notebook", 3000.0, electronic);

            var cart = new Business.Domains.ShoppingCart();
            cart.AddItem(apple, 1);
            cart.AddItem(banana, 1);
            cart.AddItem(mobilePhone, 3);
            cart.AddItem(notebook, 1);

            var campaign1 = new Campaign(food, 20.0, 2, DiscountType.Rate);
            var campaign2 = new Campaign(food, 30.0, 2, DiscountType.Amount);
            var campaign3 = new Campaign(electronic, 30.0, 1, DiscountType.Amount);
            cart.ApplyCampaigns(campaign1, campaign2, campaign3);

            var coupon = new Coupon(500.0, 10, DiscountType.Amount);
            cart.ApplyCoupon(coupon);

            cart.Print();
        }
    }
}
