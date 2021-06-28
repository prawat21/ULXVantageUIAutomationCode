using System.Collections.Generic;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class QuickCreateSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly FieldGetter _fieldGetter;
        private readonly FieldSetter _fieldSetter;

        public QuickCreateSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _fieldGetter = new FieldGetter(_uiContext);
            _fieldSetter = new FieldSetter(_uiContext, ScenarioContext);
        }

        [When(@"I fill in the following fields on Quick Create - Agreement Package form:")]
        public void FillInTheFollowingFieldsOnQuickCreate_AgreementPackageForm(Dictionary<string, string> keyValues)
        {
            _fieldSetter.FillInQuickCreateFields(keyValues);
        }

        [When(@"I save Name from Quick Create - Agreement Package form as '(.*)'")]
        public void SaveNameFromQuickCreate_AgreementPackageFormAs(string key)
        {
            ScenarioContext.Add(key, _fieldGetter.GetQuickCreateValue("Name"));
        }

        [When(@"I click Save & Close on Quick Create form")]
        public void ClickSaveCloseOnQuickCreateForm()
        {
            _uiContext.XrmApp.QuickCreate.Save();
        }
    }
}
