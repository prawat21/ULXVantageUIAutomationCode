using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class AssertionUtils
    {
        public static void IsEmpty<T>(IEnumerable<T> collection, string message)
        {
            Assert.IsFalse(collection.Any(), $"{message}. \r\nActual elements: {string.Join(", ", collection)}");
        }

        public static void Batch(params Action[] assertMethods)
        {
            var exceptions = ExceptionUtils.Catch(assertMethods);
            if (exceptions.Length != 0)
            {
                var message = string.Join(Environment.NewLine, exceptions.Select(ex => ex.Message));
                Assert.Fail(Environment.NewLine + message);
            }
        }
    }
}
