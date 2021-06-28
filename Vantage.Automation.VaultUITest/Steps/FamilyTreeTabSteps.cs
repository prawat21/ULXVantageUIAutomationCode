using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class FamilyTreeTabSteps : BaseSteps
    {
        private readonly UIContext _uiContext;

        private const string _XpathFamilyTreeFrameId = "//*[@id='WebResource_Hierarchy']";
        private const string _XpathFamilyTreeRecord = "//tr[@aria-level={0}]//div[text()='{1}']";
        private const string _XpathFamilyTreeRecordExpandButton = "//tr//div[text()='{0}']//ancestor::tr//div[contains(@class,'dx-treelist-collapsed')]";
        private const string _XpathFamilyTreeRecordDragArea = "//tr//div[text()='{0}']//ancestor::tr/td[contains(@class,'dx-command-drag')]";
        private const string _XpathFamilyTreeRecordRow = "//tr//div[text()='{0}']//ancestor::tr";
        private const string _XpathFamilyTreeRecordSelectBox = "//tr//div[text()='{0}']//ancestor::tr//div[contains(@class,'dx-select-checkbox')]";
        private const string _XpathDownloadFilesButton = "//div[contains(@aria-label,'Download Selected Files')]";


        public FamilyTreeTabSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [Then("the Family Tree contains record with name '(.*)' at level (.*)")]
        public void VerifyFamilyTree(string recordName, int level)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathFamilyTreeFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            recordName = TransformString(recordName);

            string recordXpath = string.Format(_XpathFamilyTreeRecord, level, recordName);

            var element = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(recordXpath));
            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
            if (element == null)
            { 
                Assert.Fail($"Could not find a record with name '{recordName}' at level {level}");
            }                     
        }

        [When("I expand the Family Tree record with name '(.*)'")]
        public void ExpandFamilyTreeRecord(string recordName)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathFamilyTreeFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            recordName = TransformString(recordName);

            string recordXpath = string.Format(_XpathFamilyTreeRecordExpandButton, recordName);

            var expandButton = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(recordXpath));
            if (expandButton == null)
            {
                Assert.Fail($"Could not find a record with name '{recordName}'");
            }
            expandButton.Click();

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [Then("I select record with name '(.*)' and Download Files")]
        public void DownloadFile(string recordName)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathFamilyTreeFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            recordName = TransformString(recordName);

            string recordXpath = string.Format(_XpathFamilyTreeRecordSelectBox, recordName);

            var checkBox = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(recordXpath));
            if (checkBox == null)
            {
                Assert.Fail($"Could not find a record with name '{recordName}'");
            }
            checkBox.Click();

            BrowserFileHelper browserFileHelper = new BrowserFileHelper();
            int oldFileCount = browserFileHelper.GetDownloadFolderFileCount();

            var downloadFilesButton = _uiContext.WebClient.Browser.Driver.WaitUntilClickable(By.XPath(_XpathDownloadFilesButton));
            downloadFilesButton.Click();
            _uiContext.XrmApp.ThinkTime(5000);

            Assert.IsTrue(browserFileHelper.GetDownloadFolderFileCount() > oldFileCount, "File was not downloaded");

            _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
        }

        [When("I drag the Family Tree record with name '(.*)' to the record with name '(.*)'")]
        public void DragRecord(string recordName, string targetName)
        {
            var frameElement = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(_XpathFamilyTreeFrameId));
            _uiContext.WebClient.Browser.Driver.SwitchTo().Frame(frameElement);

            recordName = TransformString(recordName);
            targetName = TransformString(targetName);

            string dragAreaXpath = string.Format(_XpathFamilyTreeRecordDragArea, recordName);
            string dragToXpath = string.Format(_XpathFamilyTreeRecordRow, targetName);
            try
            {
                var dragArea = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(dragAreaXpath));
                var targetArea = _uiContext.WebClient.Browser.Driver.WaitUntilAvailable(By.XPath(dragToXpath));

                var action = new Actions(_uiContext.WebClient.Browser.Driver);
                action.MoveToElement(dragArea).ClickAndHold(dragArea).MoveToElement(targetArea).Release(targetArea).Build().Perform();
            }
            catch
            {
                Assert.Fail($"Could not find a record with name '{recordName}' or '{targetName}'");
            }
            finally
            {
                _uiContext.WebClient.Browser.Driver.SwitchTo().ParentFrame();
            }
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
