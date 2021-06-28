using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AgreementClauseTabSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly FieldGetter _fieldGetter;
        private readonly FieldSetter _fieldSetter;

        public AgreementClauseTabSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _fieldGetter = new FieldGetter(_uiContext);
            _fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
        }

        [When(@"I click '(.*)' button on Clause subgrid")]
        public void ClickNewClauseButtonOnClauseSubgrid(string command)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand("Clausesagr", command);
        }

        [When(@"I fill in the following fields on New Clause form:")]
        public void FillInTheFollowingFieldsOnNewClauseForm(Dictionary<string, string> keyValues)
        {
            _fieldSetter.FillInFields(keyValues);
        }

        [Then(@"The following fields are present on New Clause form:")]
        public void FollowingFieldsArePresentOnNewClauseForm(IList<string> fieldNames)
        {
            var issuesList = new List<string>();

            foreach (var field in fieldNames)
            {
                issuesList.AddIfFalse(_fieldGetter.TryGetValue(field, out _), $"{field} field is not present");
            }
            AssertionUtils.IsEmpty(issuesList, "Some of the fields are not present");
        }

        [When(@"I save clause text from New Clause form as '(.*)'")]
        public void SaveClauseNameFromNewClauseFormAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetValue("Clause Text", skipReps: true));
        }

        [Then(@"Agreement value is '(.*)' on New Clause form")]
        public void AgreementValueIsOnNewClauseForm(string expectedValue)
        {
            expectedValue = new StringTransformer().Transform(expectedValue);
            Assert.AreEqual(expectedValue, _fieldGetter.GetValue("Agreement", skipReps: true), "Agreement field value is incorrect");
        }

        [Then(@"Clause with saved text '(.*)' is present on Clauses subgrid")]
        public void SavedClauseIsPresentOnClausesSubgrid(string key)
        {
            var savedText = ScenarioContext.Get<string>(key);
            for (int retry = 0; retry < 3; retry++)
            {
                _uiContext.XrmApp.Entity.SubGrid.Search("Clausesagr", savedText, true);
                var texts = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems("Clausesagr").Select(x => x.Attributes["Clause Name"]).ToList();
                if (texts.Contains(savedText))
                {
                    return;
                }
                _uiContext.XrmApp.ThinkTime(30000);
            }
            Assert.Fail($"Clause with text {savedText} is not present");
        }

        [Then(@"Clause is marked as Complete")]
        public void ClauseIsMarkedAsComplete()
        {
            Assert.AreEqual("True", _fieldGetter.GetValue("Is Complete", skipReps: true), "Clause is not marked as Complete");
        }

        [When(@"I go to create New Clause")]
        public void CreateNewClause()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Clauses");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");
        }

        [When(@"I open Active Clauses view")]
        public void OpenActiveClauses()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Clauses");
            _uiContext.XrmApp.Grid.SwitchView("Active Clauses");
        }
    }
}
