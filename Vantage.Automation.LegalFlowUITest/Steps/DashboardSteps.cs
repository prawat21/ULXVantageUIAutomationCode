using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private readonly UIContext _uiContext;

        public DashboardSteps(UIContext context)
        {
            _uiContext = context;
        }

        [Then("I go to view Legal Professional Dashboard")]
        public void ViewLegalProfessionalDashboard()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Dashboard");
            _uiContext.XrmApp.Dashboard.SelectDashboard("Legal Professional");
        }

        [Then("I go to view Triage Dashboard")]
        public void ViewTriageDashboard()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Dashboard");
            _uiContext.XrmApp.Dashboard.SelectDashboard("Triage Dashboard");
        }

        [Then("I go to view Legal Pro Team Lead Dashboard")]
        public void ViewLegalProTeamLeadDashboard()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Dashboard");
            _uiContext.XrmApp.Dashboard.SelectDashboard("Legal Pro Team Lead");
        }        
    }
}
