using System;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly UIContext _uiContext;
        private const int _loginRetries = 5;
        public LoginSteps(UIContext context)
        {
            _uiContext = context;
        }

        [Given("I am logged in to CRM")]
        [Given("I am logged in to CRM with Legal Pro")]
        public void LogInToCRMLegalPro()
        {
            Login(ConfigHelper.Login_LegalProUsername, ConfigHelper.Login_LegalProPassword);
        }

        [Given("I am logged in to CRM with Triage")]
        public void LogInToCRMTriage()
        {
            Login(ConfigHelper.Login_TriageUsername, ConfigHelper.Login_TriagePassword);
        }

        [Given("I am logged in to CRM with Business Requestor")]
        [Given("I am logged in to CRM with Business Requestor 1")]
        public void LogInToCRMBusinessRequestor1()
        {
            Login(ConfigHelper.Login_BusinessRequestor1Username, ConfigHelper.Login_BusinessRequestor1Password);
        }

        [Given("I am logged in to CRM with Business Requestor 2")]
        public void LogInToCRMBusinessRequestor2()
        {
            Login(ConfigHelper.Login_BusinessRequestor2Username, ConfigHelper.Login_BusinessRequestor2Password);
        }

        private void Login(string username, string password)
        {
            int tries = 0;
            while (tries < _loginRetries)
            {
                try
                {
                    _uiContext.WebClient = new WebClient(TestSettings.Options);
                    _uiContext.XrmApp = new XrmApp(_uiContext.WebClient);
 
                    _uiContext.XrmApp.OnlineLogin.Login(new Uri(ConfigHelper.VantageCRMHost),
                        username.ToSecureString(),
                        password.ToSecureString());
                    break;
                }
                catch
                {
                    _uiContext.WebClient.Browser.Driver.Manage().Cookies.DeleteAllCookies();
                    _uiContext.XrmApp.Dispose();
                    _uiContext.WebClient = null;
                    _uiContext.XrmApp = null;

                    tries++;
                    if (tries >= _loginRetries)
                    {
                        throw;
                    }
                }
            }

            _uiContext.XrmApp.Navigation.OpenApp("LegalFlow", false);
        }
    }
}
