using System.Collections.Generic;
using OpenQA.Selenium;

namespace Vantage.Automation.PortalUITest.Context
{
    public class UIContext
    {
        public IWebDriver Driver;
        public List<string> Attachments;

        public UIContext()
        {
            Driver = null;
            Attachments = new List<string>();
        }
    }
}
