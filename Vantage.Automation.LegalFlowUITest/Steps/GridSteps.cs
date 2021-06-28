using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class GridSteps
    {
        private readonly UIContext _uiContext;

        public GridSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I search the Grid for '(.*)'")]
        public void SearchGridFor(string searchQuery)
        {
            _uiContext.XrmApp.Grid.Search(searchQuery);
        }

        [When("I search the Grid for the cloned Service Request")]
        public void SearchGridForClonedServiceRequest()
        {
            _uiContext.XrmApp.Grid.Search("Copy: " + _uiContext.FilledFields["Title"]);
            _uiContext.XrmApp.ThinkTime(1000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When("I search the Grid for the created Service Request")]
        public void SearchGridForCreatedServiceRequest()
        {
            _uiContext.XrmApp.Grid.Search(_uiContext.FilledFields["Title"].ToString());
        }

        [StepDefinition("I open the Grid record at index (.*)")]
        public void IOpenTheRecordAtIndex(int index)
        {
            _uiContext.XrmApp.Grid.OpenRecord(index);
        }

        [StepDefinition("I open the SubGrid '(.*)' record at index (.*)")]
        public void IOpenTheRecordAtIndex(string subgrid, int index)
        {
            _uiContext.XrmApp.Entity.SubGrid.OpenSubGridRecord(subgrid, index);
        }

        [Then("I verify that the Grid has at least (.*) records")]
        public void VerifyGridCount(int count)
        {
            var gridItems = _uiContext.XrmApp.Grid.GetGridItems();
            Assert.IsTrue(gridItems.Count >= count, "Expected Grid to have minimum {0} records, but it has {1}", count, gridItems.Count);
        }

        [Then("I verify that the SubGrid '(.*)' has at least (.*) records")]
        public void VerifyGridCount(string subgrid, int count)
        {
            var gridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(subgrid);
            Assert.IsTrue(gridItems.Count >= count, "Expected Grid to have minimum {0} records, but it has {1}", count, gridItems.Count);
        }

        [Then("I verify that the Grid contains records with the following properties")]
        public void VerifyGridProperties(Table table)
        {
            var gridItems = _uiContext.XrmApp.Grid.GetGridItems();
            VerifyGridProperties(table, gridItems);            
        }

        [Then("I verify that the Grid record at index (.*) contains the following properties")]
        public void VerifyGridProperties(int index, Table table)
        {
            var gridItems = _uiContext.XrmApp.Grid.GetGridItems();
            Assert.IsTrue(index < gridItems.Count, "Grid record Index is out of range");
            VerifyGridProperties(table, new List<GridItem>() { gridItems[index] });
        }

        [Then("I verify that the SubGrid '(.*)' contains records with the following properties")]
        public void VerifyGridProperties(string subGrid, Table table)
        {
            VerifyGridProperties(table, _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(subGrid));
        }

        [StepDefinition("I open the first Grid record with one of the following properties")]
        public void OpenGridRecord(Table table)
        {
            OpenGridRecord(table, _uiContext.XrmApp.Grid.GetGridItems());            
        }

        [StepDefinition("I open the first SubGrid '(.*)' record with one of the following properties")]
        public void OpenGridRecord(string subGrid, Table table)
        {
            OpenGridRecord(table, _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(subGrid));
        }

        [Then("I verify that I can export the Grid to Excel")]
        public void CanExportGridToExcel()
        {
            BrowserFileHelper fileHelper = new BrowserFileHelper();
            int oldFileCount = fileHelper.GetDownloadFolderFileCount();

            _uiContext.XrmApp.CommandBar.ClickCommand("Export to Excel");
            _uiContext.XrmApp.ThinkTime(5000);

            int newFileCount = fileHelper.GetDownloadFolderFileCount();
            Assert.AreNotEqual(oldFileCount, newFileCount, "Download folder file count did not change after clicking Export to Excel");
        }

        [StepDefinition("I switch the Grid view to '(.*)'")]
        public void SwitchGridView(string viewName)
        {
            _uiContext.XrmApp.Grid.SwitchView(viewName);
        }

        private void VerifyGridProperties(Table table, List<GridItem> gridItems)
        {
            Assert.IsTrue(gridItems.Count > 0, "The Grid is empty");

            var headers = GetFullTableHeaderNames(table, gridItems[0]);

            StringTransformer transformer = new StringTransformer();
            for (int r = 0; r < table.Rows.Count; r++)
            {
                var items = gridItems;
                foreach (string header in headers.Keys)
                {
                    string expectedValue = transformer.Transform(table.Rows[r][header].ToString());
                    items = items.FindAll(x => x.GetAttribute<string>(headers[header]).Contains(expectedValue));
                    Assert.IsNotNull(items, "Could not find Grid item from row {0}", r);
                    Assert.IsTrue(items.Count > 0, "Could not find Grid item from row {0}", r);
                }
            }
        }

        private void OpenGridRecord(Table table, List<GridItem> gridItems)
        {
            Assert.IsTrue(gridItems.Count > 0, "The Grid is empty");

            var headers = GetFullTableHeaderNames(table, gridItems[0]);

            StringTransformer transformer = new StringTransformer();
            for (int r = 0; r < table.Rows.Count; r++)
            {
                for (int i = 0; i < gridItems.Count; i++)
                {
                    bool isMatch = true;
                    foreach (string header in headers.Keys)
                    {
                        string expectedValue = transformer.Transform(table.Rows[r][header].ToString());
                        if (!gridItems[i].GetAttribute<string>(headers[header]).Contains(expectedValue))
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        _uiContext.XrmApp.Grid.OpenRecord(i);
                        return;
                    }
                }
            }
            Assert.Fail("Could not find a Grid record to open with the properties specified");
        }

        private Dictionary<string,string> GetFullTableHeaderNames(Table table, GridItem gridItem)
        {
            // Get header names from the SpecFlow table
            var tableHeaderNames = table.Header.ToArray();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            // Match the Specflow header names to the actual header names
            for (int i = 0; i < tableHeaderNames.Length; i++)
            {
                var key = tableHeaderNames[i];
                var value = gridItem.Attributes.Keys.FirstOrDefault(x => x.Contains(key));
                Assert.IsFalse(string.IsNullOrEmpty(value), "Could not find a grid header that contains {0}", key);
                headers.Add(key, value);
            }

            return headers;
        }
    }
}
