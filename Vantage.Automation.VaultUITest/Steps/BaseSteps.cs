using TechTalk.SpecFlow;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public abstract class BaseSteps
    {
        protected ScenarioContext ScenarioContext { get; }

        protected BaseSteps(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }
    }
}
