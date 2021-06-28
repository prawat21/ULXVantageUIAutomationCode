using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.CustomPageObjects;
using Vantage.Automation.VaultUITest.Helpers;
using Vantage.Automation.VaultUITest.Models;

namespace Vantage.Automation.VaultUITest.Steps
{
    public class AuditHistoryGridSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AuditHistoryGridSteps(UIContext uiContext, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = uiContext;
        }

        [Then(@"Account Audit History grid contains edition record from '(.*)' to '(.*)'")]
        public void AccountAuditHistoryGridContainsEditionRecordFromTo(string initialKey, string newKey)
        {
            var initialValues = ScenarioContext.Get<AccountInformationModel>(initialKey);
            var newValues = ScenarioContext.Get<AccountInformationModel>(newKey);
            var changes = GetChanges();
            var oldValue = new AccountInformationModel();
            var newValue = new AccountInformationModel();
            foreach (var change in changes)
            {
                switch(change.ChangedField)
                {
                    case "Account Name":
                        oldValue.AccountName = change.OldValue;
                        newValue.AccountName = change.NewValue;
                        break;
                    case "Main Phone":
                        oldValue.Phone = change.OldValue;
                        newValue.Phone = change.NewValue;
                        break;
                    case "Country":
                        oldValue.Country = change.OldValue;
                        newValue.Country = change.NewValue;
                        break;
                    default:
                        break;
                }
            }

            AssertChanges(initialValues, newValues, oldValue, newValue);
        }

        [Then(@"Agreement package Audit History grid contains edition record from '(.*)' to '(.*)'")]
        public void AgreementPackageAuditHistoryGridContainsEditionRecordFromTo(string initialKey, string newKey)
        {
            var initialValues = ScenarioContext.Get<AgreementPackageGeneralModel>(initialKey);
            var newValues = ScenarioContext.Get<AgreementPackageGeneralModel>(newKey);
            var changes = GetChanges();
            var oldValue = new AgreementPackageGeneralModel();
            var newValue = new AgreementPackageGeneralModel();
            foreach (var change in changes)
            {
                switch (change.ChangedField)
                {
                    case "Name":
                        oldValue.Name = change.OldValue;
                        newValue.Name = change.NewValue;
                        break;
                    case "Data Retention Years":
                        oldValue.DataRetentionYears = int.Parse(change.OldValue);
                        newValue.DataRetentionYears = int.Parse(change.NewValue);
                        break;
                    default:
                        break;
                }
            }

            AssertChanges(initialValues, newValues, oldValue, newValue);
        }

        [Then(@"Audit History grid contains records")]
        public void AuditHistoryGridContainsRecords()
        {
            _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.WebClient.Browser.Driver.WaitUntilClickable(By.XPath(Elements.Xpath[Reference.Entity.Form]),
                    TimeSpan.FromSeconds(30),
                    "CRM Record is Unavailable or not finished loading. Timeout Exceeded");
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(_uiContext.WebClient.Browser.Driver.FindElement(By.Id("audit_iframe")));
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            var actualHistoryRecords = new AuditHistoryTable(_uiContext).GetGridItems();
            _uiContext.WebClient.Browser.Driver.SwitchTo().DefaultContent();
            Assert.IsTrue(actualHistoryRecords.Count > 0, "Audit History grid does not contain records");
        }

        private List<AuditHistoryChangesModel> GetChanges()
        {
            _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
            _uiContext.XrmApp.ThinkTime(30000);
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(_uiContext.WebClient.Browser.Driver.FindElement(By.Id("audit_iframe")));
            var actualHistoryRecords = new AuditHistoryTable(_uiContext).GetGridItems();
            _uiContext.WebClient.Browser.Driver.SwitchTo().DefaultContent();
            var currentDateTime = DateTime.Now;
            var updateRecord = actualHistoryRecords.FirstOrDefault(x => (currentDateTime - x["Changed Date"].ToString().GetDatetimeFromString()).Minutes < 3
                && x["Event"].ToString() == "Update");
            Assert.IsNotNull(updateRecord, $"New update record is not found. Present records: {JsonConvert.SerializeObject(actualHistoryRecords)}");
            
            var changedFieldNames = (IList<string>)updateRecord["Changed Field"];
            var changedFieldOldValues = (IList<string>)updateRecord["Old Value"];
            var changedFieldNewValues = (IList<string>)updateRecord["New Value"];
            var changes = new List<AuditHistoryChangesModel>();
            for (var i = 0; i < changedFieldNames.Count; i++)
            {
                changes.Add(new AuditHistoryChangesModel
                {
                    ChangedField = changedFieldNames[i],
                    OldValue = changedFieldOldValues[i],
                    NewValue = changedFieldNewValues[i]
                });
            }

            return changes;
        }

        private void AssertChanges<T>(T initialValues, T newValues, T historyOldValues, T historyNewValues) where T: BaseModel
        {
            var issuesList = new List<string>();
            var diffProps = initialValues.GetType().GetProperties().Where(x =>
            {
                var newPropertyValue = newValues.GetType().GetProperty(x.Name).GetValue(newValues);
                var initialPropertyValue = x.GetValue(initialValues);
                return !Equals(newPropertyValue, initialPropertyValue);
            });
            foreach (var prop in diffProps)
            {
                var initialValue = prop.GetValue(initialValues);
                var newValue = prop.GetValue(newValues);
                var historyOldValue = prop.GetValue(historyOldValues);
                var historyNewValue = prop.GetValue(historyNewValues);
                if (!(Equals(initialValue, historyOldValue) && Equals(newValue, historyNewValue)))
                {
                    issuesList.Add($"{prop.Name} values are incorrect. Expected change from {initialValue} to {newValue}, but was from {historyOldValue} to {historyNewValue}");
                }
            }

            AssertionUtils.IsEmpty(issuesList, "There are issues in changes history");
        }
    }
}
