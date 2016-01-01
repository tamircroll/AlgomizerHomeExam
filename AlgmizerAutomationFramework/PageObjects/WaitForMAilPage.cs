using System.Collections.Generic;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework.PageObjects
{
    public class WaitForMAilPage : PageObjectBase
    {
        public override bool isPagePresented()
        {
            return m_Driver.FindElement(By.TagName("h1")).Text.Contains(@"Done! We've just sent a validation email to ");
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