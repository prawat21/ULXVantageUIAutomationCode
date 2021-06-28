using System;
using System.IO;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class EntityCreator
    {
        private readonly UIContext _uiContext;
        private readonly FieldSetter _fieldSetter;

        public EntityCreator(UIContext context, ScenarioContext scenarioContext)
        {
            _uiContext = context;
            _fieldSetter = new FieldSetter(_uiContext, scenarioContext);
        }

        public void GenerateAgreement(string agreementName, string accountName, string agreementPackage = null)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreements");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");

            _fieldSetter.FillInField("Agreement Name", agreementName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Vault Account", accountName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            if (!string.IsNullOrEmpty(agreementPackage))
            {
                _fieldSetter.FillInField("Agreement Package", agreementPackage);
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }

            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }
        public void GenerateAccount(string accountName, string subscriptionLevel, string parentAccount = null)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Accounts");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");

            _fieldSetter.FillInField("Account Name", accountName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Subscription Level", subscriptionLevel);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            if (!string.IsNullOrEmpty(parentAccount))
            {
                _fieldSetter.FillInField("Parent Account", parentAccount);
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }

            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        public void GenerateAgreementPackage(string agreementPackageName, string accountName)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreement Packages");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");

            _fieldSetter.FillInField("Name", agreementPackageName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Vault Account", accountName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Data Retention Years", "1");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        public void GenerateAgreementFile(string agreementFileName, string agreementPackageName, string agreementName, int numberFiles=0)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreement Files");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");

            _uiContext.XrmApp.Entity.SelectTab("Overview");

            _fieldSetter.FillInField("Agreement", agreementName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Name", agreementFileName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("File Type", "Ancillary Document");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _fieldSetter.FillInField("Agreement Package", agreementPackageName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            if (numberFiles > 0)
            {
                _uiContext.XrmApp.CommandBar.ClickCommand("Save");

                var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
                FileUtility.CreateDirectoryIfNotExists(directory);

                for (int i = 0; i < numberFiles; i++)
                {
                    var path = Path.Combine(directory, $"file_{DateTime.Now.Ticks}.txt");
                    FileUtility.CreateFileOfSize(path, 1024);
                    var fileInput = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath("//input[@type='file']"));
                    fileInput.SendKeys(path);
                    _uiContext.XrmApp.ThinkTime(5000);
                    _uiContext.WebClient.Browser.Driver.WaitForTransaction();
                    _uiContext.XrmApp.Entity.SelectTab("Overview");
                }
            }

            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        public void GenerateClause(string clauseName, string agreementName, string agreementPackageName, string agreementFileName)
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Clauses");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");

            _fieldSetter.FillInField("Clause Text", clauseName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _fieldSetter.FillInField("Agreement", agreementName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _fieldSetter.FillInField("Agreement Package", agreementPackageName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _fieldSetter.FillInField("Agreement File", agreementFileName);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _fieldSetter.FillInField("Clause Type", "Deliverable");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();

            _uiContext.XrmApp.CommandBar.ClickCommand("Save & Close");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }
    }
}
