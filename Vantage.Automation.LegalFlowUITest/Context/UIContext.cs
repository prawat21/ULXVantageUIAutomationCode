using System.Collections;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;

namespace Vantage.Automation.LegalFlowUITest.Context
{
    public class UIContext
    {
        public WebClient WebClient;
        public XrmApp XrmApp;
        public Hashtable FilledFields;
        public string SavedUrl;

        public UIContext()
        {
            WebClient = null;
            XrmApp = null;
            SavedUrl = "";
            FilledFields = new Hashtable();
        }
    }
}
