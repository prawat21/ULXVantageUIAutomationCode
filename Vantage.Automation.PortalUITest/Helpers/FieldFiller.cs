using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Vantage.Automation.PortalUITest.Context;

namespace Vantage.Automation.PortalUITest.Helpers
{
    public class FieldFiller
    {
        private readonly UIContext _uiContext;

        public FieldFiller(UIContext context)
        {
            _uiContext = context;
        }
        public void FillOutFields(Dictionary<string, string> fieldDictionary, bool throwIfNotPresent)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldInput = fieldHelper.GetFieldInput(fieldKey);
                if (fieldInput == null)
                {
                    new Actions(_uiContext.Driver).SendKeys(Keys.Tab).Perform();
                    Thread.Sleep(500);
                    fieldInput = fieldHelper.GetFieldInput(fieldKey);
                }
                if (fieldInput == null)
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
                var fieldValue = transformer.Transform(fieldDictionary[fieldKey]);
                Console.WriteLine($"Setting field {fieldKey} to {fieldValue}");

                if (fieldInput.GetAttribute("role") == "spinbutton")
                {
                    fieldInput.SendKeys(Keys.Backspace);
                    fieldInput.SendKeys(Keys.Backspace);
                    fieldInput.SendKeys(Keys.Backspace);
                }
                fieldInput.SendKeys(fieldValue);
                Thread.Sleep(500);
            }
            new Actions(_uiContext.Driver).SendKeys(Keys.Tab).Perform();
        }


        public void VerifyFields(Dictionary<string, string> fieldDictionary, bool throwIfNotPresent, bool contains)
        {
            StringTransformer transformer = new StringTransformer();
            DynamicFieldHelper fieldHelper = new DynamicFieldHelper(_uiContext);

            StringBuilder errors = new StringBuilder();

            foreach (string fieldKey in fieldDictionary.Keys)
            {
                var fieldLabel = fieldHelper.GetFieldLabel(fieldKey);
                if (fieldLabel == null)
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
                var fieldExpectedValue = transformer.Transform(fieldDictionary[fieldKey]);
                var fieldActualValue = fieldLabel.Text;

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
    }
}
