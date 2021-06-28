using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class FieldGetter
    {
        private readonly UIContext _uiContext;
        public FieldGetter(UIContext context)
        {
            _uiContext = context;
        }

        public bool IsFieldPresent(string label)
        {
            return GetField(label) != null;
        }

        public bool IsFieldRequired(string label)
        {
            try
            {
                return GetField(label).IsRequired;
            }
            catch(NullReferenceException)
            {
                throw new InvalidOperationException($"Unable to check if {label} field is required, because this field is not found");
            }
        }

        public bool IsFieldReadonly(string label, string readonlyAttribute = null)
        {
            try
            {
                var field = GetField(label);
                field.ReadonlyAttribute = readonlyAttribute;
                return field.IsReadOnly;
            }
            catch (NullReferenceException)
            {
                throw new InvalidOperationException($"Unable to check if {label} field is readonly, because this field is not found");
            }
        }

        public string GetValue(string label, bool skipReps = false)
        {
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(label);
            var fieldType = fieldHelper.GetFieldType(label);
            Log.TestLog.Info($"Getting value of a {fieldType} control with name {fieldName}");
            string actualValue = null;
            switch (fieldType)
            {
                case FieldType.Text:
                    actualValue = _uiContext.XrmApp.Entity.GetValue(fieldName);
                    break;
                case FieldType.DateTime:
                    actualValue = _uiContext.XrmApp.Entity.GetValue(new DateTimeControl(fieldName)).ToString();
                    break;
                case FieldType.Lookup:
                    actualValue = _uiContext.XrmApp.Entity.GetValue(new LookupItem { Name = fieldName });
                    break;
                case FieldType.Boolean:
                    actualValue = _uiContext.XrmApp.Entity.GetValue(new BooleanItem { Name = fieldName }).ToString();
                    break;
                case FieldType.OptionSet:
                    actualValue = _uiContext.XrmApp.Entity.GetValue(new OptionSet { Name = fieldName }).ToString();
                    break;
            }
            
            if(!string.IsNullOrEmpty(actualValue) && skipReps)
            {
                var reps = Regex.Split(actualValue, @";\s").ToList();
                if (fieldType == FieldType.Lookup)
                {
                    reps = reps.Where(x => x != "").ToList();
                }
                if (reps.All(x => x == reps[0]))
                {
                    Log.TestLog.Info($"There were reps in value, result: {reps[0]}");
                    return reps[0];
                }
                else
                {
                    throw new InvalidOperationException($"Unable to skip reps, because values are different: {actualValue}");
                }
            }

            Log.TestLog.Info($"{fieldName} field value is {actualValue}");
            return actualValue;
        }

        public string GetQuickCreateValue(string label, bool skipReps = false)
        {
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(label);
            var fieldType = fieldHelper.GetFieldType(label);
            Log.TestLog.Info($"Getting value of a {fieldType} control with name {fieldName} from Quick Create form");
            string actualValue = null;
            switch (fieldType)
            {
                case FieldType.Text:
                    actualValue = _uiContext.XrmApp.QuickCreate.GetValue(fieldName);
                    break;
                case FieldType.DateTime:
                    throw new NotImplementedException("No get method found for datetime");
                case FieldType.Lookup:
                    actualValue = _uiContext.XrmApp.QuickCreate.GetValue(new LookupItem { Name = fieldName });
                    break;
                case FieldType.Boolean:
                    actualValue = _uiContext.XrmApp.QuickCreate.GetValue(new BooleanItem { Name = fieldName }).ToString();
                    break;
                case FieldType.OptionSet:
                    actualValue = _uiContext.XrmApp.QuickCreate.GetValue(new OptionSet { Name = fieldName }).ToString();
                    break;
            }

            if (actualValue != null && skipReps)
            {
                var reps = Regex.Split(actualValue, @";\s");
                if (reps.All(x => x == reps[0]))
                {
                    Log.TestLog.Info($"There were reps in value, result: {reps[0]}");
                    return reps[0];
                }
                else
                {
                    throw new InvalidOperationException($"Unable to skip reps, because values are different: {actualValue}");
                }
            }

            Log.TestLog.Info($"{fieldName} field value is {actualValue}");
            return actualValue;
        }

        public bool TryGetValue(string label, out string value, bool skipReps = false)
        {
            value = null;
            try
            {
                value = GetValue(label, skipReps);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public void ClickLookupSearchIcon(string label)
        {
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(label);
            _uiContext.XrmApp.Entity.ClickLookupSearchIcon(new LookupItem { Name = fieldName });
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

        public List<string> GetLookupResults(string label, string value)
        {
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(label);
            return _uiContext.XrmApp.Entity.GetLookupResults(new LookupItem { Name = fieldName, Value = value });
        }

        private Field GetField(string label)
        {
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);
            var fieldName = fieldHelper.GetUnderlyingFieldName(label);
            var field = _uiContext.XrmApp.Entity.GetField(fieldName, false);
            return field;
        }
    }
}
