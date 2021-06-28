using System.Threading;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using Vantage.Automation.PortalUITest.Context;
using Vantage.Automation.PortalUITest.Helpers;

namespace Vantage.Automation.PortalUITest.Pages
{
    public class LoginPage
    {
        private readonly UIContext _uiContext;

        private const string _usernameInput = "//*[@id='i0116']";       
        private const string _passwordInput ="//*[@id='i0118']";
        private const string _nextButton = "//*[@id='idSIButton9']";
        private const string _dontshowAgainCheck = "//*[@id='KmsiCheckboxField']";
        public LoginPage(UIContext context)
        {
            _uiContext = context;
        }

        public void Login(string username, string password)
        {
            var usernameInput = _uiContext.Driver.WaitUntilAvailable(By.XPath(_usernameInput), ConfigHelper.DefaultTimeOutInSeconds);
            usernameInput.SendKeys(username);
            var nexbutton1 = _uiContext.Driver.WaitUntilClickable(By.XPath(_nextButton), ConfigHelper.DefaultTimeOutInSeconds);
            nexbutton1.Click();
            Thread.Sleep(2000);

            var passwordInput = _uiContext.Driver.WaitUntilAvailable(By.XPath(_passwordInput), ConfigHelper.DefaultTimeOutInSeconds);
            passwordInput.SendKeys(password);
            var nexbutton2 = _uiContext.Driver.WaitUntilClickable(By.XPath(_nextButton), ConfigHelper.DefaultTimeOutInSeconds);
            nexbutton2.Click();
            Thread.Sleep(2000);

            _uiContext.Driver.WaitUntilAvailable(By.XPath(_dontshowAgainCheck), ConfigHelper.DefaultTimeOutInSeconds);
            var nexbutton3 = _uiContext.Driver.WaitUntilClickable(By.XPath(_nextButton), ConfigHelper.DefaultTimeOutInSeconds);
            nexbutton3.Click();
        }
    }
}
