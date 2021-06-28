using System;
using System.Collections.Generic;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class ExceptionUtils
    {
        public static Exception[] Catch(params Action[] methods)
        {
            var exceptions = new List<Exception>();
            foreach (var method in methods)
            {
                try
                {
                    method();
                }
                catch (Exception ex)
                {
                    Log.TestLog.Error(ex.Message);
                    exceptions.Add(ex);
                }
            }

            return exceptions.ToArray();
        }
    }
}
