using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class EntitySteps
    {
        private readonly UIContext _uiContext;

        private const string _warningNotificationXpath = "//span[@data-id='warningNotification' and contains(text(),'{0}')]";

        public EntitySteps(UIContext context)
        {
            _uiContext = context;
        }

        [StepDefinition("I verify that the Entity has an Id")]
        public void GetEntityId()
        {
            bool entityHasId = false;
            try
            {
                var guid = _uiContext.XrmApp.Entity.GetObjectId();
                entityHasId = true;
            }
            catch
            { }
            Assert.IsTrue(entityHasId, "the Entity does not have an Id");
        }

        [StepDefinition("I Save the Entity")]
        public void SaveEntity()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Save");
        }

        [StepDefinition("I Save And Close the Entity")]
        public void SaveAndCloseEntity()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
        }

        [StepDefinition("I Delete the Entity")]
        public void DeleteEntity()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Delete");
            _uiContext.XrmApp.Dialogs.ConfirmationDialog(true);
        }

        [Then("I verify that a warning notification containing text '(.*)' is present")]
        public void WarningNotificationIsPresent(string messageContains)
        {
             Assert.IsTrue(IsWarningNotificationPresent(messageContains),
                $"Did not find a notification with text {messageContains}");
        }

        [Then("I verify that a warning notification containing text '(.*)' is not present")]
        public void WarningNotificationIsNotPresent(string messageContains)
        {
            Assert.IsFalse(IsWarningNotificationPresent(messageContains),
                $"Found a notification containing text {messageContains}");
        }

        private bool IsWarningNotificationPresent(string messageContains)
        {
            try
            {
                _uiContext.WebClient.Browser.Driver.FindElement(By.XPath(string.Format(_warningNotificationXpath, messageContains)));
                return true;
            }
            catch (NoSuchElementException) { }
            return false;
        }
    }
}
