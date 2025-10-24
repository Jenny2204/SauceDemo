using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Hooks
{
    public abstract class TestBase
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        [SetUp]
        public void SetUp()
        {
            // Read HEADLESS from environment (default: false)
            var headless = (Environment.GetEnvironmentVariable("HEADLESS") ?? "false")
                .Equals("true", StringComparison.OrdinalIgnoreCase);

            var options = new ChromeOptions();
            if (headless) options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1280,900");

            // Extra stability flags for CI runners (Linux/containers)
            var isCi = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITHUB_ACTIONS")) ||
                       !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
            if (isCi)
            {
                options.AddArgument("--no-sandbox");           // avoid sandbox issues in containers
                options.AddArgument("--disable-dev-shm-usage"); // use disk instead of /dev/shm (low shared memory)
                options.AddArgument("--disable-gpu");           // safe even in headless
            }

            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                // On failure: capture a screenshot and log the current URL
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    try
                    {
                        var ssDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "TestResults", "Screenshots");
                        Directory.CreateDirectory(ssDir);

                        var file = Path.Combine(
                            ssDir,
                            $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                        );

                        var ss = ((ITakesScreenshot)Driver).GetScreenshot();
                        ss.SaveAsFile(file); // file extension decides the image format
                        TestContext.WriteLine("Saved screenshot: " + file);
                        TestContext.WriteLine("URL on failure: " + Driver.Url);
                    }
                    catch
                    {
                        // best-effort: ignore any screenshot/log exceptions
                    }
                }
            }
            finally
            {
                // Always attempt to quit the browser
                try { Driver?.Quit(); } catch { /* ignore */ }
            }
        }
    }
}
