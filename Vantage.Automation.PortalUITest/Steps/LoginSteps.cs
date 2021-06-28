using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Helpers;
using Vantage.Automation.PortalUITest.Pages;

namespace Vantage.Automation.PortalUITest.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly UIContext _uiContext;
        public LoginSteps(UIContext context)
        {
            _uiContext = context;
        }

        [Given("I am logged in to Portal")]
        [Given("I am logged in to Portal with Legal Pro")]
        public void LogInToCRMLegalPro()
        {
            Login(ConfigHelper.Login_LegalProUsername, ConfigHelper.Login_LegalProPassword);
        }

        [Given("I am logged in to Portal with Triage")]
        public void LogInToCRMTriage()
        {
            Login(ConfigHelper.Login_TriageUsername, ConfigHelper.Login_TriagePassword);
        }

        private void Login(string username, string password)
        {
            _uiContext.Driver = new ChromeDriver(ConfigHelper.DriversPath);
            _uiContext.Driver.Url = ConfigHelper.PortalHost;

            var loginPage = new LoginPage(_uiContext);
            loginPage.Login(username, password);
        }
    }
}
