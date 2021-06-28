using Vantage.Automation.PortalUITest.Context;
using OpenQA.Selenium;

namespace Vantage.Automation.PortalUITest.Helpers
{
    public class DynamicFieldHelper
    {
        private const string _xPathToInput1 = "//label[@class='ulx-label' and contains(text(),'{0}')]//following::*[local-name()='input' or local-name()='textarea']";
        private const string _xPathToInput2 = "//*[@placeholder and contains(@placeholder,'{0}')]";
        private const string _xPathToInput3 = "//span[contains(text(),'{0}')]//following::*[@placeholder]";

        private const string _xPathToLabel1 = "//span[@class='ulx-form-section-header-text' and contains(text(),'{0}')]//following::div[not(@class)]";
        private const string _xPathToLabel2 = "//label[@class='ulx-label' and contains(text(),'{0}')]//following::div[not(@class)]";

        private readonly UIContext _uiContext;
        public DynamicFieldHelper(UIContext context)
        {
            _uiContext = context;
        }

        public IWebElement GetFieldInput(string field)
        {
            var input = FindElement(string.Format(_xPathToInput1, field)) ??
                (FindElement(string.Format(_xPathToInput2, field)) ?? FindElement(string.Format(_xPathToInput3, field)));

            return input;
        }

        public IWebElement GetFieldLabel(string field)
        {
            var input = FindElement(string.Format(_xPathToLabel1, field)) ?? FindElement(string.Format(_xPathToLabel2, field));

            return input;
        }

        private IWebElement FindElement(string xPath)
        {
            try
            {
                return _uiContext.Driver.FindElement(By.XPath(xPath));
            }
            catch (NotFoundException)
            {
                return null;
            }
        }
    }
}
