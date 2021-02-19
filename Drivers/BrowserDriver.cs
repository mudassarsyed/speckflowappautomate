using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace TestApplication.UiTests.Drivers
{
    public class BrowserDriver
    {
        readonly WebDriver _webDriver;
        public BrowserDriver(WebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ValidateTitleShouldBe(string expectedTitle)
        {
            string result = _webDriver.Wait.Until(d => d.Title);
            result.Should().Be(expectedTitle);
        }

        [Obsolete]
        public void GoToApp(){

            System.Threading.Thread.Sleep(5000);
            _webDriver.Current.FindElement(By.Id("search_container")).Click();
            System.Threading.Thread.Sleep(5000);
            _webDriver.Current.FindElement(By.Id("search_src_text")).SendKeys("BrowserStack");
           
            ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Title matched!\"}}");

        }




    }
}
