using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AlgmizerAutomationFramework.PageObjects
{
    public abstract class PageObjectBase
    {

        protected IWebDriver m_Driver = Driver.Instance;

        protected List<string> m_IdsToValidate;

        protected abstract List<string> idsToValidateBy();

        protected abstract List<string> classesToValidateBy();

        public virtual bool isPagePresented()
        {
            bool isIdMissing = false;
            bool isClassMissing = false;
            List<string> isToValidateList = idsToValidateBy();
            List<string> classesToValidateList = this.classesToValidateBy();

            if (isToValidateList != null)
                isIdMissing = isToValidateList.Any(id => m_Driver.FindElement(By.Id(id)) == null);

            if (classesToValidateList != null)
                isClassMissing = classesToValidateList.Any(className => m_Driver.FindElement(By.ClassName(className)) == null);

            if (isIdMissing || isClassMissing) return false;

            return true;
        }

        public static T WaitAndGetPage<T>() where T : PageObjectBase, new()
        {
            return WaitAndGetPage<T>(TimeSpan.FromSeconds(10));
        }

        public static T WaitAndGetPage<T>(TimeSpan i_TimeToWait) where T : PageObjectBase, new()
        {
            T page = new T();
            waitForPageToLoad(page, i_TimeToWait);
            return page;
        }

        protected T switchTabAndGetPage<T>(string i_Url) where T : PageObjectBase, new()
        {
            switchTab(i_Url);
            return WaitAndGetPage<T>();
        }

        private void switchTab(string i_Url)
        {
            var popup = m_Driver.WindowHandles[1]; // handler for the new tab
            if (string.IsNullOrEmpty(popup)) throw new Exception("tab was not opened");
            if (m_Driver.SwitchTo().Window(popup).Url != i_Url) throw new Exception("Not requested Url"); // url is OK  
            m_Driver.SwitchTo().Window(m_Driver.WindowHandles[1]);
        }

        private static void waitForPageToLoad<T>(T i_Page, TimeSpan i_TimeToWait) where T : PageObjectBase
        {
            var wait = new WebDriverWait(Driver.Instance, i_TimeToWait);

            try
            {
                wait.Until(x => i_Page.isPagePresented());
            }
            catch (TimeoutException e)
            {
                throw new TimeoutException(string.Format("Page {0} failed to load", i_Page.GetType().Name), e);
            }
        }
    }
}