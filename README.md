Prerequisites
Install Visual Studio with .NET Core v3.1

Appium

Specflow+ Runner

Get your BrowserStack credentials from here.

Set them as environment variables like this:  


set BROWSERSTACK_USERNAME=<browserstack-username>
set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>

Run your first test :
Upload your Android or iOS App

Upload your Android app (.apk or .aab file) or iOS app (.ipa file) to BrowserStack servers using our REST API. Here is an example cURL request :


curl -u "YOUR_USERNAME:YOUR_ACCESS_KEY" \
-X POST "https://api-cloud.browserstack.com/app-automate/upload" \
-F "file=@/path/to/apk/file"

Ensure that @ symbol is prepended to the file path in the above request. Please note the app_url value returned in the API response. We will use this to set the application under test while configuring the test later on.

Note: If you do not have an .apk or .ipa file and are looking to simply try App Automate, you can download and test using our sample Android app or sample iOS app.

2. Configure your First Test.

Specflow is BDD framework. Hence we will have *.feature files and steps with respect to these files. For this sample repository we will be navigating to App homepage.The feature file for that is as follows:

Wikipidie.feature


Feature: wikipedia

@Google_Pixel3
@Samsung_galaxyS10
@Samsung_galaxyNote20
Scenario: Goto wikipedia
	  Given I am on the wikipedia page
This file uses tag for devices that we will be using for each tests. The step file w.r.t. the above feature file is as follows:


[Binding]
    public class wikipediaSteps
    {
        readonly BrowserDriver _driver;
      
        public wikipediaSteps(BrowserDriver driver)
        {

            _driver = driver;
           
        }

        [Given(@"I am on the wikipedia page")]
        public void GivenIAmOnTheWikipediaPage()
        {
            _driver.GoToApp();
        }
        
    }
Define BrowserStack Username and Accesskey in test-appsettings.json and change device and os_version value as per your requirenment.


{
	"seleniumBaseUrl": "hub.browserstack.com",
	"browserstack_username": "BROWSERSTACK_USER",
	"browserstack_access_key": "BROWERSTACK_ACCESS_KEY",
	"browserstack_buildName": "demo-specflow-csharp",
	"browserstack_baseSessionName": "Specflow+ Sample Test",
	"browserstack_projectName": "Demo CSharp",
	"pixel3": {
        "browserstack.local": true,
          "device": "Google Pixel 3",
          "os_version": "10.0"
        },
        "galaxyNote20": {
          "device": "Samsung Galaxy Note 20",
          "os_version": "10.0"
        },
        "galaxyS10": {
          "device": "Samsung Galaxy S10",
          "os_version": "9.0"
        }
}
In BrowserSeleniumDriverFactory.cs  class add the app_url to app capability which is recieved as response while uploading app in step 1.


 public IWebDriver GetForBrowser(string browserId)
        {
            ...
            caps.SetCapability("app", "app_url");
            ...
        }
            
Launch your tests from Visual Studio by:

Navigating to Test Explorer

Selecting your feature file

Run by clicking on “Run” button


 

Mark Test as passed or failed
BrowserStack provides a comprehensive REST API and a JavaScript Executor to mark your tests as passed or failed to access and update information about your tests. Shown below is a sample code snippet which allows you to mark your tests as pass or fail based on the assertions in your SpecFlow test cases using JavaScript Executor. A full reference of our REST API can be found here.

GoogleSearchSteps.cs


...
        [AfterScenario]
		public void MarkTestAsPassOrFail()
		{
			if (passed)
			{
				((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"Tests function correctly\"}}");
			}
			else
			{
				((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"" + message + "\"}}");
			}
		}
...
Parallel Testing
For testing in parallel, we use *.srprofile which is a configuration file for Specflow+. It automatically picks up runtime properties from it.

Default.srprofile


<?xml version="1.0" encoding="utf-8"?>
<TestProfile xmlns="http://www.specflow.org/schemas/plus/TestProfile/1.5">
  <Settings projectName="TestApplication.UiTests" projectId="{347ddf1e-cf82-4520-8c9b-724ed9d7f5f0}" />
  <Execution stopAfterFailures="0" testThreadCount="5" testSchedulingMode="Sequential" retryCount="0" apartmentState="MTA"  />
  <Environment testThreadIsolation="Process" />
  <TestAssemblyPaths>
    <TestAssemblyPath>TestApplication.UiTests.dll</TestAssemblyPath>
  </TestAssemblyPaths>
	
  <Report>
    <Template name="Report\ReportTemplate.cshtml"/>
  </Report>
  
  <Targets>
    <Target name="Samsung_galaxyS10">
      <Filter>Samsung_galaxyS10</Filter>
      <DeploymentTransformationSteps>
        <EnvironmentVariable variable="Test_Browser" value="GalaxyS10" />
      </DeploymentTransformationSteps>
    </Target>
    <Target name="Google_Pixel3">
      <Filter>Google_Pixel3</Filter>
      <DeploymentTransformationSteps>
        <EnvironmentVariable variable="Test_Browser" value="Pixel3" />
      </DeploymentTransformationSteps>
    </Target>
    <Target name="Samsung_galaxyNote20">
      <Filter>Samsung_galaxyNote20</Filter>
      <DeploymentTransformationSteps>
        <EnvironmentVariable variable="Test_Browser" value="GalaxyNote20" />
      </DeploymentTransformationSteps>
    </Target>
  </Targets>

	<TestThreads>
		<TestThread id="0">
			<TestAffinity>testpath:Target:Google_Pixel3</TestAffinity>
		</TestThread>
		<TestThread id="1">
			<TestAffinity>testpath:Target:Samsung_galaxyS10</TestAffinity>
		</TestThread>
		<TestThread id="2">
			<TestAffinity>testpath:Target:Samsung_galaxyNote20</TestAffinity>
		</TestThread>
	</TestThreads>
</TestProfile>

Launch your tests using:


dotnet test
OR from Visual Studio:

Navigating to Test Explorer

Select the project root

Run all the tests by clicking on “Run All Tests” button


Local Testing
Prerequisite: You’ll need C# local language binding for Windows application or BrowserStack Local Binary if testing on a linux/macOS machine.

You will need to specify the correct capability in test-appsettings.json:


{
  ...
  "chrome": {
    ...
    "browserstack.local": true,
    ...
  },
  ...
}
Windows:
If you are using a Windows machine, the above repository is already configured to pick up the capability and initiate BrowserStackLocal automatically.

Linux/MacOS:
For this configuration you will have to comment out some code from above repository and it goes as follows:

Drivers/WebDriver.cs


private IWebDriver GetWebDriver()
{
	string testBrowserId = Environment.GetEnvironmentVariable("Test_Browser");
	// if (_currentLocal == null)
	// {
	//  	_currentLocal = GetBrowserStackLocal();
	// }
	return _browserSeleniumDriverFactory.GetForBrowser(testBrowserId);
}
and from your command line run before starting your tests:


./BrowserStackLocal --key <access_key>
 

