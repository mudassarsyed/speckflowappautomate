using FluentAssertions;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace TestApplication.UiTests.Drivers
{
    public class BrowserDriver
    {
        private readonly WebDriver _webDriver;

        public BrowserDriver(WebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ValidateTitleShouldBe(string expectedTitle)
        {
            string result = _webDriver.Wait.Until(d => d.Title);
            result.Should().Be(expectedTitle);
        }

        public void GoToGoogle(){
           
        }
    }
}
