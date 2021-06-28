using System;
using System.Collections.Generic;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class FieldSetter
    {
        private readonly UIContext _uiContext;
        private readonly ScenarioContext _scenarioContext;
        public FieldSetter(UIContext context, ScenarioContext scenarioContext)
        {
            _uiContext = context;
            _scenarioContext = scenarioContext;
        }

        public void FillInFields(Dictionary<string, string> fieldDictionary)
        {
            foreach (string fieldKey in fieldDictionary.Keys)
            {
                FillInField(fieldKey, fieldDictionary[fieldKey]);
                _uiContext.WebClient.Browser.Driver.WaitForTransaction();
            }
        }

        public void FillInQuickCreateFields(Dictionary<string, string> fieldDictionary)
        {
            foreach (string fieldKey in fieldDictionary.Keys)
            {
                FillInQuickCreateField(fieldKey, fieldDictionary[fieldKey]);
            }
        }

        public void FillInField(string name, string value)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldType = fieldHelper.GetFieldType(name);
            var fieldName = fieldHelper.GetUnderlyingFieldName(name);
            string fieldValue = string.Empty;

            if (value.StartsWith("[") && value.Length > 2 &&
                   _scenarioContext.ContainsKey(value.Substring(1, value.Length - 2)))
            {
                fieldValue = _scenarioContext[value.Substring(1, value.Length - 2)].ToString();
            }
            else
            {
                fieldValue = transformer.Transform(value);
            }

            switch (fieldType)
            {
                case FieldType.Text:
                    _uiContext.XrmApp.Entity.SetValue(fieldName, fieldValue);
                    break;
                case FieldType.DateTime:
                    try
                    {
                        _uiContext.XrmApp.Entity.SetValue(new DateTimeControl(fieldName) { Value = DateTime.Parse(fieldValue) });
                    }
                    catch(NoSuchElementException ex)
                    {
                        if(!ex.Message.Contains("headerFieldsExpandButton"))
                        {
                            throw ex;
                        }
                    }
                    break;
                case FieldType.Lookup:
                    _uiContext.XrmApp.Entity.ClearValue(new LookupItem { Name = fieldName });
                    _uiContext.XrmApp.Entity.SetValue(new LookupItem { Name = fieldName, Value = fieldValue, Index = 0 }, useJsScroll: true);
                    _uiContext.XrmApp.ThinkTime(3000);
                    break;
                case FieldType.Boolean:
                    bool resultValue = "true yes".Contains(fieldValue.ToLower());
                    _uiContext.XrmApp.Entity.SetValue(new BooleanItem { Name = fieldName, Value = resultValue });
                    break;
                case FieldType.OptionSet:
                    _uiContext.XrmApp.Entity.SetValue(new OptionSet { Name = fieldName, Value = fieldValue });
                    break;
                case FieldType.MultiValueOptionSet:
                    _uiContext.XrmApp.Entity.SetValue(new MultiValueOptionSet() { Name = fieldName, Values = new string[] { fieldValue } });
                    break;
                case FieldType.MultiValuePickList:
                    _uiContext.XrmApp.Entity.SetValue(new MultiValuePickList() { Name = fieldName, Values = new string[] { fieldValue } });
                    break;
            }
        }

        public void FillInQuickCreateField(string name, string value)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            var fieldType = fieldHelper.GetFieldType(name);
            var fieldName = fieldHelper.GetUnderlyingFieldName(name);
            var fieldValue = transformer.Transform(value);

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
                    _uiContext.XrmApp.ThinkTime(2000);
                    break;
                case FieldType.Boolean:
                    bool resultValue = "true yes".Contains(fieldValue.ToLower());
                    _uiContext.XrmApp.QuickCreate.SetValue(new BooleanItem { Name = fieldName, Value = resultValue });
                    break;
                case FieldType.OptionSet:
                    _uiContext.XrmApp.QuickCreate.SetValue(new OptionSet { Name = fieldName, Value = fieldValue });
                    break;
                case FieldType.MultiValueOptionSet:
                    _uiContext.XrmApp.QuickCreate.SetValue(new MultiValueOptionSet { Name = fieldName, Values = new string[] { fieldValue } });
                    break;
            }
            _uiContext.XrmApp.ThinkTime(2000);
        }

        public void FillInLookupValueWithoutConfirmation(string name, string value)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(name);
            var fieldValue = transformer.Transform(value);
            _uiContext.XrmApp.Entity.FillInValue(new LookupItem { Name = fieldName, Value = fieldValue, Index = 0 });
        }
    }
}
