using Microsoft.Dynamics365.UIAutomation.Api.UCI;

namespace Vantage.Automation.VaultUITest.Context
{
    public class UIContext
    {
        public WebClient WebClient;
        public XrmApp XrmApp;

        public UIContext()
        {
            WebClient = null;
            XrmApp = null;
        }
    }
}
