using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;   // ← חשוב ל- WebDriverWait
using SauceDemo.Pages;
using SauceDemo.Tests.Hooks;

namespace SauceDemo.Tests
{
    public class CartTests : TestBase
    {
        [Test]
        [Category("smoke")]
        public void AddToCart_ShouldIncreaseBadge()
        {
            new LoginPage(Driver, TimeSpan.FromSeconds(15))
                .Open()
                .Login("standard_user", "secret_sauce");

            var inv = new InventoryPage(Driver, TimeSpan.FromSeconds(15));
            inv.AddBackpack();

            var cart = new CartPage(Driver, TimeSpan.FromSeconds(15));
            cart.OpenCart();

            Assert.That(cart.BadgeText(), Is.EqualTo("1"), "מונה העגלה לא הראה 1");
            TestContext.Progress.WriteLine("✅ ddToCart_ShouldIncreaseBadg");

        }

        [Test]
        [Category("smoke")]
        public void Cart_ToCheckout_Navigation()
        {
            new LoginPage(Driver, TimeSpan.FromSeconds(15))
                .Open()
                .Login("standard_user", "secret_sauce");

            var inv = new InventoryPage(Driver, TimeSpan.FromSeconds(15));
            inv.AddBackpack();

            var cart = new CartPage(Driver, TimeSpan.FromSeconds(15));
            cart.OpenCart();
            cart.Checkout(); // מוביל ל- checkout-step-one

            // בלי Page Object: רק וידוא שהמסך נטען
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Assert.That(() =>
            {
                wait.Until(d =>
                    d.Url.Contains("checkout-step-one", StringComparison.OrdinalIgnoreCase) ||
                    d.FindElements(By.Id("first-name")).Any()
                );
            }, Throws.Nothing, "Checkout Step One לא נטען בזמן");
        }

    }
}
