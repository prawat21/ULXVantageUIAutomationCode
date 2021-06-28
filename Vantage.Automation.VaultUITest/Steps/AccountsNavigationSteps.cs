using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AccountsNavigationSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AccountsNavigationSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open Active Accounts view")]
        public void OpenAccountsTab()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Accounts");
            _uiContext.XrmApp.Grid.SwitchView("Active Accounts");
        }

        [When(@"I open accounts grid record with name '(.*)'")]
        public void OpenAccountsGridRecordWithName(string name)
        {
            name = new StringTransformer().Transform(name);
            var indexToSelect = _uiContext.XrmApp.Grid.GetGridItems().Select((value, index) => new { Value = value, Index = index })
                .First(pair => pair.Value.Attributes[pair.Value.Attributes.Keys.First(x => x.Contains("name"))].ToString() == name).Index;
            _uiContext.XrmApp.Grid.OpenRecord(indexToSelect);
        }

        [When(@"I go to create New Account")]
        public void CreateNewAccount()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Accounts");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");
        }
    }
}
