using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class RevertAuditSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        private const string _XpathAuditFrameId = "//*[@id='FullPageWebResource']";
        private const string _XpathAuditSelectControl = "//select[@id='SystemUser']";
        private const string _XpathAuditSearchButton = "//button[@id='SearchButton']";

        private const string _XpathAuditTableRows = "//tr[@role='row' and @aria-rowindex]";

        private const string _XpathAuditTableExpandButton = "//div[@class='dx-datagrid-group-closed']";
        private const string _XpathAuditTableCheckBox = "//span[@class='dx-checkbox-icon']";
        private const string _XpathAuditTableUndoButton = "//span[text()='Undo Audit (All Fields)']";

        public RevertAuditSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open the Revert Audit area")]
        public void OpenRevertAudit()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Settings", "Revert Audit");
        }

        [When(@"I search Revert Audit for user '(.*)'")]
        public void SearchRevertAudit(string user)
        {
            user = new StringTransformer().Transform(user);

            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            _uiContext.XrmApp.ThinkTime(1000);

            var selectElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditSelectControl));
            var selectElementControl = new SelectElement(selectElement);

            selectElementControl.SelectByText(user, true);

            var searchButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditSearchButton));
            searchButton.Click();

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [Then(@"I verify that the Revert Audit table has at least (.*) records")]
        public void VerifyRevertAuditTableRecordCount(int expectedRecords)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditTableRows));
            var records = _uiContext.WebClient.Browser.Driver.FindElements(By.XPath(_XpathAuditTableRows));

            Assert.IsTrue(records.Count >= expectedRecords, "Expected minimum {0} records but found {1}", expectedRecords, records.Count);

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [Then(@"I verify that the Revert Audit table contains (.*) records with the following values:")]
        public void VerifyRevertAuditTableRecord(int expectedMatches, IList<string> values)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditTableRows));
            var records = _uiContext.WebClient.Browser.Driver.FindElements(By.XPath(_XpathAuditTableRows));
            
            StringTransformer transformer = new StringTransformer();
            int foundMatches = 0;
            foreach (var record in records)
            {
                var tdElements = record.FindElements(By.TagName("td"));
                bool thisIsAMatch = true;
                foreach (string val in values)
                {
                    string expectedValue = transformer.Transform(val);
                    if (expectedValue.StartsWith("[") && expectedValue.Length > 2 &&
                        ScenarioContext.ContainsKey(expectedValue.Substring(1, expectedValue.Length - 2)))
                    {
                        expectedValue = ScenarioContext[expectedValue.Substring(1, expectedValue.Length - 2)].ToString();
                    }

                    if (!tdElements.Any(x => x.Text == expectedValue))
                    {
                        thisIsAMatch = false;
                        break;
                    }
                }
                if (thisIsAMatch)
                {
                    foundMatches++;
                }
            }
            Assert.AreEqual(expectedMatches, foundMatches, $"Did not find {expectedMatches} Revert Audit table records with the values specified");

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [When(@"I attempt to Undo Audit of record (.*) of the Revert Audit table")]
        public void UndoAuditOnRevertAuditTable(int recordToUndo)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditTableRows));
            var records = _uiContext.WebClient.Browser.Driver.FindElements(By.XPath(_XpathAuditTableRows));

            var expandButton = records[recordToUndo - 1].FindElement(By.XPath(_XpathAuditTableExpandButton));
            expandButton.Click();

            var checkBox = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathAuditTableCheckBox));
            checkBox.Click();

            var undoButton = _uiContext.WebClient.Browser.Driver.WaitUntilClickable(By.XPath(_XpathAuditTableUndoButton));
            undoButton.Click();

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();

            _uiContext.XrmApp.Dialogs.ConfirmationDialog(true);
            _uiContext.XrmApp.Dialogs.CloseModalDialog();

            _uiContext.WebClient.Browser.Driver.SwitchTo().DefaultContent();

        }
    }
}
