using System;
using NUnit.Framework;
using SauceDemo.Pages;
using SauceDemo.Tests.Hooks;

namespace SauceDemo.Tests
{
    public class LoginPomTests : TestBase
    {
        [Test]
        [Category("smoke")]
        public void Login_ShouldSeeProducts_WithPOM()
        {
            new LoginPage(Driver, TimeSpan.FromSeconds(15))
                .Open()
                .Login("standard_user", "secret_sauce");

            var inventory = new InventoryPage(Driver, TimeSpan.FromSeconds(15));
            Assert.That(inventory.TitleText(), Does.Contain("Products"));
        }
        //TEST

    }
}
