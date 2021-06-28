using System;
using System.Collections.Generic;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ServiceRequestSteps
    {
        private readonly UIContext _uiContext;

        public ServiceRequestSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I go to create a new Service Request")]
        public void GoToCreateNewServiceRequest()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("My Workspace", "Service Requests");
            _uiContext.XrmApp.CommandBar.ClickCommand("Create Service Request");
        }

        [When("I go to My Assigned Service Requests")]
        public void GoToMyAssignedServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("My Workspace", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("My Assigned Service Requests");
        }

        [When("I go to Active Service Requests")]
        public void GoToActiveServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("My Workspace", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("*Active");
        }

        [When("I go to My Service Requests")]
        public void GoToMyServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("My Workspace", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("My SRs");
        }

        [When("I go to New Service Requests")]
        public void GoToNewServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("***New");
        }

        [When("I go to Closed Service Requests")]
        public void GoToClosedServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("*Closed");
        }

        [When("I go to All Service Requests")]
        public void GoToAllServiceRequests()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Service Requests");
            _uiContext.XrmApp.Grid.SwitchView("ALL");
        }

        [Then("I verify that there are at least (.*) Service Request Records")]
        public void GoToMyAssignedServiceRequests(int expectedCount)
        {
            var recordCount = _uiContext.XrmApp.Grid.GetGridItems().Count;
            Assert.IsTrue(recordCount >= expectedCount, "There are only {0} records present", recordCount);
        }

        [StepDefinition("I save the Service Request")]
        public void SaveServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Save");
        }

        [StepDefinition("I clone the Service Request")]
        public void CloneServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Clone");
        }

        [StepDefinition("I save and close the Service Request")]
        public void SaveAndCloseServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
        }

        [StepDefinition("I accept the Service Request")]
        public void AcceptServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Accept");
        }

        [StepDefinition("I refresh the Service Request")]
        public void RefreshServiceRequest()
        {
            try
            {
                _uiContext.XrmApp.CommandBar.ClickCommand("Refresh");
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }
            catch (InvalidOperationException)
            {
            }
        }

        [StepDefinition("I submit the Service Request")]
        public void SubmitServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Submit");
            _uiContext.XrmApp.Dialogs.ConfirmationDialog(true);
            _uiContext.XrmApp.Dialogs.CloseModalDialog();
        }

        [StepDefinition("I reject the Service Request")]
        public void RejectServiceRequest()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Reject");
            _uiContext.XrmApp.Dialogs.RejectionDialog(true);
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I reject the Service Request with Reason '(.*)'")]
        public void RejectServiceRequestReason(string reason)
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Reject");
            _uiContext.XrmApp.Dialogs.RejectionDialog(true, reason);
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I reject the Service Request with Other Reason '(.*)'")]
        public void RejectServiceRequestOtherReason(string otherReason)
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Reject");
            _uiContext.XrmApp.Dialogs.RejectionDialog(true, "Other", otherReason);
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I Confirm Completion of the Service Request")]
        public void ConfirmCompletion()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Confirm Completion");
            _uiContext.XrmApp.Dialogs.ConfirmCompletionDialog(true, "Accept");
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I verify Service Request cannot be submitted")]
        public void FailToSubmitServiceRequest()
        {
            List<string> commandValues = new List<string>();
            commandValues = _uiContext.XrmApp.CommandBar.GetCommandValues();
            Assert.IsFalse(commandValues.Contains("Submit"), "Serivice Request submitted with missing fields");
        }

        [Then("I verify that the Service Request has a Request ID")]
        public void ServiceRequestHasARequestId()
        {
            var requestId = _uiContext.XrmApp.Entity.GetHeaderContainerValue("Request ID");
            Assert.IsFalse(string.IsNullOrEmpty(requestId), "Service Request does not have a Request ID");
        }

        [Then("I verify that the Service Request owner is '(.*)'")]
        public void VerifyServiceRequesOwner(string expectedOwner)
        {
            var actualOwner = _uiContext.XrmApp.Entity.GetHeaderContainerValue("Assignee");
            Assert.IsFalse(string.IsNullOrEmpty(actualOwner), "Service Request does not have an Owner");

            StringTransformer transformer = new StringTransformer();
            expectedOwner = transformer.Transform(expectedOwner);
            Assert.IsTrue(actualOwner.Contains(expectedOwner), "Expected Service Request Owner {0} to contain {1}", actualOwner, expectedOwner);
        }

        [Then("I verify that the Service Request owner is not '(.*)'")]
        public void VerifyServiceRequesOwnerIsNot(string expectedOwner)
        {
            StringTransformer transformer = new StringTransformer();
            expectedOwner = transformer.Transform(expectedOwner);

            var actualOwner = _uiContext.XrmApp.Entity.GetHeaderContainerValue("Assignee");
            Assert.IsFalse(actualOwner.Contains(expectedOwner), "Service Request Owner {0} contains {1}", actualOwner, expectedOwner);
        }        

        [StepDefinition("I verify that the Service Request has been accepted")]
        public void VerifyServiceRequstHasBeenAccepted()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Active");
            string dateValue = "";
            try
            {
                dateValue = _uiContext.XrmApp.BusinessProcessFlow.GetValue(new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Accepted On", false, true));
            }
            catch (NullReferenceException)
            {
                Assert.Fail("Field 'Accepted On' was not found on 'Active' stage");
            }
            Assert.IsTrue(DateTime.TryParse(dateValue, out DateTime result), "Could not parse 'Accepted On' value--service request has not been accepted?");
            Assert.IsTrue(DateTime.UtcNow - DateTime.Parse(dateValue) < TimeSpan.FromDays(1), "The service request accepted on date '{0}' is not today", dateValue);

            string acceptedBy = "";
            try
            {
                acceptedBy = _uiContext.XrmApp.BusinessProcessFlow.GetValue(new LookupItem() { Name = new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Accepted By") });
            }
            catch (NullReferenceException)
            {
                Assert.Fail("Field 'Accepted By' was not found on 'Active' stage");
            }
            Assert.IsFalse(string.IsNullOrEmpty(acceptedBy), "The service request has not been accepted by");
        }

        [StepDefinition("I verify that the Service Request has been rejected")]
        public void VerifyServiceRequstHasBeenRejected()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Triage");
            var rejectReasonField = new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Reject Reason", false, true);
            var rejectReason = _uiContext.XrmApp.BusinessProcessFlow.GetValue(new OptionSet() { Name = rejectReasonField });
            Assert.IsFalse(string.IsNullOrEmpty(rejectReason), "The service request has not been rejected (or 'Reject Reason' field is not visible)");
        }

        [StepDefinition("I verify that the Service Request has been completed")]
        public void VerifyServiceRequstHasBeenCompleted()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Completed");
            var dateValue = _uiContext.XrmApp.BusinessProcessFlow.GetValue(new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Completed On"));
            Assert.IsTrue(DateTime.TryParse(dateValue, out DateTime result), "The service request has not been completed");
            Assert.IsTrue(DateTime.UtcNow - result < TimeSpan.FromDays(1), "The service request completed date is not today");
        }

        [StepDefinition("I assign the Service Request to '(.*)'")]
        public void AssignServiceRequestTo(string assignTo)
        {
            assignTo = new StringTransformer().Transform(assignTo);

            _uiContext.XrmApp.CommandBar.ClickCommand("Reassign");
            _uiContext.XrmApp.Dialogs.ReassignDialog(true, assignTo);
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I assign the Service Request to '(.*)' with reason '(.*)'")]
        public void AssignServiceRequestWithReason(string assignTo, string reason)
        {
            assignTo = new StringTransformer().Transform(assignTo);

            _uiContext.XrmApp.CommandBar.ClickCommand("Reassign");
            _uiContext.XrmApp.Dialogs.ReassignDialog(true, assignTo, reason);
            _uiContext.XrmApp.ThinkTime(5000);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [StepDefinition("I verify that I cannot assign the Service Request to '(.*)'")]
        public void CannotAssignServiceRequestTo(string assignTo)
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Assigned");
            var assigneeField = new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Asignee");

            assignTo = new StringTransformer().Transform(assignTo);
            try
            {
                _uiContext.XrmApp.BusinessProcessFlow.SetValue(new LookupItem() { Name = assigneeField, Value = assignTo });
                Assert.Fail("Was able to assign the Service Request to " + assignTo);
            }
            catch (Exception ex)
            {
                if (!ex.ToString().Contains("No Results Matching"))
                {
                    throw ex;
                }
            }
        }

        [StepDefinition("I attach a private document to the Service Request with name '(.*)' and size (.*)")]
        public void UploadFileAsPrivate(string fileName, int length)
        {
            UploadFile(fileName, length, false);
        }

        [StepDefinition("I attach a public document to the Service Request with name '(.*)' and size (.*)")]
        public void UploadFileAsPublic(string fileName, int length)
        {
            UploadFile(fileName, length, true);
        }

        private void UploadFile(string fileName, int length, bool markAsPublic)
        {
            IWebElement element = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath("//input[@type='file']"));
            if (element == null)
            {
                throw new NotFoundException("Did not find the file upload area");
            }

            FileUtility fileUtility = new FileUtility();
            string filePath = "";
            try
            {
                filePath = fileUtility.CreateFile(fileName, length);
                _uiContext.XrmApp.ThinkTime(1000);
                element.SendKeys(filePath);
                _uiContext.XrmApp.ThinkTime(5000);
            }
            finally
            {
                fileUtility.DeleteFile(filePath);
            }

            if (markAsPublic)
            {
                IWebElement publicButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath("//button[contains(@id,'Toggle')]"));
                if (publicButton != null)
                {
                    publicButton.Click();
                    _uiContext.XrmApp.ThinkTime(3000);
                }
            }
        }
    }
}
