using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly UIContext _uiContext;
        private readonly ScenarioContext _scenarioContext;

        private static string _testRunFolder;

        public ScenarioHooks(ScenarioContext scenarioContext, UIContext context)
        {
            _scenarioContext = scenarioContext;
            _uiContext = context;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            if (ConfigHelper.KillChromeDriversOnStart)
            {
                KillChromeDrivers();
            }

            var screenshotsFolder = Path.Combine(Environment.CurrentDirectory, ConfigHelper.ScreenshotsPath);
            if (!Directory.Exists(screenshotsFolder))
            {
                Directory.CreateDirectory(screenshotsFolder);
            }
            _testRunFolder = Path.Combine(screenshotsFolder, "TestRun_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
        }
        
        [AfterScenario]
        public void AfterScenario()
        {
            if (_uiContext.XrmApp != null)
            {
                if (_scenarioContext.TestError != null)
                {
                    if (!Directory.Exists(_testRunFolder))
                    {
                        Directory.CreateDirectory(_testRunFolder);
                    }
                    
                    string testTitle = Regex.Replace(_scenarioContext.ScenarioInfo.Title, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                    string screenshotPath = Path.Combine(_testRunFolder, $"Fail_{testTitle}.png");
                    _uiContext.WebClient.Browser.TakeWindowScreenShot(screenshotPath, OpenQA.Selenium.ScreenshotImageFormat.Png);

                    Console.WriteLine("Failure Screenshot: " + screenshotPath);
                    TryAddArtifactToResults(screenshotPath);
                }

                _uiContext.XrmApp.Dispose();
                if (ConfigHelper.KillChromeDriversOnStop)
                {
                    KillChromeDrivers();
                }
            }
        }

        private static void KillChromeDrivers()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                chromeDriverProcess.Kill();
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
