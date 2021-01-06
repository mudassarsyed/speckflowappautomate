using TechTalk.SpecFlow;
using TestApplication.UiTests.Drivers;

namespace TestApplication.UiTests.Steps
{
    [Binding]
    public class wikipediaSteps
    {
        private readonly BrowserDriver _driver;

        public wikipediaSteps(BrowserDriver driver)
        {
            _driver = driver;
        }

        [Given(@"goto wikipedia")]
        public void GivenINavigatedToGoogle()
        {
            _driver.GoToGoogle();
        }
        
    }
}
