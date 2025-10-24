using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, TimeSpan timeout)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, timeout);
        }

        protected IWebElement Find(By by) => Driver.FindElement(by);
        protected IWebElement WaitFor(By by) => Wait.Until(d => d.FindElement(by));

        // הקלדה יציבה (ללא Actions) + fallback JS + אירועים
        protected void Type(By by, string text)
        {
            var el = WaitFor(by);
            el.Clear();
            el.Click();
            el.SendKeys(text);

            var val = el.GetAttribute("value");
            if (val != text)
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript(@"
                    arguments[0].value = arguments[1];
                    arguments[0].dispatchEvent(new Event('input',  { bubbles: true }));
                    arguments[0].dispatchEvent(new Event('change', { bubbles: true }));
                    arguments[0].dispatchEvent(new Event('blur',   { bubbles: true }));
                ", el, text);
            }
        }

        // קליק עם fallback ל-JS (TEST123)
        protected void Click(By by)
        {
            var el = WaitFor(by);
            try { el.Click(); }
            catch { ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", el); }
        }
    }
}
