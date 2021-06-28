using System;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps.Account
{
    public class AliasesTabSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AliasesTabSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [When(@"I save all account aliases from grid as '(.*)'")]
        public void SaveAllAccountAliasesFromGridAs(string key)
        {
            var names = _uiContext.XrmApp.Grid.GetGridItems().Select(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("name"))].ToString()).ToList();
            ScenarioContext.Add(key, names);
        }

        [Then(@"Account Aliases grid contains new record with account name from saved '(.*)'")]
        public void AccountAliasesGridContainsNewRecordWithAccountNameFromSaved(string key)
        {
            var savedName = ScenarioContext.Get<string>(key);
            var currentDateTime = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand("Subgrid_1", "See all records");
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.XrmApp.Grid.Search(savedName);
            var records = _uiContext.XrmApp.Grid.GetGridItems();
            var item = records.FirstOrDefault(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("name"))].ToString() == savedName
                && (currentDateTime - x.Attributes[x.Attributes.Keys.First(y => y.Contains("createdon"))].ToString().GetDatetimeFromString()).Minutes < 3);
            Assert.IsNotNull(item, $"Account alias '{savedName}' is not present on Aliases grid. Present records: {JsonConvert.SerializeObject(records.Select(x => x.Attributes))}");
        }
    }
}
