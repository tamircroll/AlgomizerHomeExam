using System;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        public static void Initialize(IWebDriver driver)
        {
            Instance = driver;
            Instance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }
    }
}
