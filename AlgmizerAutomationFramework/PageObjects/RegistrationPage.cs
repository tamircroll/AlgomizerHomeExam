using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using AlgmizerAutomationFramework.Utilities;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework.PageObjects
{
    public class RegistrationPage : PageObjectBase
    {
        protected override List<string> idsToValidateList()
        {
            return new List<string>
            {
                "acc_setup_wrapper",
                "has_domain",
                "brand_domain",
                "email",
                "acc_setup_wrapper",
                "confirm_pwd",
                "phone_prefix",
                "phone_number",
            };
        }

        public static RegistrationPage LaunchSiteAndGetPage()
        {
            IWebDriver driver = Driver.Instance;
            Driver.Instance.Navigate().GoToUrl(@"http://test.ad-assistant.com/#/register");
            driver.Manage().Window.Maximize();

            return WaitAndGetPage<RegistrationPage>();
        }

        public static void LaunchSite()
        {
            Driver.Instance.Navigate().GoToUrl(@"http://test.ad-assistant.com/#/register");
        }

        public RegistrationPage CheckBuisnessWebsiteCheckBox()
        {
            IWebElement checkBox = m_Driver.FindElement(By.Id("has_domain"));
            if (!checkBox.Selected) checkBox.Click();

            return this;
        }

        public RegistrationPage UncheckBuisnessWebsiteCheckBox()
        {
            IWebElement checkBox = m_Driver.FindElement(By.Id("has_domain"));
            if (checkBox.Selected) checkBox.Click();

            return this;
        }

        public RegistrationPage SetBuisnessWebSite(string i_WebSite)
        {
            CheckBuisnessWebsiteCheckBox();
            m_Driver.FindElement(By.Id("brand_domain")).SendKeys(i_WebSite);

            return this;
        }

        public RegistrationPage SetUsername(string i_UserName)
        {
            m_Driver.FindElement(By.Id("username")).SendKeys(i_UserName);

            return this;
        }

        public RegistrationPage SetEmail(string i_Email)
        {
            m_Driver.FindElement(By.Id("email")).SendKeys(i_Email);

            return this;
        }

        public RegistrationPage SetPassword(string i_Password)
        {
            m_Driver.FindElement(By.Id("password")).SendKeys(i_Password);
            return this;
        }

        public RegistrationPage SetConfirmPassword(string i_Password)
        {
            m_Driver.FindElement(By.Id("confirm_pwd")).SendKeys(i_Password);

            return this;
        }

        public RegistrationPage SetPasswordAndConfirmPasswordWithSamePassword(string i_Password)
        {
            SetPassword(i_Password);
            SetConfirmPassword(i_Password);

            return this;
        }

        public RegistrationPage SetPhonePrefix(ePhonePrefix i_ePrefix)
        {
            m_Driver.FindElement(By.Id("phone_prefix")).Click();
            Thread.Sleep(500);
            ReadOnlyCollection<IWebElement> prefixes = m_Driver.FindElement(By.ClassName("dropdown-menu"))
                .FindElements(By.TagName("li"));

            foreach (IWebElement prefix in prefixes)
            {
                IWebElement countryCode = prefix.FindElement(By.ClassName("fll"));
                if (countryCode.Text == i_ePrefix.ToString())
                {
                    prefix.Click();
                    return this;
                }
            }

            throw new Exception("Couldn't find the phone prefix: " + i_ePrefix);
        }

        public RegistrationPage SetPhoneNumber(string i_Phone)
        {
            m_Driver.FindElement(By.Id("phone_number")).SendKeys(i_Phone);
            return this;
        }

        public RegistrationPage AgreeToCreateGoogleAccount()
        {
            IWebElement checkBox = m_Driver.FindElement(By.Id("account"));
            if (!checkBox.Selected) checkBox.Click();

            return this;
        }

        public RegistrationPage AgreeToTermsOfUse()
        {
            IWebElement checkBox = m_Driver.FindElement(By.Id("terms"));
            if (!checkBox.Selected) checkBox.Click();

            return this;
        }

        public T SendForm<T>() where T : PageObjectBase, new()
        {
            m_Driver.FindElement(By.ClassName("common-btn")).Click();
            return WaitAndGetPage<T>();
        }
    }
}
