using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class AdvancedFindSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public AdvancedFindSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [StepDefinition("I search Advanced Find for an Agreement with name '(.*)' and value '(.*)'='(.*)'")]
        public void AdvancedFindAgreement(string agreementName, string key, string value)
        {
            agreementName = TransformString(agreementName);
            key = TransformString(key);
            value = TransformString(value);

            var agreements = _uiContext.XrmApp.AdvancedFind.Find("Agreements", true, "Agreement Name", agreementName);

            Assert.IsTrue(agreements.Any(x => x[key].ToString() == value),
                $"Could not find an agreement named '{agreementName}' with {key}={value}");
        }

        private string TransformString(string input)
        {
            if (input.StartsWith("[") && input.Length > 2 &&
                        ScenarioContext.ContainsKey(input.Substring(1, input.Length - 2)))
            {
                input = ScenarioContext[input.Substring(1, input.Length - 2)].ToString();
            }
            else
            {
                input = new StringTransformer().Transform(input);
            }
            return input;
        }
    }
}
