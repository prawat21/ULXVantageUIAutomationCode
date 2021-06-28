using NLog;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class Log
    {
        public static readonly Logger TestLog = LogManager.GetLogger("TestLog");
    }
}
