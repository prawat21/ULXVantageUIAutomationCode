using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ServiceRequestNotificationSteps
    {
        private readonly UIContext _uiContext;

        private const string _newActivityButton = "//button[contains(@title,'New Activity')]";
        private const string _emailButton = "//button[contains(@title,'Email')]";

        private const string _emailBodyXPath = "//body[contains(@class,'cke_editable')]";
        private const string _designerFrameXPath = "//iframe[@title='Designer']";
        private const string _editorFrameXPath = "//iframe[contains(@class,'cke_wysiwyg_frame')]";

        private const string _signatureDocumentInput = "//div[contains(@data-id,'ulx_documentstosign')]//following::input";
        private const string _signatureDocumentCheckBox = "//div[@role='checkbox']";
        private const string _signatureDocumentButton = "//div[contains(@data-id,'ulx_documentstosign')]//following::button";

        public ServiceRequestNotificationSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I select the Service Request Activities tab")]
        public void SelectServiceRequestActivitiesTab()
        {
            _uiContext.XrmApp.Entity.SelectTab("Related", "Activities");
        }

        [When("I add an Email to the Service Request with the following properties")]
        public void AddAnEmailToAServiceRequest(Table table)
        {
            var newActivityButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_newActivityButton));
            newActivityButton.Click();
            _uiContext.XrmApp.ThinkTime(500);
            var emailButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_emailButton));
            emailButton.Click();
            _uiContext.XrmApp.ThinkTime(500);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.EntityFillOutFields(keyValues, true);
            _uiContext.XrmApp.ThinkTime(500);
        }

        [When("I send the Email")]
        public void ClickSendButton()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Send");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When("I set the email body to '(.*)'")]
        public void SetEmailBodyTo(string body)
        {
            var designerFrame = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_designerFrameXPath));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(designerFrame);

            var editorFrame = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_editorFrameXPath));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(editorFrame);

            var articleBody = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_emailBodyXPath));
            articleBody.SendKeys(body);
            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
            _uiContext.XrmApp.ThinkTime(500);
        }

        [Then("I verify that I can click Initiate BriefBox")]
        public void ClickInitiateBriefbox()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Initiate BriefBox");
        }

        [When("I click Request Signature")]
        public void ClickRequestSignature()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Request Signature");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [When("I select Signature Document '(.*)'")]
        public void SelectSignatureDocument(string document)
        {
            var documentInput = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_signatureDocumentInput));
            documentInput.SendKeys(document);

            var documentCheckBox = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_signatureDocumentCheckBox));
            documentCheckBox.Click();

            _uiContext.XrmApp.ThinkTime(500);
            var documentOkButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_signatureDocumentButton));
            documentOkButton.Click();            
        }

        [When("I add a Signee to the Signature Request with the following properties")]
        public void AddASigneeSignatureRequest(Table table)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand("SigneesSubGrid", "New Signee");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.QuickCreateFillOutFields(keyValues);

            _uiContext.XrmApp.QuickCreate.Save();
        }

        [When("I submit the Signature Request")]
        public void ClickSubmit()
        {
            _uiContext.XrmApp.CommandBar.ClickCommand("Submit");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }
    }
}
