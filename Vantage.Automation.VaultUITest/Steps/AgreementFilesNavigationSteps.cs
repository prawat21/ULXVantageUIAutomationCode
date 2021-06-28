using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementFilesNavigationSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AgreementFilesNavigationSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open Active Agreement files view")]
        public void OpenActiveAgreementFilesView()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreement Files");
            _uiContext.XrmApp.Grid.SwitchView("Active Agreement Files");
        }

        [When(@"I go to create New Agreement File")]
        public void CreateNewAgreementFile()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreement Files");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");
        }
    }
}
