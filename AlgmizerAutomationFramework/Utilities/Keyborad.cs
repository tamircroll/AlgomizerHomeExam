using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework.Utilities
{
    public class Keyborad
    {
        static Actions action = new Actions(Driver.Instance);

        public static void PressTabButton()
        {
            action.SendKeys(Keys.Tab).Build().Perform();
        }
    }
}