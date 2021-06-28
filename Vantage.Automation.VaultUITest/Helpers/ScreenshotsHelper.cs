using System;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers.Screenshots;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class ScreenshotsHelper
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly UIContext _uiContext;

        public ScreenshotsHelper(UIContext uiContext, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _uiContext = uiContext;
        }

        public string MakeCurrentScreenshot(int testNameMaximalLength = 55, int guidPostfixLength = 5)
        {
            var testName = new string(_scenarioContext.ScenarioInfo.Title.Take(testNameMaximalLength).ToArray()).Replace(" ", "");
            return new ScreenshotProvider(_uiContext).PublishScreenshot(
                $"{GetType().Name}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("n", CultureInfo.InvariantCulture).Substring(0, guidPostfixLength)}",
                parentDirectory: testName);
        }

        public bool TryAddArtifactToResults(string filePath)
        {
            if (!_scenarioContext.ScenarioContainer.IsRegistered<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>())
            {
                Log.TestLog.Warn(
                    $"Cannot create an instance of Microsoft.VisualStudio.TestTools.UnitTesting.TestContext for {GetType().Name}. Artifact {filePath} won't be sent to attachments");
                return false;
            }

            _scenarioContext.ScenarioContainer.Resolve<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>().AddResultFile(filePath);
            return true;
        }
    }
}
