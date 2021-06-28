using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class GridSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public GridSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When("I open the grid record at index '(.*)'")]
        public void OpenTheRecordAtIndex(int index)
        {
            _uiContext.XrmApp.Grid.OpenRecord(index - 1);
            _uiContext.XrmApp.ThinkTime(1000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When(@"I select row \#'(.*)' from Grid")]
        public void SelectRowFromGrid(int index)
        {
            _uiContext.XrmApp.Grid.HighLightRecord(index - 1);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition(@"I search the Grid for saved key '(.*)'")]
        public void SearchForSavedAgreement(string key)
        {
            var savedName = ScenarioContext.Get<string>(key);
            _uiContext.XrmApp.Grid.Search(savedName);
        }

        [When(@"I search for entity with name '(.*)'")]
        public void SearchForEntityWithName(string name)
        {
            name = new StringTransformer().Transform(name);
            _uiContext.XrmApp.Grid.Search(name);
        }

        [When(@"I search for entity with saved name '(.*)'")]
        public void SearchForEntityWithSavedName(string name)
        {
            name = ScenarioContext.Get<string>(name);
            for (int retry = 0; retry < 3; retry++)
            {
                _uiContext.XrmApp.Grid.Search(name);
                if (_uiContext.XrmApp.Grid.GetGridItems().Count > 0)
                {
                    break;
                }
                _uiContext.XrmApp.ThinkTime(30000);
            }
        }

        [Then(@"Access Team Members Subgrid header label is null on Entity page")]
        public void GridHeaderLabelIsOnEntityPage()
        {
            string actualTitle;
            try
            {
                actualTitle = _uiContext.XrmApp.Entity.SubGrid.GetSubgridHeaderTitle("Confidential");
            }
            catch (NotFoundException ex)
            {
                Log.TestLog.Error(ex, "Confidential subgrid is not present, trying to get Access_Team_Members");
                actualTitle = _uiContext.XrmApp.Entity.SubGrid.GetSubgridHeaderTitle("Access_Team_Members");
            }
            Assert.IsNull(actualTitle, "Subgrid title is present when should not be");
        }

        [Then(@"'(.*)' Subgrid is not present on entity page")]
        public void SubgridIsNotPresentOnEntityPage(string subgridName)
        {
            var result = false;
            try
            {
                _uiContext.XrmApp.Entity.SubGrid.GetSubgridHeaderTitle(subgridName);
            }
            catch (NotFoundException ex)
            {
                Log.TestLog.Error(ex, $"{subgridName} subgrid is not present");
                result = true;
            }

            Assert.IsTrue(result, $"{subgridName} subgrid is present when should not be");
        }

        [Then("I verify that the SubGrid '(.*)' contains records with the following properties")]
        public void VerifySubGridProperties(string subGrid, Table table)
        {
            VerifyGridProperties(table, _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(subGrid));
        }

        [Then("I verify that the Grid contains records with the following properties")]
        public void VerifyGridProperties(Table table)
        {
            VerifyGridProperties(table, _uiContext.XrmApp.Grid.GetGridItems());
        }

        [Then("I verify that the SubGrid '(.*)' has at least (.*) records")]
        public void VerifySubGridCount(string subgrid, int count)
        {
            var gridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(subgrid);
            Assert.IsTrue(gridItems.Count >= count, "Expected Grid to have minimum {0} records, but it has {1}", count, gridItems.Count);
        }

        [Then("I verify that the Grid has at least (.*) records")]
        public void VerifyGridCount(int count)
        {
            var gridItems = _uiContext.XrmApp.Grid.GetGridItems();
            Assert.IsTrue(gridItems.Count >= count, "Expected Grid to have minimum {0} records, but it has {1}", count, gridItems.Count);
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
                    string expectedFieldValue = table.Rows[r][header].ToString();
                    if (expectedFieldValue.StartsWith("[") && expectedFieldValue.Length > 2 &&
                        ScenarioContext.ContainsKey(expectedFieldValue.Substring(1, expectedFieldValue.Length - 2)))
                    {
                        expectedFieldValue = ScenarioContext[expectedFieldValue.Substring(1, expectedFieldValue.Length - 2)].ToString();
                    }
                    else
                    {
                        expectedFieldValue = transformer.Transform(expectedFieldValue);
                    }

                    items = items.FindAll(x => x.GetAttribute<string>(headers[header]).Contains(expectedFieldValue));
                    Assert.IsNotNull(items, "Could not find Grid item from row {0}", r);
                    Assert.IsTrue(items.Count > 0, "Could not find Grid item from row {0}", r);
                }
            }
        }

        private Dictionary<string, string> GetFullTableHeaderNames(Table table, GridItem gridItem)
        {
            // Get header names from the SpecFlow table
            var tableHeaderNames = table.Header.ToArray();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            // Match the Specflow header names to the actual header names
            for (int i = 0; i < tableHeaderNames.Length; i++)
            {
                var key = tableHeaderNames[i];
                var value = gridItem.Attributes.Keys.FirstOrDefault(x => x.EndsWith(key));
                Assert.IsFalse(string.IsNullOrEmpty(value), "Could not find a grid header that contains '{0}'", key);
                headers.Add(key, value);
            }

            return headers;
        }
    }
}
