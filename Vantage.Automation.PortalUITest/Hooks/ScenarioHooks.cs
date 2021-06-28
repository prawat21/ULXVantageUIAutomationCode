using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Helpers;

namespace Vantage.Automation.PortalUITest.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly UIContext _uiContext;
        private readonly ScenarioContext _scenarioContext;

        private static string _testRunFolder;

        public ScenarioHooks(UIContext context, ScenarioContext scenarioContext)
        {
            _uiContext = context;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var screenshotsFolder = Path.Combine(Environment.CurrentDirectory, "Screenshots");
            if (!Directory.Exists(screenshotsFolder))
            {
                Directory.CreateDirectory(screenshotsFolder);
            }
            _testRunFolder = Path.Combine(screenshotsFolder, "TestRun_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            foreach (string attachment in _uiContext.Attachments)
            {
                new FileUtility().DeleteFile(attachment);
            }

            if (_scenarioContext.TestError != null)
            {

                if (!Directory.Exists(_testRunFolder))
                {
                    Directory.CreateDirectory(_testRunFolder);
                }
                string testTitle = Regex.Replace(_scenarioContext.ScenarioInfo.Title, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                string screenshotPath = Path.Combine(_testRunFolder, $"Fail_{testTitle}.png");
                _uiContext.Driver.TakeScreenshot().SaveAsFile(screenshotPath, OpenQA.Selenium.ScreenshotImageFormat.Png);

                Console.WriteLine("Failure Screenshot: " + screenshotPath);
                TryAddArtifactToResults(screenshotPath);
            }

            if (_uiContext.Driver != null)
            {
                _uiContext.Driver.Dispose();
            }
        }

        private bool TryAddArtifactToResults(string filePath)
        {
            if (!_scenarioContext.ScenarioContainer.IsRegistered<TestContext>())
            {
                Console.WriteLine(
                    $"Cannot create an instance of Microsoft.VisualStudio.TestTools.UnitTesting.TestContext for {GetType().Name}. Artifact {filePath} won't be sent to attachments");
                return false;
            }

            _scenarioContext.ScenarioContainer.Resolve<TestContext>().AddResultFile(filePath);
            return true;
        }
    }
}
