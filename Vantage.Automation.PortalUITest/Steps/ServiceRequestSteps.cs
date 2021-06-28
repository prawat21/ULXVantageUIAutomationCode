using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Helpers;
using Vantage.Automation.PortalUITest.Pages;

namespace Vantage.Automation.PortalUITest.Steps
{
    [Binding]
    public class ServiceRequestSteps
    {
        private readonly UIContext _uiContext;
        public ServiceRequestSteps(UIContext context)
        {
            _uiContext = context;
        }

        [StepDefinition("I fill out the following fields")]
        public void FillOutFields(Table table)
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            srPage.WaitUntilPageIsLoaded();

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.FillOutFields(keyValues, true);
        }

        [StepDefinition("I verify the following field values are")]
        public void VerifyFieldValuesAre(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.VerifyFields(keyValues, true, false);
        }

        [StepDefinition("I verify the following field values contain")]
        public void VerifyFieldValuesContain(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.VerifyFields(keyValues, true, true);
        }

        [StepDefinition("I submit the Service Request")]
        public void ClickSubmit()
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            srPage.ClickSubmitButton();
        }

        [StepDefinition("I verify that the Service Request was submitted")]
        public void ClickGoToMyRequests()
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            srPage.WaitForGoToMyRequestsButton(false);
        }

        [StepDefinition("I verify that the Service Request is privileged")]
        public void VerifyPriviledged()
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            Assert.AreEqual(true, srPage.IsRequestPrivileged(), "The Service Request is not privileged");
        }

        [StepDefinition("I verify that the Service Request is not privileged")]
        public void VerifyNotPriviledge()
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            Assert.AreEqual(false, srPage.IsRequestPrivileged(), "The Service Request is privileged");
        }

        [StepDefinition("I attach a document to the Service Request")]
        public void AttachADocument()
        {
            ServiceRequestPage srPage = new ServiceRequestPage(_uiContext);
            srPage.AttachADocument();
        }
    }
}
