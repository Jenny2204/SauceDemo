using System;
using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class CheckoutOverviewPage : BasePage
    {
        private By FinishBtn => By.Id("finish");
        private By SuccessTitle => By.CssSelector(".complete-header"); // מציג "THANK YOU"

        public CheckoutOverviewPage(IWebDriver d, TimeSpan t) : base(d, t) { }

        public void Finish() => WaitFor(FinishBtn).Click();
        public string SuccessText() => WaitFor(SuccessTitle).Text;
    }
}
