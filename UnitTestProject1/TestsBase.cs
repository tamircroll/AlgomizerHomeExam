using AlgmizerAutomationFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace AlgomizerTests
{
    public class TestsBase
    {
        [TestInitialize]
        public void InitTest()
        {
            Driver.Initialize(new ChromeDriver());
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Instance.Quit();
        }
    }
}
