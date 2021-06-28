using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.CustomPageObjects
{
    public class AuditHistoryTable
    {
        private readonly UIContext _uiContext;

        private readonly By CoreLocator = By.CssSelector(".ms-crm-ListArea");

        private readonly By HeadersLocator = By.XPath(".//tr[not(@class='ms-crm-Hidden-List')]//th[@scope='col']//label");

        private readonly By RowLocator = By.CssSelector(".ms-crm-List-MultilineRow");

        private readonly By CellLocator = By.XPath(".//td[contains(@class, 'ms-crm-List-DataCell')]");

        private readonly By CellLineLocator = By.CssSelector(".ms-crm-List-RowLine");

        public AuditHistoryTable(UIContext uiContext)
        {
            _uiContext = uiContext;
        }

        public List<Dictionary<string, object>> GetGridItems()
        {
            var headers = _uiContext.WebClient.Browser.Driver.FindElements(new ByChained(CoreLocator, HeadersLocator)).Select(x => x.Text).ToList();
            var rows = _uiContext.WebClient.Browser.Driver.FindElements(new ByChained(CoreLocator, RowLocator));
            var results = new List<Dictionary<string, object>>();
            foreach(var row in rows)
            {
                var cells = row.FindElements(CellLocator);
                if(cells.Count != headers.Count)
                {
                    throw new InvalidOperationException();
                }
                var dict = new Dictionary<string, object>();
                for (var i = 0; i<cells.Count; i++)
                {
                    var lines = cells[i].FindElements(CellLineLocator);
                    if (lines.Any())
                    {
                        dict.Add(headers[i], lines.Select(x => x.Text).ToList());
                    }
                    else
                    {
                        dict.Add(headers[i], cells[i].Text);
                    }
                }

                results.Add(dict);
            }

            return results;
        }
    }
}
