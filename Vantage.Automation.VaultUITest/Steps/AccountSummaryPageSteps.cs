using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;
using Vantage.Automation.VaultUITest.Models;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AccountSummaryPageSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly FieldGetter _fieldGetter;
        private readonly FieldSetter _fieldSetter;

        public AccountSummaryPageSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _fieldGetter = new FieldGetter(_uiContext);
            _fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
        }

        [When(@"I fill in the following fields on Account Summary page:")]
        public void FillInTheFollowingFieldsOnAccountSummaryPage(Dictionary<string, string> keyValues)
        {
            _fieldSetter.FillInFields(keyValues);
        }

        [Then(@"The following fields are present on Account Summary page:")]
        public void FollowingFieldsArePresentOnAccountSummaryPage(IList<string> fieldNames)
        {
            var issuesList = new List<string>();

            foreach (var field in fieldNames)
            {
                issuesList.AddIfFalse(_fieldGetter.TryGetValue(field, out _), $"{field} field is not present");
            }
            AssertionUtils.IsEmpty(issuesList, "Some of the fields are not present");
        }

        [When(@"I save account info from Account Summary page as '(.*)'")]
        public void SaveAccountInfoFromAccountSummaryPageAs(string key)
        {
            var account = new AccountInformationModel
            {
                AccountName = _fieldGetter.GetValue("Account Name"),
                Phone = _fieldGetter.GetValue("Phone"),
                Country = _fieldGetter.GetValue("Country", skipReps: true)
            };
            ScenarioContext.Add(key, account);
        }

        [When(@"I save account name from Account Summary page as '(.*)'")]
        public void SaveAccountNameFromAccountSummaryPageAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Account Name"));
        }

        [Then(@"Legal Hold value is editable on Account Summary tab")]
        public void LegalHoldValueIsEditableOnAccountSummaryTab()
        {
            var initialValue = _fieldGetter.GetValue("Legal Hold");
            var newValue = initialValue == "True" ? "False" : "True";
            _fieldSetter.FillInField("Legal Hold", newValue);
            Assert.AreEqual(newValue, _fieldGetter.GetValue("Legal Hold"), "Legal hold field value is the same");
        }

        [Then(@"Subscription level lookup contains the following values on Account page:")]
        public void SubscriptionLevelLookupContainsTheFollowingValuesOnAccountPage(IList<string> options)
        {
            _fieldGetter.ClickLookupSearchIcon("Subscription Level");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            var lookupValues = _fieldGetter.GetLookupResults("Subscription Level", options.ElementAt(0));
            CollectionAssert.AreEquivalent(options.ToList(), lookupValues, $"Subscription levels do not match. Expected: {JsonConvert.SerializeObject(options)}, actual: {JsonConvert.SerializeObject(lookupValues)}");
        }
    }
}
