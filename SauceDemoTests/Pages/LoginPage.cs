using System;                               // חשוב!
using OpenQA.Selenium;

namespace SauceDemo.Pages
{
    public class LoginPage : BasePage
    {
        private By UserName => By.Id("user-name");
        private By Password => By.Id("password");
        private By LoginBtn => By.Id("login-button");
        private By ErrorMsg => By.CssSelector("h3[data-test='error']");

        public LoginPage(IWebDriver d, TimeSpan t) : base(d, t) { }

        public LoginPage Open()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            return this;
        }

        public void Login(string user, string pass)
        {
            Find(UserName).SendKeys(user);
            Find(Password).SendKeys(pass);
            Find(LoginBtn).Click();
        }

        public string? GetError() =>
            Driver.FindElements(ErrorMsg).FirstOrDefault()?.Text;
    }
}
