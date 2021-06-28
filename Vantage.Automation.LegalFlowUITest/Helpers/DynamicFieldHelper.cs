using OpenQA.Selenium;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public class DynamicFieldHelper
    {
        private const string _xPathToInput = "//label[@role='presentation' and text()='{0}']//following::*[@data-id and contains(@data-id,'.') and @aria-label and not(contains(@aria-label,'Required'))][1]";
        private const string _xPathToCustomControl = "//label[@role='presentation' and text()='{0}']//following::div[contains(@class,'customControl')][1]";
        private const string _quickCreatePrefix = "//div[@id='quickCreateTabContainer']";

        private readonly UIContext _uiContext;
        public DynamicFieldHelper(UIContext context)
        {
            _uiContext = context;
        }

        public FieldType GetFieldType(string field, bool quickCreate = false)
        {
            string dataId = GetFieldDataId(field, quickCreate);
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

            string customControlClassName = GetCustomControlClassName(field, quickCreate);
            if (string.IsNullOrEmpty(customControlClassName))
            {
                return FieldType.Text;
            }
            else if (customControlClassName.Contains("MultiTagSelect"))
            {
                return FieldType.MultiValueOptionSet;
            }
            else if (customControlClassName.Contains("FlipSwitch"))
            {
                return FieldType.Boolean;
            }
            return FieldType.Text;
        }

        public string GetUnderlyingFieldName(string field, bool quickCreate = false, bool throwExceptionIfNotFound = false)
        {
            string dataId = GetFieldDataId(field, quickCreate);
            if (string.IsNullOrEmpty(dataId))
            {
                if (throwExceptionIfNotFound)
                {
                    throw new NotFoundException("Could not find a field named " + field);
                }
                return field;
            }
            return dataId.Split('.')[0].Replace("header_process_","");
        }

        private string GetFieldDataId(string field, bool quickCreate)
        {
            // Attempt to find the input element after the label
            var element = GetFieldElement(quickCreate ? _quickCreatePrefix + _xPathToInput : _xPathToInput, field);
            if (element == null)
            {
                return "";
            }
            string dataId = element.GetAttribute("data-id");
            if (string.IsNullOrEmpty(dataId))
            {
                // input did not have data-id, find the custom control instead
                element = GetFieldElement(quickCreate ? _quickCreatePrefix + _xPathToCustomControl : _xPathToCustomControl, field);
                if (element == null)
                {
                    return "";
                }
                dataId = element.GetAttribute("data-id");
            }

            return dataId;
        }

        private string GetCustomControlClassName(string field, bool quickCreate)
        {
            var element = GetFieldElement(quickCreate ? _quickCreatePrefix + _xPathToCustomControl : _xPathToCustomControl, field);
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
