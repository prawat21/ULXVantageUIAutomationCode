using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;
using Vantage.Automation.VaultUITest.Models;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementPackageGeneralTabSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly FieldGetter _fieldGetter;
        private readonly FieldSetter _fieldSetter;
        private const string SourceSystemAccountLabel = "Source System Account";
        private const string VaultAccountLabel = "Vault Account Value";

        public AgreementPackageGeneralTabSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _fieldGetter = new FieldGetter(_uiContext);
            _fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
        }

        [When(@"I save agreement package info from General page as '(.*)'")]
        public void SaveAgreementPackageInfoFromGeneralPageAs(string key)
        {
            var packageModel = new AgreementPackageGeneralModel
            {
                Name = _fieldGetter.GetValue("Name"),
                DataRetentionYears = int.Parse(_fieldGetter.GetValue("Data Retention Years")),
            };
            ScenarioContext.Add(key, packageModel);
        }

        [When(@"I fill in the following fields on Agreement Package General page:")]
        public void FillInTheFollowingFieldsOnAgreementPackageGeneralPage(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            _fieldSetter.FillInFields(keyValues);
        }

        [Then(@"Source System Account value is populated on Agreement package general tab")]
        public void SourceSystemAccountValueIsPopulatedOnAgreementPackageGeneralTab()
        {
            var fieldValue = _fieldGetter.GetValue(SourceSystemAccountLabel, skipReps: true);
            Assert.IsNotNull(fieldValue, "Source System Account value is not present");
        }

        [Then(@"Source System Account value is '(readonly|editable)' on Agreement package general tab")]
        public void SourceSystemAccountValueIsOnAgreementPackageGeneralTab(string state)
        {
            var boolState = state == "readonly";
            var fieldState = _fieldGetter.IsFieldReadonly(SourceSystemAccountLabel, "readonly");
            Assert.AreEqual(boolState, fieldState, "Source System Account value state is incorrect");
        }

        [Then(@"Vault Account value is equal to Source System Account value on Agreement package general tab")]
        public void VaultAccountValueIsEqualToSourceSystemAccountValueOnAgreementPackageGeneralTab()
        {
            var vaultValue = _fieldGetter.GetValue(VaultAccountLabel, skipReps: true);
            var sourceValue = _fieldGetter.GetValue(SourceSystemAccountLabel, skipReps: true);
            Assert.AreEqual(sourceValue, vaultValue, "Vault and source accounts are different");
        }

        [Then(@"Vault Account value is '(readonly|editable)' on Agreement package general tab")]
        public void VaultAccountValueIsOnAgreementPackageGeneralTab(string state)
        {
            var boolState = state == "readonly";
            var fieldState = _fieldGetter.IsFieldReadonly(VaultAccountLabel);
            Assert.AreEqual(boolState, fieldState, "Vault Account value state is incorrect");
        }

        [Then(@"The following fields are present on Agreement Package form:")]
        public void FollowingFieldsArePresentOnAgreementPackageForm(IList<string> fields)
        {
            var issuesList = new List<string>();
            foreach(var field in fields)
            {
                issuesList.AddIfFalse(_fieldGetter.TryGetValue(field, out _), $"{field} field is not present");
            }

            AssertionUtils.IsEmpty(issuesList, "Some agreement fields are not present");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [Then(@"The following fields are absent on Agreement Package form:")]
        public void FollowingFieldsAreAbsentOnAgreementPackageForm(IList<string> fields)
        {
            var issuesList = new List<string>();
            foreach (var field in fields)
            {
                issuesList.AddIfTrue(_fieldGetter.TryGetValue(field, out _), $"{field} field is present");
            }

            AssertionUtils.IsEmpty(issuesList, "Some agreement fields are present");
        }

        [Then(@"Revised Dates on General Agreement Package page are equal to saved '(.*)'")]
        public void RevisedDatesOnGeneralAgreementPackagePageAreEqualToSaved(string key)
        {
            var actualDates = new RevisedDatesModel
            {
                RevisedEffectiveDate = _fieldGetter.GetValue("Revised Effective Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedExpirationDate = _fieldGetter.GetValue("Revised Expiration Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedServiceStartDate = _fieldGetter.GetValue("Revised Service Start Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField)
            };
            var savedDates = ScenarioContext.Get<RevisedDatesModel>(key);
            Assert.AreEqual(savedDates, actualDates, "Revised dates on agreement package page are incorrect");
        }

        [Then(@"Revised Dates on General Agreement Package page are not equal to saved '(.*)'")]
        public void RevisedDatesOnGeneralAgreementPackagePageAreNotEqualToSaved(string key)
        {
            var actualDates = new RevisedDatesModel
            {
                RevisedEffectiveDate = _fieldGetter.GetValue("Revised Effective Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedExpirationDate = _fieldGetter.GetValue("Revised Expiration Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField),
                RevisedServiceStartDate = _fieldGetter.GetValue("Revised Service Start Date")?.GetDatetimeFromString(ProjectSpecificConstants.DatetimeFormatForDateField)
            };
            var savedDates = ScenarioContext.Get<RevisedDatesModel>(key);
            Assert.AreNotEqual(savedDates, actualDates, "Revised dates on agreement package page are equal, when should not be");
        }        
    }
}
