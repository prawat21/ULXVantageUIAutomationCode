using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class CommonSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public CommonSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [StepDefinition("I close the browser")]
        public void CloseBrowser()
        {
            if (_uiContext.XrmApp != null)
            {
                _uiContext.XrmApp.Dispose();
                _uiContext.WebClient = null;
                _uiContext.XrmApp = null;
            }
        }

        [StepDefinition("I save the Browser Url")]
        public void SaveUrl()
        {
            ScenarioContext.Add("SavedBrowserUrl", _uiContext.WebClient.Browser.Driver.Url);
        }

        [StepDefinition("I navigate to the saved Browser Url")]
        public void GoToSavedUrl()
        {
            Assert.IsTrue(ScenarioContext.TryGetValue("SavedBrowserUrl", out string url), "Url has not been saved first");
            _uiContext.WebClient.Browser.Driver.Url = url;
        }

        [When(@"I open '(.*)' tab")]
        public void OpenTab(string tab)
        {
            _uiContext.XrmApp.Entity.SelectTab(tab);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When(@"I open '(.*)' -> '(.*)' subtab")]
        public void OpenTab(string tab, string subTab)
        {
            _uiContext.XrmApp.Entity.SelectTab(tab, subTab);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When("I click '(.*)' on Command Bar")]
        public void SaveEntity(string command)
        {
            _uiContext.XrmApp.CommandBar.ClickCommand(command);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When("I open Entity Audit History")]
        public void OpenEntityAuditHistory()
        {
            _uiContext.XrmApp.Entity.SelectTab("Related", "Audit History");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When(@"I close Error popup")]
        public void CloseErrorPopup()
        {
            _uiContext.XrmApp.Dialogs.CloseErrorDialog();
        }        

        [When("I wait '(.*)' seconds")]
        public void WaitSeconds(int seconds)
        {
            _uiContext.XrmApp.ThinkTime(TimeSpan.FromSeconds(seconds));
        }        

        [When(@"I open '(.*)' from left navigation menu")]
        public void OpenFromLeftNavigationMenu(string item)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea(item);
        }

        [Then(@"'(.*)' tab is '(present|absent)' on entity page")]
        public void TabIsPresentOnEntityPage(string tab, string state)
        {
            var result = true;
            var boolState = state == "present";
            try
            {
                _uiContext.XrmApp.Entity.SelectTab(tab);
            }
            catch(Exception)
            {
                result = false;
            }

            Assert.AreEqual(boolState, result, $"{tab} state is incorrect.");
        }

        [Then(@"Entity field '(.*)' is '(.*)'")]
        public void EntityFieldIs(string label, string value)
        {
            var fieldGetter = new FieldGetter(_uiContext);
            var fieldValue = fieldGetter.GetValue(label, skipReps: true);
            Assert.AreEqual(value, fieldValue, "Field value for label '{0}'", label);
        }

        [Then(@"Command bar contains the following actions:")]
        public void CommandBarContainsTheFollowingActions(IList<string> commands)
        {
            var actualCommands = _uiContext.XrmApp.CommandBar.GetCommandValues().Value;
            CollectionAssert.IsSubsetOf(commands.ToList(), actualCommands, $"Commands are not present. Expected: {JsonConvert.SerializeObject(commands)}, actual: {JsonConvert.SerializeObject(actualCommands)}");
        }

        [Then(@"Command bar contains only the following actions:")]
        public void CommandBarContainsOnlyTheFollowingActions(IList<string> commands)
        {
            var actualCommands = _uiContext.XrmApp.CommandBar.GetCommandValues().Value;
            CollectionAssert.AreEquivalent(commands.ToList(), actualCommands, $"Commands are not equivalent. Expected: {JsonConvert.SerializeObject(commands)}, actual: {JsonConvert.SerializeObject(actualCommands)}");
        }

        [Then(@"a header with name '(.*)' is present")]
        public void AHeaderWithNameIsPresent(string headerName)
        {
            string headerValue = _uiContext.XrmApp.Entity.GetHeaderContainerValue(headerName);
            if (string.IsNullOrEmpty(headerValue))
            { 
                Assert.Fail("Header with name '{0}' does not exist", headerName);
            }            
        }

        [Then(@"I verify field '(.*)' contains '(.*)'")]
        public void VerifyFieldContains(string fieldName, string contains)
        {
            var fieldGetter = new FieldGetter(_uiContext);
            Assert.IsTrue(fieldGetter.TryGetValue(fieldName, out string fieldValue) ,$"{fieldName} field is not present");

            Assert.IsTrue(fieldValue.Contains(contains), $"{fieldName} value '{fieldValue}' does not contain '{contains}'");
        }

        [StepDefinition("I wait for the Browser to go Idle")]
        public void WaitUntilIdle()
        {
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I verify the following field tooltips contain")]
        public void VerifyTheFollowingFieldToolTipsContain(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldGetter fieldGetter = new FieldGetter(_uiContext);
            fieldGetter.EntityFieldToolTipsContain(keyValues);
        }
    }
}
