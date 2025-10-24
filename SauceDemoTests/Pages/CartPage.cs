using System;
using System.Linq;
using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class CartPage : BasePage
    {
        private By CartLink => By.CssSelector(".shopping_cart_link");   // אייקון העגלה בראש
        private By CheckoutBtn => By.Id("checkout");                        // כפתור Checkout בעמוד העגלה
        private By CartBadge => By.CssSelector(".shopping_cart_badge");   // מונה פריטים ליד האייקון

        public CartPage(IWebDriver d, TimeSpan t) : base(d, t) { }

        public void OpenCart()
        {
            var link = WaitFor(CartLink);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", link);

            // המתנה לטעינת עמוד העגלה (URL או הופעת כפתור Checkout)
            Wait.Until(d =>
                d.Url.Contains("cart.html", StringComparison.OrdinalIgnoreCase) ||
                d.FindElements(CheckoutBtn).Any()
            );
        }

        public string? BadgeText() =>
            Driver.FindElements(CartBadge).FirstOrDefault()?.Text;

        // ← מתודה מעודכנת (ממתינה ל־checkout-step-one)
        public void Checkout()
        {
            var btn = WaitFor(CheckoutBtn);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", btn);

            Wait.Until(d =>
                d.Url.Contains("checkout-step-one", StringComparison.OrdinalIgnoreCase) ||
                d.FindElements(By.Id("first-name")).Any()
            );
        }
    }
}
