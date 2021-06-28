using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class BusinessProcessFlowSteps
    {
        private readonly UIContext _uiContext;

        public BusinessProcessFlowSteps(UIContext context)
        {
            _uiContext = context;
        }

        [StepDefinition("I select Next Stage from '(.*)'")]
        public void ISelectNextStageFrom(string stage)
        {
            _uiContext.XrmApp.BusinessProcessFlow.NextStage(stage);
            _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            _uiContext.XrmApp.ThinkTime(2000);
        }

        [StepDefinition("I verify that the Business Process Flow stage is '(.*)'")]
        public void IVerifyStageIs(string stage)
        {
            Assert.AreEqual(stage, _uiContext.XrmApp.BusinessProcessFlow.GetActiveStage(), "Business Process Flow stage mistmatch");
        }

        [Then("I verify that Business Process Flow exists")]
        public void BusinessProcessFlowExists()
        {
            try
            {
                _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Submitted");
            }
            catch
            {
                Assert.Fail("Business process flow is not present");
            }
        }

        [StepDefinition("I select Finish from '(.*)'")]
        public void ISelectFinishFrom(string stage)
        {
            _uiContext.XrmApp.BusinessProcessFlow.NextStage(stage, finish: true);
        }

        [StepDefinition("I select Stage '(.*)'")]
        public void SelectStage(string stage)
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage(stage);
            _uiContext.XrmApp.ThinkTime(1000);
        }

        [StepDefinition("I fill out the following Business Process Flow fields")]
        public void FillOutBusinessProcessFlowFields(Table table)
        {
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.BusinessProcessFlowFillOutFields(keyValues);
        }

        [StepDefinition("I verify that Business Process Flow status contains '(.*)'")]
        [StepDefinition("I verify that Service Request status contains '(.*)'")]
        public void VerifyStatusContains(string contains)
        {
            var statusField = _uiContext.XrmApp.BusinessProcessFlow.GetStatusField();
            Assert.IsNotNull(statusField, "Failed to retrieve the status field");
            Assert.IsTrue(statusField.Label.Contains(contains), "Status field '{0}' does not contain '{1}'", statusField.Label, contains);
        }
    }
}
