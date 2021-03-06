﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using AlgmizerAutomationFramework.Utilities;
using OpenQA.Selenium;

namespace AlgmizerAutomationFramework.PageObjects
{
    public class RegistrationPage : PageObjectBase
    {
        protected override List<string> idsToValidateBy()
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
                "phone_number"
            };
        }

        protected override List<string> classesToValidateBy()
        {
            return new List<string>
            {
                "common-btn"
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
            openPhonePrefixesTable();
            Thread.Sleep(500);
            ReadOnlyCollection<IWebElement> prefixes = m_Driver.FindElement(By.ClassName("dropdown-menu"))
                .FindElements(By.TagName("li"));

            foreach (var prefix in prefixes)
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

        public List<string> GetAllPhonePrefixesNamesFromTable()
        {
            List<string> toReturn = new List<string>();
            openPhonePrefixesTable();

            var allPrefixes = m_Driver.FindElement(By.ClassName("dropdown-menu")).FindElements(By.TagName("li"));

            foreach (var prefix in allPrefixes)
            {
                IWebElement countryCode = prefix.FindElement(By.ClassName("fll"));
                toReturn.Add(countryCode.Text);
            }

            return toReturn;
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

        public void ClickTermsAndConditions()
        {
            var elements = m_Driver.FindElements(By.ClassName("ng-binding"));
            var tcLink = elements.First(element => element.Text == "Terms and Conditions");

            tcLink.Click();
        }

        public bool IsTermsAndConditionsDivApears()
        {
            return m_Driver.FindElement(By.ClassName("registration-terms")) != null;
        }

        public HomePage ClickHomeLinkAndGetHomePage()
        {
            ClickHomeLink();
            return switchTabAndGetPage<HomePage>("http://www.algomizer.com/");
        }

        public void ClickHomeLink()
        {
            var footer = m_Driver.FindElement(By.ClassName("footer-links"));
            var links = footer.FindElements(By.TagName("li"));
            IWebElement homeLink = links.FirstOrDefault(link => link.Text == "Home");

            if (homeLink == null) throw new Exception("Could't find home link");

            homeLink.Click();
        }

        public bool IsPasswordNotEqualMsgAppears()
        {
            var errors = m_Driver.FindElements(By.ClassName("error"));
            return errors.Any(error => error.Text.Contains(@"Passwords don't match, please confirm password"));
        }

        private void openPhonePrefixesTable()
        {
            var dropdownWrapper = m_Driver.FindElement(By.ClassName("dropdown_wrap"));
            bool tableIsOpen = dropdownWrapper.GetAttribute("class").Contains("Open");

            if (!tableIsOpen)
                m_Driver.FindElement(By.Id("phone_prefix")).Click();

        }

        public bool IsNextButtonEnabled()
        {
            return m_Driver.FindElement(By.ClassName("common-btn")).Enabled;
        }
    }
}
