using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementsNavigationSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AgreementsNavigationSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open Active Agreements view")]
        public void OpenActiveAgreementsView()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreements");
            _uiContext.XrmApp.Grid.SwitchView("Active Agreements");
        }

        [When(@"I open All Agreements view")]
        public void OpenAllAgreementsView()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreements");
            _uiContext.XrmApp.Grid.SwitchView("All Agreements");
        }

        [When(@"I open agreements grid record with name '(.*)'")]
        public void OpenAgreementsGridRecordWithName(string name)
        {
            name = new StringTransformer().Transform(name);
            var indexToSelect = _uiContext.XrmApp.Grid.GetGridItems().Select((value, index) => new { Value = value, Index = index })
                .First(pair => pair.Value.Attributes[pair.Value.Attributes.Keys.First(x => x.Contains("name"))].ToString() == name).Index;
            _uiContext.XrmApp.Grid.OpenRecord(indexToSelect);
        }

        [When(@"I go to create new Agreement")]
        public void GoToCreateNewAgreement()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Agreements");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");
        }

        [When(@"I search for saved '(.*)' agreement")]
        public void SearchForSavedAgreement(string key)
        {
            var savedName = ScenarioContext.Get<string>(key);
            _uiContext.XrmApp.Grid.Search(savedName);
        }

        [Then(@"Saved '(.*)' agreement is present on grid with the following values:")]
        public void SavedAgreementIsPresentOnGridWithTheFollowingValues(string key, Dictionary<string, string> keyValues)
        {
            var agreementName = ScenarioContext.Get<string>(key);
            var item = _uiContext.XrmApp.Grid.GetGridItems()
                .FirstOrDefault(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("name"))].ToString() == agreementName);
            Assert.IsNotNull(item, $"Agreement with name {agreementName} is not found");

            var attributes = item.Attributes;
            var issuesList = new List<string>();
            foreach(var kv in keyValues)
            {
                var actualValue = attributes[attributes.Keys.First(x => x.Contains(kv.Key))].ToString();
                issuesList.AddIfFalse(actualValue == kv.Value, $"{kv.Key} value is incorrect. Expected: {kv.Value}, actual: {actualValue}");
            }

            AssertionUtils.IsEmpty(issuesList, "Some agreement fields are incorrect");
        }

        [Then(@"Is Attested value is equal to saved '(.*)' for found '(.*)' agreement")]
        public void IsAttestedValueIsEqualToSavedForFoundAgreement(string valueKey, string nameKey)
        {
            var expectedValue = ScenarioContext.Get<string>(valueKey) == "True" ? "Yes" : "No";
            var agreementName = ScenarioContext.Get<string>(nameKey);
            var item = _uiContext.XrmApp.Grid.GetGridItems()
                .FirstOrDefault(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("name"))].ToString() == agreementName);
            Assert.IsNotNull(item, $"Agreement with name {agreementName} is not found");
            var actualValue = item.Attributes[item.Attributes.Keys.First(y => y.Contains("isattested"))].ToString();
            Assert.AreEqual(expectedValue, actualValue, "Is attested value is incorrect");
        }
    }
}
