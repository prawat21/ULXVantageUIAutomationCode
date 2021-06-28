using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class ServiceRequestsGridSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public ServiceRequestsGridSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [Then(@"Service requests are present and ordered by descending date")]
        public void ServiceRequestsArePresentAndOrderedByDescendingDate()
        {
            var dates = _uiContext.XrmApp.Grid.GetGridItems()
                .Select(x => x.Attributes[x.Attributes.Keys.First(y => y.Contains("createdon"))].ToString().GetDatetimeFromString()).ToList();
            var sortedDates = dates.OrderByDescending(x => x).ToList();
            CollectionAssert.AreEqual(sortedDates, dates, $"Dates are not order by descending. Actual values: {string.Join(",\r\n", dates)}");
        }
    }
}
