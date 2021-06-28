using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;
using Vantage.Automation.VaultUITest.Models;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementPageSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly FieldGetter _fieldGetter;
        private readonly FieldSetter _fieldSetter;

        public AgreementPageSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _fieldGetter = new FieldGetter(_uiContext);
            _fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
        }

        private Dictionary<string, long> fileToSize => new Dictionary<string, long>
        {
            { "acceptable", 20971520 },
            { "unacceptable", 26214400 }
        };

        [When(@"I upload '(acceptable|unacceptable)' file to Agreement Files")]
        public void UploadFileToAgreementFiles(string fileType)
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
            FileUtility.CreateDirectoryIfNotExists(directory);
            var path = Path.Combine(directory, $"{fileType}ToUpload{DateTime.Now.Ticks}.txt");
            FileUtility.CreateFileOfSize(path, fileToSize[fileType]);
            _uiContext.WebClient.Browser.Driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(path);
            var spinnerLocator = By.XPath("//*[contains(@class, 'uploadDivs')]//*[@data-icon='spinner']");
            _uiContext.WebClient.Browser.Driver.WaitUntilVisible(spinnerLocator);
            _uiContext.WebClient.Browser.Driver.WaitUntilNotVisible(spinnerLocator, TimeSpan.FromMinutes(10));
            _uiContext.XrmApp.ThinkTime(3000);
        }

        [When(@"I fill in the following fields on General Agreement page:")]
        public void FillInTheFollowingFieldsOnGeneralAgreementPage(Dictionary<string, string> keyValues)
        {
            _fieldSetter.FillInFields(keyValues);
        }

        [When(@"I toggle Attested field value on General page")]
        public void ToggleAttestedFieldValueOnGeneralPage()
        {
            var fieldName = "Attested";
            var fieldValue = _fieldGetter.GetValue(fieldName);
            bool value = !"true yes".Contains(fieldValue.ToLower());
            _fieldSetter.FillInField(fieldName, value.ToString());
        }

        [When(@"I save Agreement Name from General page as '(.*)'")]
        public void SaveAgreementNameFromGeneralPageAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Agreement Name"));
        }

        [When(@"I save Attested field value from General page as '(.*)'")]
        public void SaveAttestedFieldValueFromGeneralPageAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Attested"));
        }

        [When(@"I save Attested Yes or No field value from General page as '(.*)'")]
        public void SaveAttestedFieldYesNoValueFromGeneralPageAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Attested") == "True" ? "Yes" : "No");
        }

        [When(@"I save Revised Dates from General Agreement page as '(.*)'")]
        public void SaveRevisedDatesFromGeneralAgreementPageAs(string key)
        {
            var dates = GetRevisedDates();
            ScenarioContext.Add(key, dates);
        }

        [When(@"I update Agreement Package field value by searching for '(.*)' from Agreement page and save new value as '(.*)'")]
        public void UpdateAgreementPackageFieldValueBySearchingForFromAgreementPageAndSaveNewValueAs(string searchCriteria, string key)
        {
            var currentValue = _fieldGetter.GetValue("Agreement Package");
            _fieldSetter.FillInLookupValueWithoutConfirmation("Agreement Package", searchCriteria);
            var searchResults = _fieldGetter.GetLookupResults("Agreement Package", searchCriteria).Select(x => Regex.Split(x, @"\r\n")[0]).ToList();
            searchResults.Remove(currentValue);
            var newValue = searchResults.First();
            _fieldSetter.FillInField("Agreement Package", newValue);
            ScenarioContext.Add(key, newValue);
        }

        [When(@"I click New Agreement Page link from Agreement Package field dropdown on Agreement page")]
        public void ClickNewAgreementPageLinkFromAgreementPackageFieldDropdownOnAgreementPage()
        {
            _fieldSetter.FillInLookupValueWithoutConfirmation("Agreement Package", "q");
            _uiContext.XrmApp.Lookup.New();
        }

        [Then(@"New '(acceptable|unacceptable)' file is '(present|absent)' on Agreement Files grid")]
        public void FileIsNotPresentOnAgreementFilesGrid(string fileType, string state)
        {
            var boolState = state == "present";
            var currentDateTime = DateTime.Now;
            var item = _uiContext.XrmApp.Grid.GetGridItems()
                .FirstOrDefault(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("name"))].ToString().Contains($"{fileType}ToUpload")
                                && (currentDateTime - x.Attributes[x.Attributes.Keys.First(y => y.Contains("createdon"))].ToString().GetDatetimeFromString()).Minutes < 10);
            Assert.AreEqual(boolState, item != null, $"File {fileType} is not {state}");
        }

        [Then(@"The following date fields are present on Agreement form:")]
        public void FollowingDateFieldsArePresentOnAgreementForm(IList<string> fieldNames)
        {
            var issuesList = new List<string>();
            foreach(var fieldName in fieldNames)
            {
                DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
                var fieldType = fieldHelper.GetFieldType(fieldName);
                issuesList.AddIfFalse(fieldType == FieldType.DateTime, $"{fieldName} field type is not DateTime. Actual type is {fieldType}");
            }

            AssertionUtils.IsEmpty(issuesList, "Some of date fields are incorrect");
        }

        [Then(@"The following fields are present on Agreement form:")]
        public void FollowingFieldsArePresentOnAgreementForm(IList<string> fieldNames)
        {
            var issuesList = new List<string>();

            foreach (var field in fieldNames)
            {
                issuesList.AddIfFalse(_fieldGetter.TryGetValue(field, out _), $"{field} field is not present");
            }
            AssertionUtils.IsEmpty(issuesList, "Some of the fields are not present");
        }

        [Then(@"Revised Dates on General Agreement page are equal to saved '(.*)'")]
        public void RevisedDatesOnGeneralAgreementPageAreEqualToSaved(string key)
        {
            var actualDates = GetRevisedDates();
            var savedDates = ScenarioContext.Get<RevisedDatesModel>(key);
            Assert.AreEqual(savedDates, actualDates, "Revised dates on agreement package page are incorrect");
        }

        [Then(@"Revised Dates on General Agreement page are not equal to saved '(.*)'")]
        public void RevisedDatesOnGeneralAgreementPageAreNotEqualToSaved(string key)
        {
            var actualDates = GetRevisedDates();
            var savedDates = ScenarioContext.Get<RevisedDatesModel>(key);
            Assert.AreNotEqual(savedDates, actualDates, "Revised dates on agreement package page are equal, when should not be");
        }

        [Then(@"Agreement Package field value on Agreement page is equal to saved '(.*)'")]
        public void AgreementPackageFieldValueOnAgreementPageIsEqualToSaved(string key)
        {
            var savedValue = ScenarioContext.Get<string>(key);
            var actualValue = _fieldGetter.GetValue("Agreement Package", skipReps: true);
            Assert.AreEqual(savedValue, actualValue, "Agreement package field value is incorrect");
        }

        [Then(@"'(.*)' field is populated on Agreement page")]
        public void FieldIsPopulatedOnGeneralAgreementPage(string field)
        {
            var actualValue = _fieldGetter.GetValue(field);
            Assert.AreNotEqual("", actualValue, $"{field} value is empty");
        }

        [Then(@"'(.*)' field is empty on Agreement page")]
        public void FieldIsNotPopulatedOnAgreementPage(string field)
        {
            var actualValue = _fieldGetter.GetValue(field);
            Assert.AreEqual("---", actualValue, $"{field} value is not empty.");
        }

        [Then(@"the Counterparty Country field contains values")]
        public void CounterPartyCountryContainsFields()
        {
            string xPathForControl = "//div[contains(@data-id,'ulx_counterpartycountry')]//span[@role='combobox']";
            var control = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(xPathForControl));
            control.Click();
            string xPathForOptions = "//ul[@class='select2-results__options']//li";
            var countryOptions = _uiContext.WebClient.Browser.Driver.FindElements(By.XPath(xPathForOptions));

            Assert.IsTrue(countryOptions.Count > 0, "Counterparty Country list elements were not found");
        }

        [When(@"I save agreement name from Agreement form as '(.*)'")]
        public void SaveAgreementNameFromAgreement(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Agreement Name", skipReps: true));
        }

        [When(@"I save Signature ID from Agreement form as '(.*)'")]
        public void SaveSignatureIDFromAgreement(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Signature ID"));
        }

        private RevisedDatesModel GetRevisedDates()
        {
            return new RevisedDatesModel
            {
                RevisedEffectiveDate = _fieldGetter.GetValue("Revised Effective Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedExpirationDate = _fieldGetter.GetValue("Revised Expiration Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedServiceStartDate = _fieldGetter.GetValue("Revised Service Start Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField)
            };
        }
    }
}
