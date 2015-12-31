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
