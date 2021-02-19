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
    public class localSteps
    {
        readonly BrowserDriver _driver;

        public localSteps(BrowserDriver driver)
        {

            _driver = driver;
           
        }

        [Given(@"I am on the wikipedia page local")]
        public void GivenIAmOnTheWikipediaPage()
        {
            _driver.GoToApp();
        }
        
    }
}
