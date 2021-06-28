using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class PathUtility
    {
        private static readonly Dictionary<string, Func<string>> tokenHandlers = new Dictionary<string, Func<string>>
        {
            {"baseUrl", () => Environment.CurrentDirectory},
            {"currentDir", () => Environment.CurrentDirectory},
            {"testFiles", () => Path.Combine(Environment.CurrentDirectory, "TestData", "Files")}
        };

        public static string BuildAbsoluteUrl(string relativeUrlWithTokens)
        {
            return new Regex(@"\{([\w0-9_]+)\}").Replace(relativeUrlWithTokens.Replace("/", "\\"), m => tokenHandlers[m.Groups[1].Value]());
        }
    }
}
