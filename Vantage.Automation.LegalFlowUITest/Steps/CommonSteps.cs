using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly UIContext _uiContext;

        public CommonSteps(UIContext context)
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

        [StepDefinition("I click the Browser back button")]
        public void NavigateBack()
        {
            _uiContext.WebClient.Browser.Driver.Navigate().Back();
        }

        [StepDefinition("I click the Browser forward button")]
        public void NavigateForward()
        {
            _uiContext.WebClient.Browser.Driver.Navigate().Forward();
        }

        [StepDefinition("I save the Browser Url")]
        public void SaveUrl()
        {
            _uiContext.SavedUrl = _uiContext.WebClient.Browser.Driver.Url;
        }

        [StepDefinition("I navigate to the saved Browser Url")]
        public void GoToSavedUrl()
        {
            Assert.IsFalse(string.IsNullOrEmpty(_uiContext.SavedUrl), "Url has not been saved first");
            _uiContext.WebClient.Browser.Driver.Url = _uiContext.SavedUrl;
        }

        [StepDefinition("I set the Browser zoom level to (.*)")]
        public void SetZoomLevel(int level)
        {
            _uiContext.WebClient.Browser.Driver.ExecuteScript($"document.body.style.zoom='{level}%'");
            _uiContext.WebClient.Browser.Driver.ClearFocus();
        }

        [StepDefinition("I wait for the Browser to go Idle")]
        public void WaitUntilIdle()
        {
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I wait for the Browser to go Idle and wait (.*) seconds")]
        public void WaitUntilIdle(int seconds)
        {
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.XrmApp.ThinkTime(System.TimeSpan.FromSeconds(seconds));
        }

        [StepDefinition("I ignore the duplicate popup")]
        public void IgnoreDuplicatePopup()
        {
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.XrmApp.Dialogs.DuplicateDetection(true);
            // Sometimes a modal dialog will also pop up
            _uiContext.XrmApp.Dialogs.CloseModalDialog();
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I fill out the following fields")]
        public void FillOutFields(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFillOutFields(keyValues, true);
        }

        [StepDefinition("I fill out the following fields if they are present")]
        public void FillOutFieldsIfPresent(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFillOutFields(keyValues, false);
        }

        [StepDefinition("I verify the following field values are")]
        public void VerifyTheFollowingFieldValuesAre(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityVerifyFieldValues(keyValues, false);
        }

        [StepDefinition("I verify the following field tooltips contain")]
        public void VerifyTheFollowingFieldToolTipsContain(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFieldToolTipsContain(keyValues);
        }

        [StepDefinition("I verify the following field values contain")]
        public void VerifyTheFollowingFieldValuesContain(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityVerifyFieldValues(keyValues, true);
        }

        [StepDefinition("I verify that the following fields are present")]
        public void VerifyFieldsArePresent(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h].ToLower() == "true");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFieldsArePresent(keyValues);
        }

        [StepDefinition("I verify that the following fields are required")]
        public void VerifyFieldsAreMarkedRequired(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h].ToLower() == "true");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFieldsAreRequired(keyValues);
        }

        [StepDefinition("I verify CommandBar button '(.*)' is present")]
        public void CommandBarButtonIsPresent(string button)
        {
            Assert.IsTrue(_uiContext.XrmApp.CommandBar.GetCommandValues().Value.Contains(button), "CommandBar Button '{0}' is not present", button);
        }

        [StepDefinition("I verify CommandBar button '(.*)' is not present")]
        public void CommandBarButtonIsNotPresent(string button)
        {
            Assert.IsFalse(_uiContext.XrmApp.CommandBar.GetCommandValues().Value.Contains(button), "CommandBar Button '{0}' is present but should not be", button);
        }
    }
}