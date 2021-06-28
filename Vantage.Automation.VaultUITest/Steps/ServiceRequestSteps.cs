using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class ServiceRequestSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public ServiceRequestSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I go to create a New Service Request")]
        public void GoToCreateNewAgreement()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Service Requests");
            _uiContext.XrmApp.CommandBar.ClickCommand("Create Service Request");
        }

        [When(@"I fill in the following fields on Service Request page:")]
        public void FillInTheFollowingFieldsOnServiceRequestPage(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            var fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
            fieldSetter.FillInFields(keyValues);
        }
    }
}