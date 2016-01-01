using System.Collections.Generic;
using AlgmizerAutomationFramework.PageObjects;
using AlgmizerAutomationFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgomizerTests
{
    [TestClass]
    public class RegistrationPageTests : TestsBase
    {

        [TestMethod]
        public void RegistrationPageObject_FillAllFields_NextButtonEnabledOnlyAfterAllFieldsSet()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetUsername("UserName");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetEmail("UserName@yahoo.com");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetPassword("Abcdefghi");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetConfirmPassword("Abcdefghi");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetBuisnessWebSite(@"http://www.SomeBuisnessWebsite.com");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetPhonePrefix(ePhonePrefix.IL);
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.SetPhoneNumber("43298753");
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.AgreeToCreateGoogleAccount();
            Assert.IsFalse(registrationPage.IsNextButtonEnabled());
            registrationPage.AgreeToTermsOfUse();
            Assert.IsTrue(registrationPage.IsNextButtonEnabled());
        }

        [TestMethod]
        public void RegistrationPageObject_ClickTermsAndConditionLink_DivWithTermsAndConditionsAppear()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.ClickTermsAndConditions();

            Assert.IsTrue(registrationPage.IsTermsAndConditionsDivApears());
        }

        private const string k_SomePassword = "Abcdefgh";
        private const string k_SomeOtherPassword = "12345678";

        [TestMethod]
        public void RegistrationPageObject_ConfirmedPasswordDifferentFromPassword_ErrorMessageAppears()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.SetPassword(k_SomePassword);
            registrationPage.SetConfirmPassword(k_SomeOtherPassword);
            Keyborad.PressTabButton();

            Assert.IsTrue(registrationPage.IsPasswordNotEqualMsgAppears());
        }

        [TestMethod]
        public void RegistrationPageObject_HomeLinkClicked_HomePageAppears()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.ClickHomeLinkAndGetHomePage();
        }

        [TestMethod]
        public void RegistrationPageObject_ILPhonePrefixChosen_ILPhonePrefixNotShownInPrefixesTable()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.SetPhonePrefix(ePhonePrefix.IL);
            List<string> allPrefixes = registrationPage.GetAllPhonePrefixesNamesFromTable();

            Assert.IsFalse(allPrefixes.Contains(ePhonePrefix.IL.ToString()), ePhonePrefix.GB + " Prefixes exists in table");
        }

//        in comment because user Should be removed the from the AWS before each run of this test (in the initTest() method),
//        But his is how to validate the WaitForMailPage loded:
//
//        [TestMethod] 
//        public void RegistrationPageObject_FillAllFieldsAndSendForm_WaitForMailPageApears()
//        {
//            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
//            FillInAllRegistraionFields(registrationPage);
//            registrationPage.SendForm<WaitForMAilPage>();
//        }

        private static RegistrationPage FillInAllRegistraionFields(RegistrationPage i_RegistrationPage)
        {
            return i_RegistrationPage.SetUsername("UserName")
                .SetEmail("UserName@yahoo.com")
                .SetPassword("Abcdefghi")
                .SetConfirmPassword("Abcdefghi")
                .SetBuisnessWebSite(@"http://www.SomeBuisnessWebsite.com")
                .SetPhonePrefix(ePhonePrefix.IL)
                .SetPhoneNumber("43298753")
                .AgreeToCreateGoogleAccount()
                .AgreeToTermsOfUse();
        }
    }
}
