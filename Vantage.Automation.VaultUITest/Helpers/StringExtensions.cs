using System;
using System.Globalization;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class StringExtensions
    {
        public static DateTime GetDatetimeFromString(this string str, string format = ProjectSpecificConstants.DatetimeFormatForGrid)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }
    }
}
