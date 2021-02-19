using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using TechTalk.SpecFlow;
using TestApplication.UiTests.Drivers;

namespace TestApplication.UiTests.Steps
{
    [Binding]
    public class singleSteps
    {
        readonly BrowserDriver _driver;

        public singleSteps(BrowserDriver driver)
        {

            _driver = driver;
           
        }

        [Given(@"I am on wikipedia page")]
        public void GivenIAmOnTheWikipediaPage()
        {
            _driver.GoToApp();
        }
        
    }
}
