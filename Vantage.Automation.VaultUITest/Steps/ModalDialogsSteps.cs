using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class ModalDialogsSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        public ModalDialogsSteps(UIContext uIContext, ScenarioContext context) : base(context)
        {
            _uiContext = uIContext;
        }

        [Then(@"Error popup is present with text '(.*)'")]
        public void ErrorPopupIsPresentWithText(string text)
        {
            var error = _uiContext.XrmApp.Entity.GetBusinessProcessError();
            Assert.AreEqual(text, error, "Error text is incorrect");
        }

    }
}
