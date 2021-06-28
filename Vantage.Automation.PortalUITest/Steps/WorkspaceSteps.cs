using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Pages;

namespace Vantage.Automation.PortalUITest.Steps
{
    [Binding]
    public class WorkspaceSteps
    {
        private readonly UIContext _uiContext;
        public WorkspaceSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I go to Service Requests")]
        public void GoToCServiceRequests()
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            workspacePage.GoToServiceRequests();
        }

        [When("I go to create a new Service Request")]
        public void GoToCreateNewServiceRequest()
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            workspacePage.GoToServiceRequests();
            workspacePage.ClickNewServiceRequest();
        }

        [When("I click the Tiles View Button")]
        public void ClickTilesViewButton()
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            workspacePage.ClickTilesViewButton();
        }

        [When("I click the Table View Button")]
        public void ClickTableViewButton()
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            workspacePage.ClickTableViewButton();
        }

        [Then("I verify that at least (.*) Tiles are present")]
        public void VerifyTileCount(int expectedTileCount)
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            int tileCount = workspacePage.CountTiles();
            Assert.IsTrue(expectedTileCount <= tileCount, "Expected minimum {0} tiles but found {1}", expectedTileCount, tileCount);            
        }

        [Then("I verify that at least (.*) Table Rows are present")]
        public void VerifyRowsCount(int expectedRowCount)
        {
            WorkspacePage workspacePage = new WorkspacePage(_uiContext);
            int rowCount = workspacePage.CountTableRows();
            Assert.IsTrue(expectedRowCount <= rowCount, "Expected minimum {0} table rows but found {1}", expectedRowCount, rowCount);
        }
    }
}
