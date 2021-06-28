using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public class FieldFiller
    {
        private readonly UIContext _uiContext;

        private const string _sectionXPath = "//section[@data-id='{0}']";

        public FieldFiller(UIContext context)
        {
            _uiContext = context;
        }
        public void EntityFillOutFields(Dictionary<string, string> fieldDictionary, bool throwIfNotPresent)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey);
                bool fieldExists = false;
                try
                {
                    var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
                    fieldExists = field != null;
                }
                catch
                {
                }
                if (!fieldExists)
                {
                    Console.WriteLine($"Field {fieldKey} is not present");
                    if (throwIfNotPresent)
                    {
                        throw new Exception($"Field {fieldKey} is not present");
                    }
                    else
                    {
                        continue;
                    }
                }

                var fieldType = fieldHelper.GetFieldType(fieldKey);
                var fieldValue = transformer.Transform(fieldDictionary[fieldKey]);
                _uiContext.FilledFields[fieldKey] = fieldValue;
                Console.WriteLine($"Setting field {fieldKey} to {fieldValue}");
                switch (fieldType)
                {
                    case FieldType.Text:
                        _uiContext.XrmApp.Entity.SetValue(fieldName, fieldValue);
                        break;
                    case FieldType.DateTime:
                        _uiContext.XrmApp.Entity.SetValue(new DateTimeControl(fieldName) { Value = DateTime.Parse(fieldValue) });
                        break;
                    case FieldType.Lookup:
                        _uiContext.XrmApp.Entity.ClearValue(new LookupItem { Name = fieldName });
                        _uiContext.XrmApp.Entity.SetValue(new LookupItem { Name = fieldName, Value = fieldValue, Index = 0 });
                        _uiContext.WebClient.Browser.Driver.WaitForTransaction();
                        break;
                    case FieldType.Boolean:
                        bool value = "true yes".Contains(fieldValue.ToLower());
                        _uiContext.XrmApp.Entity.SetValue(new BooleanItem { Name = fieldName, Value = value });
                        break;
                    case FieldType.OptionSet:
                        _uiContext.XrmApp.Entity.SetValue(new OptionSet { Name = fieldName, Value = fieldValue });
                        break;
                    case FieldType.MultiValueOptionSet:
                        _uiContext.XrmApp.Entity.SetValue(new MultiValueOptionSet() { Name = fieldName, Values = new string[] { fieldValue } });
                        break;
                }
            }
        }
        public void EntityVerifyFieldValues(Dictionary<string, string> fieldDictionary, bool contains)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            StringBuilder errors = new StringBuilder();

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldType = fieldHelper.GetFieldType(fieldKey);
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey);
                string fieldExpectedValue = string.Empty;
                if (fieldDictionary[fieldKey].StartsWith("[") && fieldDictionary[fieldKey].Length > 2 &&
                    _uiContext.FilledFields.ContainsKey(fieldDictionary[fieldKey].Substring(1, fieldDictionary[fieldKey].Length - 2)))
                {
                    fieldExpectedValue = _uiContext.FilledFields[fieldDictionary[fieldKey].Substring(1, fieldDictionary[fieldKey].Length - 2)].ToString();
                }
                else
                {
                    fieldExpectedValue = transformer.Transform(fieldDictionary[fieldKey]);
                }
                string fieldActualValue = string.Empty;
                switch (fieldType)
                {
                    case FieldType.Text:
                        fieldActualValue = _uiContext.XrmApp.Entity.GetValue(fieldName);
                        break;
                    case FieldType.DateTime:
                        fieldActualValue = _uiContext.XrmApp.Entity.GetValue(new DateTimeControl(fieldName)).ToString();
                        break;
                    case FieldType.Lookup:
                        fieldActualValue = _uiContext.XrmApp.Entity.GetValue(new LookupItem { Name = fieldName });
                        break;
                    case FieldType.Boolean:
                        fieldActualValue = _uiContext.XrmApp.Entity.GetValue(new BooleanItem { Name = fieldName }).ToString();
                        break;
                    case FieldType.OptionSet:
                        fieldActualValue = _uiContext.XrmApp.Entity.GetValue(new OptionSet { Name = fieldName }).ToString();
                        break;
                }

                bool areEqual = contains ? fieldActualValue.Contains(fieldExpectedValue) : fieldExpectedValue == fieldActualValue;

                if (!areEqual)
                {
                    errors.AppendFormat(
                        "Field '{0}' value should {1} '{2}' but it is '{3}'{4}",
                        fieldKey,
                        contains ? "contain" : "be",
                        fieldExpectedValue,
                        fieldActualValue,
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }
        public void EntityFieldsArePresent(Dictionary<string, bool> fieldDictionary)
        {
            StringBuilder errors = new StringBuilder();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey);

                bool fieldExists = false;
                try
                {
                    var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
                    fieldExists = field != null;
                }
                catch
                {
                }

                if (fieldDictionary[fieldKey] != fieldExists)
                {
                    errors.AppendFormat(
                        "Field '{0}' should {1}be present but it is{2}{3}",
                        fieldKey,
                        fieldDictionary[fieldKey] ? "" : "not ",
                        fieldExists ? "" : " not",
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }
        public void EntityFieldsAreRequired(Dictionary<string, bool> fieldDictionary)
        {
            StringBuilder errors = new StringBuilder();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey);

                var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
                bool fieldIsRequired = field != null && field.IsRequired;

                if (fieldDictionary[fieldKey] != fieldIsRequired)
                {
                    errors.AppendFormat(
                        "Field '{0}' should {1}be required but it is{2}{3}",
                        fieldKey,
                        fieldDictionary[fieldKey] ? "" : "not ",
                        fieldIsRequired ? "" : " not",
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }

        public void EntityUnderlyingFieldsAreRequired(string fieldsList)
        {
            StringBuilder errors = new StringBuilder();
            string[] fields = fieldsList.Split(";");
            foreach (string fieldName in fields)
            {
                var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
                bool fieldIsRequired = field != null && field.IsRequired;

                if (!fieldIsRequired)
                {
                    errors.AppendFormat(
                        "Field '{0}' should be required but it is not{1}",
                        fieldName,
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }

        public void EntitySectionsArePresent(string sectionsList)
        {
            StringBuilder errors = new StringBuilder();
            string[] sections = sectionsList.Split(";");
            foreach (string sectionName in sections)
            {
                if (sectionName.StartsWith("portal_"))
                {
                    continue;
                }    
                bool sectionIsPresent = false;
                try
                {
                    _uiContext.WebClient.Browser.Driver.FindElement(By.XPath(string.Format(_sectionXPath, sectionName)));
                    sectionIsPresent = true;
                }
                catch (NoSuchElementException)
                {
                }

                if (!sectionIsPresent)
                {
                    errors.AppendFormat(
                        "Section '{0}' should be present but it is not{1}",
                        sectionName,
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }

        public void EntityFieldToolTipsContain(Dictionary<string, string> fieldDictionary)
        {
            StringBuilder errors = new StringBuilder();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey);

                var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
                string fieldTooltip = field != null ? field.ToolTip : "";

                if (!fieldTooltip.Contains(fieldDictionary[fieldKey]))
                {
                    errors.AppendFormat(
                        "Field '{0}' tooltip '{1}' should contain '{2}'{3}",
                        fieldKey,
                        fieldTooltip,
                        fieldDictionary[fieldKey],
                        Environment.NewLine);
                }
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }
        }

        public void QuickCreateFillOutFields(Dictionary<string, string> fieldDictionary)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldType = fieldHelper.GetFieldType(fieldKey, true);
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey, true);
                var fieldValue = transformer.Transform(fieldDictionary[fieldKey]);

                _uiContext.FilledFields[fieldKey] = fieldValue;
                Console.WriteLine($"Setting field {fieldKey} to {fieldValue}");
                switch (fieldType)
                {
                    case FieldType.Text:
                        _uiContext.XrmApp.QuickCreate.SetValue(fieldName, fieldValue);
                        break;
                    case FieldType.DateTime:
                        _uiContext.XrmApp.QuickCreate.SetValue(new DateTimeControl(fieldName) { Value = DateTime.Parse(fieldValue) });
                        break;
                    case FieldType.Lookup:
                        _uiContext.XrmApp.QuickCreate.ClearValue(new LookupItem { Name = fieldName });
                        _uiContext.XrmApp.QuickCreate.SetValue(new LookupItem { Name = fieldName, Value = fieldValue, Index = 0 });
                        _uiContext.WebClient.Browser.Driver.WaitForTransaction();
                        break;
                    case FieldType.Boolean:
                        bool value = "true yes".Contains(fieldValue.ToLower());
                        _uiContext.XrmApp.QuickCreate.SetValue(new BooleanItem { Name = fieldName, Value = value });
                        break;
                    case FieldType.OptionSet:
                        _uiContext.XrmApp.QuickCreate.SetValue(new OptionSet { Name = fieldName, Value = fieldValue });
                        break;
                    case FieldType.MultiValueOptionSet:
                        _uiContext.XrmApp.QuickCreate.SetValue(new MultiValueOptionSet { Name = fieldName, Values = new string[] { fieldValue } });
                        break;
                }
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }
        }

        public void BusinessProcessFlowFillOutFields(Dictionary<string, string> fieldDictionary)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldType = fieldHelper.GetFieldType(fieldKey, false);
                var fieldName = fieldHelper.GetUnderlyingFieldName(fieldKey, false);
                var fieldValue = transformer.Transform(fieldDictionary[fieldKey]);

                _uiContext.FilledFields[fieldKey] = fieldValue;
                Console.WriteLine($"Setting field {fieldKey} to {fieldValue}");
                switch (fieldType)
                {
                    case FieldType.Text:
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(fieldName, fieldValue);
                        break;
                    case FieldType.DateTime:
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(new DateTimeControl(fieldName) { Value = DateTime.Parse(fieldValue) });
                        break;
                    case FieldType.Lookup:
                        _uiContext.XrmApp.BusinessProcessFlow.ClearValue(new LookupItem { Name = fieldName });
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(new LookupItem { Name = fieldName, Value = fieldValue, Index = 0 });
                        _uiContext.XrmApp.ThinkTime(2000);
                        break;
                    case FieldType.Boolean:
                        bool value = "true yes".Contains(fieldValue.ToLower());
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(new BooleanItem { Name = fieldName, Value = value });
                        break;
                    case FieldType.OptionSet:
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(new OptionSet { Name = fieldName, Value = fieldValue });
                        break;
                    case FieldType.MultiValueOptionSet:
                        _uiContext.XrmApp.BusinessProcessFlow.SetValue(new MultiValueOptionSet { Name = fieldName, Values = new string[] { fieldValue } });
                        break;
                }
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }
        }
    }
}
