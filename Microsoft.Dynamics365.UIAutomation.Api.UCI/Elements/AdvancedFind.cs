using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace Microsoft.Dynamics365.UIAutomation.Api.UCI
{
    public class AdvancedFind : Element
    {
        private readonly WebClient _client;

        private const string _advancedFindButton = "//button[@data-id='advancedFindLauncher']";
        private const string _resultsButton = "//a[@id='Mscrm.AdvancedFind.Groups.Show.Results-Large']";
        private const string _editColumnsButton = "//a[@id='Mscrm.AdvancedFind.Groups.View.EditColumns-Medium']";
        private const string _bottomFrame = "//iframe[@id='contentIFrame0']";
        private const string _bottomFrameId = "contentIFrame0";
        private const string _selectEntity = "//select[@id='slctPrimaryEntity']";

        private const string _selectAdvanced = "//select[contains(@id,'advFindE_fieldList')]";
        private const string _textAdvanced = "//input[@type='text' and contains(@id,'advFind')]";

        private const string _editColumnsFrame = "//iframe[@id='InlineDialog_Iframe']";
        private const string _editColumnsFrameId = "InlineDialog_Iframe";
        private const string _viewEditorFrame = "//iframe[@id='viewEditor']";
        private const string _viewEditorFrameId = "viewEditor";
        private const string _editColumnsDialogOkButton = "//button[@id='butBegin']";

        private const string _addColumnsFrame = "//iframe[@id='InlineDialog1_Iframe']";
        private const string _addColumnsFrameId = "InlineDialog1_Iframe";
        private const string _addColumnsDialogCheckAllButton = "//input[@id='chkAll']";
        private const string _addColumnsDialogOkButton = "//button[@id='butBegin']";

        private const string _resultsFrame = "//iframe[@id='resultFrame']";
        private const string _resultsFrameId = "resultFrame";

        private const string _resultsNextPageButton = "//a[@id='_nextPageImg' and not(@disabled)]";

        private const string _addColumnsButton = "//div[@id='_TBAddColumnsoActivefalse']";

        // Locators for the results table
        private readonly By CoreLocator = By.CssSelector(".ms-crm-ListArea");
        private readonly By HeadersLocator = By.XPath(".//tr//th[@scope='col']//label");
        private readonly By RowLocator = By.CssSelector(".ms-crm-List-Row");
        private readonly By CellLocator = By.XPath(".//td[contains(@class, 'ms-crm-List-DataCell')]");

        public AdvancedFind(WebClient client) : base()
        {
            _client = client;
        }

        public List<Dictionary<string, object>> Find(string entity, bool checkAllColumns, string queryKey=null, string queryValue=null)
        {
            var advancedFindButton = _client.Browser.Driver.WaitUntilAvailable(By.XPath(_advancedFindButton));

            advancedFindButton.Click();

            _client.ThinkTime(3000);

            if (_client.Browser.Driver.WindowHandles.Count < 2)
            {
                throw new Exception("Should have opened a second window");
            }
            _client.Browser.Driver.SwitchTo().Window(_client.Browser.Driver.WindowHandles[1]);

            _client.Browser.Driver.WaitUntilAvailable(By.XPath(_bottomFrame));
            _client.Browser.Driver.SwitchTo().Frame(_bottomFrameId);

            var selectEntity = _client.Browser.Driver.FindElement(By.XPath(_selectEntity));
            selectEntity.SendKeys(entity);
            _client.ThinkTime(1000);

            if (!string.IsNullOrEmpty(queryKey) && !string.IsNullOrEmpty(queryValue))
            {
                var advancedSelectParent = _client.Browser.Driver.WaitUntilAvailable(By.XPath(_selectAdvanced + "/parent::div/parent::div"));
                advancedSelectParent.Click();

                var advancedSelect = _client.Browser.Driver.WaitUntilAvailable(By.XPath(_selectAdvanced));
                _client.Browser.Driver.ExecuteScript("arguments[0].style.border='3px solid red'", advancedSelect);

                var advancedSelectElement = new SelectElement(advancedSelect);
                advancedSelectElement.SelectByText(queryKey);
                _client.ThinkTime(1000);

                var advancedTextParent = _client.Browser.Driver.WaitUntilAvailable(By.XPath(_textAdvanced + "/parent::div/parent::div/parent::div"));
                advancedTextParent.Click();
                var advancedText = _client.Browser.Driver.WaitUntilAvailable(By.XPath(_textAdvanced));
                advancedText.SendKeys(queryValue);
                _client.ThinkTime(1000);
            }

            _client.Browser.Driver.SwitchTo().ParentFrame();

            if (checkAllColumns)
            {
                var editColumnsButton = _client.Browser.Driver.FindElement(By.XPath(_editColumnsButton));
                editColumnsButton.Click();

                _client.Browser.Driver.WaitUntilAvailable(By.XPath(_editColumnsFrame));
                _client.Browser.Driver.SwitchTo().Frame(_editColumnsFrameId);
                _client.Browser.Driver.WaitUntilAvailable(By.XPath(_viewEditorFrame));
                _client.Browser.Driver.SwitchTo().Frame(_viewEditorFrameId);

                var addColumnsButton = _client.Browser.Driver.FindElement(By.XPath(_addColumnsButton));
                addColumnsButton.Click();
                _client.Browser.Driver.SwitchTo().ParentFrame();
                _client.Browser.Driver.SwitchTo().ParentFrame();
                _client.Browser.Driver.WaitUntilAvailable(By.XPath(_addColumnsFrame));
                _client.Browser.Driver.SwitchTo().Frame(_addColumnsFrameId);

                _client.ThinkTime(1000);

                var checkAllButton = _client.Browser.Driver.FindElement(By.XPath(_addColumnsDialogCheckAllButton));
                checkAllButton.Click();

                var okButton1 = _client.Browser.Driver.FindElement(By.XPath(_addColumnsDialogOkButton));
                okButton1.Click();
                _client.Browser.Driver.SwitchTo().ParentFrame();
                _client.Browser.Driver.SwitchTo().Frame(_editColumnsFrameId);
                var okButton2 = _client.Browser.Driver.FindElement(By.XPath(_editColumnsDialogOkButton));
                okButton2.Click();
                _client.Browser.Driver.SwitchTo().ParentFrame();
            }

            var resultsButton = _client.Browser.Driver.FindElement(By.XPath(_resultsButton));
            resultsButton.Click();
            _client.Browser.Driver.WaitUntilAvailable(By.XPath(_bottomFrame));
            _client.Browser.Driver.SwitchTo().Frame(_bottomFrameId);
            _client.Browser.Driver.WaitUntilAvailable(By.XPath(_resultsFrame));
            _client.Browser.Driver.SwitchTo().Frame(_resultsFrameId);

            SetZoomLevel(_client.Browser.Driver, 20);
            var gridItems = GetGridItems(_client.Browser.Driver);
            while (_client.Browser.Driver.HasElement(By.XPath(_resultsNextPageButton)))
            {
                SetZoomLevel(_client.Browser.Driver, 100);
                _client.Browser.Driver.FindElement(By.XPath(_resultsNextPageButton)).Click();
                _client.ThinkTime(3000);
                SetZoomLevel(_client.Browser.Driver, 20);
                gridItems.AddRange(GetGridItems(_client.Browser.Driver));
            }
            SetZoomLevel(_client.Browser.Driver, 100);
            _client.Browser.Driver.SwitchTo().ParentFrame();
            _client.Browser.Driver.Close();
            _client.Browser.Driver.SwitchTo().Window(_client.Browser.Driver.WindowHandles[0]);

            return gridItems;
        }

        private void SetZoomLevel(IWebDriver driver, int level)
        {
            driver.ExecuteScript($"document.body.style.zoom='{level}%'");
        }

        private List<Dictionary<string, object>> GetGridItems(IWebDriver driver)
        {
            var headers = driver.FindElements(new ByChained(CoreLocator, HeadersLocator)).Select(x => x.Text).ToList();
            var rows = driver.FindElements(new ByChained(CoreLocator, RowLocator));
            var results = new List<Dictionary<string, object>>();
            foreach (var row in rows)
            {
                var cells = row.FindElements(CellLocator);
                if (cells.Count - 1 != headers.Count)
                {
                    throw new InvalidOperationException();
                }
                var dict = new Dictionary<string, object>();
                for (var i = 0; i < cells.Count-1; i++)
                {
                    if (string.IsNullOrEmpty(headers[i]))
                    {
                        continue;
                    }
                    dict.Add(headers[i], cells[i].Text);
                }

                results.Add(dict);
            }

            return results;
        }
    }
}
