using System;
using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class InventoryPage : BasePage
    {
        private By Title => By.CssSelector(".title");
        private By InventoryList => By.CssSelector(".inventory_list");

        // ממיר שם מוצר ל-slug של data-test, למשל:
        // "Sauce Labs Backpack" -> "add-to-cart-sauce-labs-backpack"
        private string ToAddSlug(string name) =>
            "add-to-cart-" + name.Trim().ToLower().Replace(" ", "-");

        private By AddToCartDataTest(string name) =>
            By.CssSelector($"[data-test='{ToAddSlug(name)}']");

        public InventoryPage(IWebDriver d, TimeSpan t) : base(d, t) { }

        public string TitleText() => WaitFor(Title).Text;

        public void WaitUntilLoaded()
        {
            // ודאי שהעמוד והמוצרים נטענו לפני פעולות
            WaitFor(InventoryList);
        }

        public void AddProduct(string name)
        {
            WaitUntilLoaded();
            var by = AddToCartDataTest(name);
            WaitFor(by).Click();
        }

        // אופציונלי: קיצור ישיר ויציב לתיק-גב
        public void AddBackpack()
        {
            WaitUntilLoaded();
            WaitFor(By.CssSelector("[data-test='add-to-cart-sauce-labs-backpack']")).Click();
        }
    }
}
