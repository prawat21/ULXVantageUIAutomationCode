using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ServiceRequestTaskSteps
    {
        private readonly UIContext _uiContext;

        public ServiceRequestTaskSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I select the Service Request Tasks tab")]
        public void SelectServiceRequestTasksTab()
        {
            _uiContext.XrmApp.Entity.SelectTab("Tasks");
        }

        [When("I add a task to the Service Request with the following properties")]
        public void AddATaskToAServiceRequest(Table table)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand("subgrid_tasks", "New Task");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.QuickCreateFillOutFields(keyValues);

            _uiContext.XrmApp.QuickCreate.Save();
        }
    }
}
