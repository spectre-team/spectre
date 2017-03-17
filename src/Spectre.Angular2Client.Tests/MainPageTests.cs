using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Spectre.Angular2Client.Tests
{
    [TestFixture, Category("Angular2Client")]
    public class MainPageTests
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void SetUpClass()
        {
            _driver = new InternetExplorerDriver {Url = "http://localhost:440"};
        }

        [OneTimeTearDown]
        public void TearDownClass()
        {
            _driver.Close();
            _driver.Dispose();
            _driver = null;
        }

        [Test]
        public void PageOpens()
        {
            Assert.IsTrue(_driver.Title.Contains("Spectre"), $"Title does not contain 'Spectre': {_driver.Title}\nSource:\n{_driver.PageSource}");
        }

        [Test]
        public void DriverStarts()
        {
            //do nothing
        }
    }
}
