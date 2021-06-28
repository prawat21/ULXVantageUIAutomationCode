using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class GlobalSearchSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public GlobalSearchSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I open global search")]
        public void OpenGlobalSearch()
        {
            _uiContext.XrmApp.Navigation.OpenGlobalSearch();
        }

        [When(@"I fill in saved value '(.*)' into global search")]
        public void FillInSavedValueIntoGlobalSearch(string key)
        {
            var searchString = ScenarioContext.Get<string>(key);
            _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.XrmApp.GlobalSearch.Search(searchString);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
        }

        [Then(@"Global search results contain saved '(.*)' account")]
        public void GlobalSearchResultsContainSavedAccount(string key)
        {
            var savedAccountName = ScenarioContext.Get<string>(key);
            _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
            _uiContext.XrmApp.ThinkTime(1000);
            var results = _uiContext.XrmApp.GlobalSearch.GetGlobalSearchRecords("account").Select(x => x[x.Keys.First(y => y.Contains("name"))]).ToList();
            CollectionAssert.Contains(results, savedAccountName, "Account not found");
        }

        [Then(@"Global search results contain saved '(.*)' agreement")]
        public void GlobalSearchResultsContainSavedAgreement(string key)
        {
            var savedAccountName = ScenarioContext.Get<string>(key);
            _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
            _uiContext.XrmApp.ThinkTime(1000);
            var results = _uiContext.XrmApp.GlobalSearch.GetGlobalSearchRecords("agreement").Select(x => x[x.Keys.First(y => y.Contains("name"))]).ToList();
            CollectionAssert.Contains(results, savedAccountName, "Agreement not found");
        }

        [Then(@"Global Search results are present for all saved '(.*)'")]
        public void GlobalSearchResultsArePresentForAllSaved(string aliasesKey)
        {
            var savedAliases = ScenarioContext.Get<List<string>>(aliasesKey);
            Log.TestLog.Info($"Aliases to find: {string.Join(", ", savedAliases)}");
            var issuesList = new List<string>();
            foreach (var alias in savedAliases)
            {
                _uiContext.WebClient.Browser.Driver.WaitForPageToLoad();
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
                _uiContext.XrmApp.GlobalSearch.Search(alias);
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
                try
                {
                    var results = _uiContext.XrmApp.GlobalSearch.GetGlobalSearchRecords("ulx_alias").Select(x => x[x.Keys.First(y => y.Contains("name"))]);
                    issuesList.AddIfFalse(results.Contains(alias), $"Account alias '{alias}' is not present after global search");
                }
                catch (InvalidOperationException)
                {
                    issuesList.Add($"No results found after global search for alias '{alias}'");
                }
            }

            AssertionUtils.IsEmpty(issuesList, "Not all aliases can be found in global search");
        }
    }
}
