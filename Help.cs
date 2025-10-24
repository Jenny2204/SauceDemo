////🧪 Regression tests – בדיקות עומק לכל הפיצ׳רים.

////⚡ Sanity tests – בדיקות ממוקדות אחרי תיקון באגים.

////🧭 E2E (End-to-End) – זרימות מלאות במערכת.
////Smoke test(בעברית: בדיקת עשן 🧯) הוא סוג של בדיקה מהירה ושטחית שמטרתה לוודא שהמערכת בכלל עובדת
///
///Build = יצירת הגרסה.
//Deploy = התקנת הגרסה בסביבה אמיתית.


< Project Sdk = "Microsoft.NET.Sdk" >

  < PropertyGroup >
    < OutputType > Exe </ OutputType >
    < TargetFramework > net9.0 </ TargetFramework >
    < ImplicitUsings > enable </ ImplicitUsings >
    < Nullable > enable </ Nullable >
  </ PropertyGroup >

  < ItemGroup >
    < PackageReference Include = "Microsoft.NET.Test.Sdk" Version = "18.0.0" />
    < PackageReference Include = "NUnit" Version = "4.4.0" />
    < PackageReference Include = "NUnit3TestAdapter" Version = "5.2.0" />
    < PackageReference Include = "Selenium.Support" Version = "4.36.0" />
    < PackageReference Include = "Selenium.WebDriver" Version = "4.36.0" />
  </ ItemGroup >

</ Project >

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

[TestFixture]
public class SauceDemoTests
{
    [Test]
    public void LoginPage()
    {

        ChromeDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        driver.FindElement(By.CssSelector("#user-name")).SendKeys("standard_user");
        driver.FindElement(By.CssSelector("#password")).SendKeys("secret_sauce");
        driver.FindElement(By.CssSelector("#login-button")).Click();

        //try
        //{
        //    string titleText = driver.FindElement(By.CssSelector(".title")).Text;
        //    if (titleText.Contains("Products"))
        //        System.Console.WriteLine("התחברות הצליחה ✅");
        //    else
        //        System.Console.WriteLine("התחברות לא הצליחה ❌");
        //}
        //catch
        //{
        //    System.Console.WriteLine("לא מצאתי את הכותרת — כנראה לא התחברנו ❌");
        //}
        Assert
        ////var titleText = driver.FindElement(By.CssSelector(".title")).Text;
        ////Assert.That(titleText, Does.Contain("Products"), "Text 'PRODUCTS' was not found on the page ❌");
        //Assert.That(driver.FindElement(By.CssSelector(".title")).Text,
        //Does.Contain("Products"), "Text 'PRODUCTS' was not found on the page ❌");

        Assert.That(driver.Title, Does.Contain("Swag Labs"));


        driver.Quit();
    }
}
