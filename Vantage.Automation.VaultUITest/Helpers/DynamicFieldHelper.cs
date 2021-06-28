using OpenQA.Selenium;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class DynamicFieldHelper
    {
        private const string _xPathToInput = "//label[@role='presentation' and text()='{0}']//following::*[contains(@class,'customControl') or (@data-id and contains(@data-id,'.') and (@title or (@aria-label and not(contains(@aria-label,'Required ')))))][1]";

        private readonly UIContext _uiContext;
        public DynamicFieldHelper(UIContext context)
        {
            _uiContext = context;
        }

        public FieldType GetFieldType(string field)
        {
            string dataId = GetFieldDataId(field);
            if (string.IsNullOrEmpty(dataId))
            {
                return FieldType.Text;
            }
            string dataType = dataId.Split('.')[1];
            if (dataType.Contains("text-box"))
            {
                return FieldType.Text;
            }
            else if (dataType.Contains("date-time"))
            {
                return FieldType.DateTime;
            }
            else if (dataType.Contains("LookupResults"))
            {
                return FieldType.Lookup;
            }
            else if (dataType.Contains("option-set"))
            {
                return FieldType.OptionSet;
            }
            else if (dataType.Contains("checkbox"))
            {
                return FieldType.Boolean;
            }

            string customControlClassName = GetCustomControlClassName(field);
            if (string.IsNullOrEmpty(customControlClassName))
            {
                return FieldType.Text;
            }
            else if (customControlClassName.Contains("MultiTagSelect"))
            {
                return FieldType.MultiValueOptionSet;
            }
            else if (customControlClassName.Contains("MultiSelectPicklist"))
            {
                return FieldType.MultiValuePickList;
            }            
            else if (customControlClassName.Contains("FlipSwitch"))
            {
                return FieldType.Boolean;
            }
            return FieldType.Text;
        }

        public string GetUnderlyingFieldName(string field)
        {
            string dataId = GetFieldDataId(field);
            if (string.IsNullOrEmpty(dataId))
            {
                return field;
            }
            return dataId.Split('.')[0].Replace("header_process_","");
        }

        private string GetFieldDataId(string field)
        {
            // Attempt to find the input element after the label
            var element = GetFieldElement(_xPathToInput, field);
            if (element == null)
            { 
                    return "";
            }
            return element.GetAttribute("data-id");
        }

        private string GetCustomControlClassName(string field)
        {
            var element = GetFieldElement(_xPathToInput, field);
            if (element == null)
            {
                return "";
            }
            return element.GetAttribute("class");
        }


        private IWebElement GetFieldElement(string xpath, string field)
        {
            IWebElement element = null;
            try
            {
                element = _uiContext.WebClient.Browser.Driver.FindElement(By.XPath(string.Format(xpath, field)));
                return element;
            }
            catch (NoSuchElementException)
            {
            }

            return element;
        }
    }
}
