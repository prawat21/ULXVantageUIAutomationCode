using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ContactSteps
    {
        private readonly UIContext _uiContext;

        public ContactSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I go to view Active Contacts")]
        public void GoToActiveContacts()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Contacts");
            _uiContext.XrmApp.Grid.SwitchView("Active Contacts");
        }

        [Then("I verify that I can run Products By Contact Report")]
        public void ContactProductsReport()
        {
            int oldTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            _uiContext.XrmApp.CommandBar.ClickCommand("Run Report", "Products By Contact");
            _uiContext.XrmApp.Dialogs.ReportDialog(true);
            _uiContext.XrmApp.ThinkTime(5000);
            int newTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            Assert.IsTrue(newTabCount > oldTabCount, "Report was not opened");
        }
    }
}
