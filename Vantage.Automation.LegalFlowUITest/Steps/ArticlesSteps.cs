using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ArticlesSteps
    {
        private readonly UIContext _uiContext;

        private const string _articleBodyXPath = "//body[@role='textbox']";
        private const string _designerFrameXPath = "//iframe[@title='Designer']";
        private const string _editorFrameXPath = "//iframe[@title='Rich text editor']";
        private const string _articleSubject = "Default Subject";

        public ArticlesSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I go to create a new Article")]
        public void GoToCreateNewArticle()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("My Workspace", "Browse Articles");
            _uiContext.XrmApp.CommandBar.ClickCommand("New");
        }

        [When("I set the article body to '(.*)'")]
        public void SetArticleBodyTo(string body)
        {
            var designerFrame = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_designerFrameXPath));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(designerFrame);

            var editorFrame = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_editorFrameXPath));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(editorFrame);

            var articleBody = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_articleBodyXPath));
            articleBody.SendKeys(body);
            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [When("I fill out Article Subject and Review")]
        public void FillOutAuthor()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Author");

            DynamicFieldHelper dfh = new DynamicFieldHelper(_uiContext);
            _uiContext.XrmApp.BusinessProcessFlow.SetValue(new BooleanItem() { Name = "readyforreview", Value = true });
            _uiContext.XrmApp.BusinessProcessFlow.SetValue("subjectid", _articleSubject);
            _uiContext.XrmApp.ThinkTime(2000);
        }

        [When("I Approve the Article")]
        public void ApproveArticle()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Review");

            DynamicFieldHelper dfh = new DynamicFieldHelper(_uiContext);
            _uiContext.XrmApp.BusinessProcessFlow.SetValue(new OptionSet() { Name = "review", Value = "Approved" });
            _uiContext.XrmApp.ThinkTime(2000);
            _uiContext.XrmApp.Dialogs.ConfirmationDialog(true);
        }

        [When("I mark the Article as Completed")]
        public void CompleteArticle()
        {
            _uiContext.XrmApp.BusinessProcessFlow.SelectStage("Publish");

            DynamicFieldHelper dfh = new DynamicFieldHelper(_uiContext);
            _uiContext.XrmApp.BusinessProcessFlow.SetValue(new BooleanItem() { Name = "setproductassociations", Value = true });
            _uiContext.XrmApp.ThinkTime(2000);
        }
    }
}
