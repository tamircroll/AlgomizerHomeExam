using System.Collections.Generic;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework.PageObjects
{
    public class HomePage : PageObjectBase
    {
        public override bool isPagePresented()
        {
            IWebElement currentItem = m_Driver.FindElement(By.ClassName("current-menu-item"));

            if (currentItem.Text == "Home") return true;

            return false;
        }

        protected override List<string> idsToValidateBy()
        {
            return null;
        }

        protected override List<string> classesToValidateBy()
        {
            return null;
        }
    }
}
