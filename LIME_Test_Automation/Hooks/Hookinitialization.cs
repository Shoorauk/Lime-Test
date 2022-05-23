using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

using AventStack.ExtentReports.Reporter;
using LIME_Test_Automation.Config;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WebDriverManager.DriverConfigs.Impl;

namespace LIME_Test_Automation.Helper
{
    [Binding]
    public sealed class Hookinitialization: WebDriver
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public WebDriver _driver;

        public readonly FeatureContext _featureContext;
        public readonly ScenarioContext _scenarioContext;
        public ExtentTest _currentScenarioName;
        [ThreadStatic]
        public static ExtentTest featureName;
        public static ExtentTest step;
        public static AventStack.ExtentReports.ExtentReports extent;
        public static ExtentKlovReporter klov;


        static string reportPath = System.IO.Directory.GetParent(@"../../../").FullName +
            System.IO.Path.DirectorySeparatorChar + "Result\\ExtentReport.html";

        static string logPath = System.IO.Directory.GetParent(@"../../../").FullName +
            System.IO.Path.DirectorySeparatorChar + "Result";
        public static ConfigSetting configSetting;

        static string configSettingPath = Directory.GetParent(@"../../../").FullName +
            Path.DirectorySeparatorChar + "Config/configsetting.json";

        public Hookinitialization(WebDriver driver, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;

        }
        [BeforeTestRun]
        public static void TestInitilizer()
        {
            //Json File
            configSetting = new ConfigSetting();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configSettingPath);
            IConfigurationRoot configurationRoot = builder.Build();
            configurationRoot.Bind(configSetting);


            //Reporting 
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new AventStack.ExtentReports.ExtentReports();
            klov = new ExtentKlovReporter();
            extent.AttachReporter(htmlReporter);

            //Logging
            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.File(logPath + @"\Logs",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} |{Level :u3} |{Message} {NewLine}",
                rollingInterval: RollingInterval.Day).CreateLogger();
        }



        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            _driver.Driver = new ChromeDriver();

            // EdgeOptions options = new EdgeOptions();
            //_driver.Driver = new EdgeDriver(options);            

            featureName = extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(_featureContext.FeatureInfo.Title);
            _currentScenarioName = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            Log.Information("Selecting feature file {0} to run", context.ScenarioInfo.Title);

        }



        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {

            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    _currentScenarioName.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    _currentScenarioName.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    _currentScenarioName.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    _currentScenarioName.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                //screenshot in the Base64 format
                var mediaEntity = CaptureScreenShot(_scenarioContext.ScenarioInfo.Title.Trim());

                if (stepType == "Given")
                    _currentScenarioName.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "When")
                    _currentScenarioName.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "Then")
                    _currentScenarioName.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
            }
            else if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    _currentScenarioName.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    _currentScenarioName.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    _currentScenarioName.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }

        }



        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Driver.Quit();
            System.Diagnostics.ProcessStartInfo p;
            p = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/C" + "killtask /f /im chromedriver.exe");
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = p;
            proc.Start();
            proc.WaitForExit();
            proc.Close();


        }

        [BeforeStep]
        public void BeforeStep()
        {
            step = _currentScenarioName;
        }
        public MediaEntityModelProvider CaptureScreenShot(string name)
        {
            var screenShot = ((OpenQA.Selenium.ITakesScreenshot)_driver.Driver).GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, name).Build();
        }

    }
}
