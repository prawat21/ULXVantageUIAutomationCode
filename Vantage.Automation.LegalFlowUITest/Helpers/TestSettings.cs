using System;
using System.IO;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public static class TestSettings
    {
        private static readonly string Type = ConfigHelper.BrowserType;
        private static readonly string RemoteType = ConfigHelper.BrowserType;
        private static readonly string RemoteHubServerURL = "http://1.1.1.1:4444/wd/hub";
        private static readonly string DriversPath = ConfigHelper.DriversPath;
        private static readonly bool UsePrivateMode = ConfigHelper.UsePrivateMode;

        // Once you change this instance will affect all follow tests executions
        public static BrowserOptions SharedOptions = new BrowserOptions
        {
            BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), Type),
            PrivateMode = UsePrivateMode,
            FireEvents = false,
            Headless = false,
            UserAgent = false,
            DefaultThinkTime = 1500,
            RemoteBrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), RemoteType),
            RemoteHubServer = new Uri(RemoteHubServerURL),
            UCITestMode = true,
            UCIPerformanceMode = false,
            DriversPath = Path.IsPathRooted(DriversPath) ? DriversPath : Path.Combine(Directory.GetCurrentDirectory(), DriversPath),
            DisableExtensions = false,
            DisableFeatures = false,
            DisablePopupBlocking = false,
            DisableSettingsWindow = false,
            EnableJavascript = false,
            NoSandbox = false,
            DisableGpu = false,
            DumpDom = false,
            EnableAutomation = false,
            DisableImplSidePainting = false,
            DisableDevShmUsage = false,
            DisableInfoBars = false,
            TestTypeBrowser = false
        };

        // Create a new options instance, copy of the share, to use just in the current test, modifications in test will not affect other tests
        public static BrowserOptions Options => new BrowserOptions
        {
            BrowserType = SharedOptions.BrowserType,
            PrivateMode = SharedOptions.PrivateMode,
            FireEvents = SharedOptions.FireEvents,
            Headless = SharedOptions.Headless,
            UserAgent = SharedOptions.UserAgent,
            DefaultThinkTime = SharedOptions.DefaultThinkTime,
            RemoteBrowserType = SharedOptions.RemoteBrowserType,
            RemoteHubServer = SharedOptions.RemoteHubServer,
            UCITestMode = SharedOptions.UCITestMode,
            UCIPerformanceMode = SharedOptions.UCIPerformanceMode,
            DriversPath = SharedOptions.DriversPath,
            DisableExtensions = SharedOptions.DisableExtensions,
            DisableFeatures = SharedOptions.DisableFeatures,
            DisablePopupBlocking = SharedOptions.DisablePopupBlocking,
            DisableSettingsWindow = SharedOptions.DisableSettingsWindow,
            EnableJavascript = SharedOptions.EnableJavascript,
            NoSandbox = SharedOptions.NoSandbox,
            DisableGpu = SharedOptions.DisableGpu,
            DumpDom = SharedOptions.DumpDom,
            EnableAutomation = SharedOptions.EnableAutomation,
            DisableImplSidePainting = SharedOptions.DisableImplSidePainting,
            DisableDevShmUsage = SharedOptions.DisableDevShmUsage,
            DisableInfoBars = SharedOptions.DisableInfoBars,
            TestTypeBrowser = SharedOptions.TestTypeBrowser
        };
    }
}
