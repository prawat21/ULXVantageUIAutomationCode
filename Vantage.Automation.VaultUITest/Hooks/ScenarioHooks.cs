using System;
using System.IO;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;
using Vantage.Automation.VaultUITest.Steps;

namespace Vantage.Automation.VaultUITest.Hooks
{
    [Binding]
    public class ScenarioHooks : BaseSteps
    {
        private readonly UIContext _uiContext;

        public ScenarioHooks(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [BeforeScenario]
        private void BeforeHooks()
        {
            FileUtility.RemoveContents(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData"));
            LogScenarioStage("Start");
        }

        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            var helper = new ScreenshotsHelper(_uiContext, ScenarioContext);
            helper.TryAddArtifactToResults(helper.MakeCurrentScreenshot());
            if (_uiContext.XrmApp != null)
            {
                _uiContext.XrmApp.Dispose();
            }
        }

        [AfterScenario(Order = 1)]
        private void LogScenarioFinish()
        {
            LogScenarioStage("Finish");
        }

        private void LogScenarioStage(string stage)
        {
            var message = string.Format($"{stage} scenario \"{0}\"", ScenarioContext.ScenarioInfo.Title);
            Log.TestLog.Info("###########################################################");
            Log.TestLog.Info(message);
        }
    }
}
