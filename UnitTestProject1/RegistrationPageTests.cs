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
        public void RegistrationPageObject_FillAllFieldsAndSendForm_WaitForMailPageApears()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            FillInAllRegistraionFields(registrationPage);
            registrationPage.SendForm<WaitForMAilPage>();
        }

        [TestMethod]
        public void RegistrationPageObject_ClickTermsAndConditionLink_DivWithTermsAndConditionsAppear()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.clickTermsAndConditions();

            Assert.IsTrue(registrationPage.isTermsAndConditionsDivApears());
        }

        private string somePassword = "Abcdefgh";
        private string someOtherPassword = "12345678";

        [TestMethod]
        public void RegistrationPageObject_ConfirmedPasswordDifferentFromPassword_ErrorMessageAppears()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.SetPassword(somePassword);
            registrationPage.SetConfirmPassword(someOtherPassword);
            Keyborad.PressTabButton();

            Assert.IsTrue(registrationPage.isPasswordNotEqualMsgAppears());
        }

        [TestMethod]
        public void RegistrationPageObject_HomeLinkClicked_HomePageAppears()
        {
            var registrationPage = RegistrationPage.LaunchSiteAndGetPage();
            registrationPage.clickHomeLinkAndGetHomePage();
        }



        private static RegistrationPage FillInAllRegistraionFields(RegistrationPage i_RegistrationPage)
        {
            return i_RegistrationPage.SetUsername("Je32sus43")
                .SetEmail("Je3sus243@yahoo.com")
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
