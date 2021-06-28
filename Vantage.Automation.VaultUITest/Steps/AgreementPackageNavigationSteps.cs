using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementPackageNavigationSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AgreementPackageNavigationSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open Active Agreement packages view")]
        public void OpenActiveAgreementPackagesView()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreement Packages");
            _uiContext.XrmApp.Grid.SwitchView("Active Agreement Packages");
        }

        [When(@"I open agreement package grid record with name '(.*)'")]
        public void OpenAgreementPackageGridRecordWithName(string name)
        {
            name = new StringTransformer().Transform(name);
            var indexToSelect = _uiContext.XrmApp.Grid.GetGridItems().Select((value, index) => new { Value = value, Index = index })
                .First(pair => pair.Value.Attributes[pair.Value.Attributes.Keys.First(x => x.Contains("name"))].ToString() == name).Index;
            _uiContext.XrmApp.Grid.OpenRecord(indexToSelect);
        }
    }
}
