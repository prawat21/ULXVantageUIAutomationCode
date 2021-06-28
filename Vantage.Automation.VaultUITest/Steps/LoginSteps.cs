using System;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private const int _loginRetries = 5;

        public LoginSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
        }

        [Given("I am logged in to CRM")]
        [Given("I am logged in to CRM as an Admin")]
        public void LogInToCRMAdmin()
        {
            Login(ConfigHelper.Login_AdminUsername, ConfigHelper.Login_AdminPassword);
        }

        [Given("I am logged in to CRM as a Normal User")]
        public void LogInToCRMNormal()
        {
            Login(ConfigHelper.Login_NormalUsername, ConfigHelper.Login_NormalPassword);
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

                    _uiContext.XrmApp.OnlineLogin.Login(new Uri(ConfigHelper.VaultCRMHost),
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

            _uiContext.XrmApp.Navigation.OpenApp("Vault Super User", false);
        }
    }
}
