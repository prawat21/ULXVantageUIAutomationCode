using OpenQA.Selenium;
using Vantage.Automation.PortalUITest.Context;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Vantage.Automation.PortalUITest.Helpers;
using System.Threading;

namespace Vantage.Automation.PortalUITest.Pages
{
    public class ServiceRequestPage
    {
        private readonly UIContext _uiContext;

        private const string _cancelButton = "//span[@class='ulx-button-contents' and contains(text(),'Cancel')]/parent::button";
        private const string _submitRequestButton = "//span[@class='ulx-button-contents' and contains(text(),'Submit Request')]/parent::button";
        private const string _goToMyRequestsButton = "//span[@class='ulx-button-contents' and contains(text(),'Go to My Requests')]/parent::button";
        private const string _privilegedLabel = "//div[@class='privilegedIconWithTooltip']";

        private const string _attachmentsButton = "//span[@class='ulx-button-contents' and contains(text(),'Add Attachments')]/parent::button";
        private const string _attachmentsDragDropArea = "//div[@appdragdrop]";
        private const string _attachmentsSaveButton = "//span[@class='ulx-button-contents' and contains(text(),'Save')]/parent::button";


        public ServiceRequestPage(UIContext context)
        {
            _uiContext = context;
        }

        public bool WaitUntilPageIsLoaded()
        {
            var cancelButton = _uiContext.Driver.WaitUntilClickable(By.XPath(_cancelButton), ConfigHelper.DefaultTimeOutInSeconds);
            return cancelButton != null;
        }

        public void ClickCancelButton()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_cancelButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Cancel button not found");
            }
            element.Click();
        }

        public void ClickSubmitButton()
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_submitRequestButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Submit Request button not found");
            }
            element.Click();
        }

        public void WaitForGoToMyRequestsButton(bool click)
        {
            var element = _uiContext.Driver.WaitUntilClickable(By.XPath(_goToMyRequestsButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (element == null)
            {
                throw new NotFoundException("Go to My Requests button not found");
            }
            if (click)
            {
                element.Click();
            }
        }

        public bool IsRequestPrivileged()
        {
            var element = _uiContext.Driver.FindVisible(By.XPath(_privilegedLabel));
            return element != null;
        }

        public void AttachADocument()
        {
            var attachmentsButton = _uiContext.Driver.WaitUntilClickable(By.XPath(_attachmentsButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (attachmentsButton == null)
            {
                throw new NotFoundException("Add attachment button not found");
            }
            attachmentsButton.Click();

            var dragDropArea = _uiContext.Driver.WaitUntilAvailable(By.XPath(_attachmentsDragDropArea), ConfigHelper.DefaultTimeOutInSeconds);
            string filePath = new FileUtility().CreateFile("test.pdf", 1024);
            _uiContext.Attachments.Add(filePath);

            var fileDropHelper = new FileDropHelper(_uiContext);
            fileDropHelper.DropFile(filePath, dragDropArea, 0, 0);
            Thread.Sleep(1000);

            var saveButton = _uiContext.Driver.WaitUntilClickable(By.XPath(_attachmentsSaveButton), ConfigHelper.DefaultTimeOutInSeconds);
            if (saveButton == null)
            {
                throw new NotFoundException("Add attachment Save button not found");
            }
            saveButton.Click();
            Thread.Sleep(2000);
        }
    }
}
