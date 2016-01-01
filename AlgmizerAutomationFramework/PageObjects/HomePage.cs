using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

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

        protected override List<string> idsToValidateList()
        {
            return null;
        }

        protected override List<string> classesToValidateList()
        {
            return null;
        }
    }
}
