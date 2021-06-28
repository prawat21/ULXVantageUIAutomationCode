using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Helpers;

namespace Vantage.Automation.PortalUITest.Pages
{
    public class WorkspacePage
    {
        private readonly UIContext _uiContext;

        private const string _myWorkSpaceLink = "//a[@class='nav-link' and contains(text(),'My Workspace')]";
        private const string _serviceRequestsLink = "//a[@class='nav-link' and contains(text(),'Service Requests')]";
        private const string _newServiceRequestButton = "//a[@routerlink='../new']";

        private const string _tilesViewButton = "//button[@data-cy='tilesViewButton']";
        private const string _tableViewButton = "//button[@data-cy='tableViewButton']";

        private const string _tiles = "//app-tiles-card";
        private const string _tableRows = "//tr[@kendogridlogicalrow]";

        public WorkspacePage(UIContext context)
        {
            _uiContext = context;
        }

        public void GoToMyWorkspace()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_myWorkSpaceLink), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("My workspace link not found");
            }
            element.Click();
        }

        public void GoToServiceRequests()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_serviceRequestsLink), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Service Requests link not found");
            }
            element.Click();
        }

        public void ClickNewServiceRequest()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_newServiceRequestButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("New Service Request button not found");
            }
            element.Click();
        }

        public void ClickTilesViewButton()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_tilesViewButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Tiles view button not found");
            }
            element.Click();
        }

        public void ClickTableViewButton()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_tableViewButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Table view button not found");
            }
            element.Click();
        }

        public int CountTiles()
        {
            var tiles = _uiContext.Driver.FindElements(By.XPath(_tiles));
            return tiles.Count;
        }

        public int CountTableRows()
        {
            var tiles = _uiContext.Driver.FindElements(By.XPath(_tableRows));
            return tiles.Count;
        }

    }
}
