using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public static class ConfigHelper
    {
        private const string _allEnvironmentsSection = "AllEnvironmentsConfig";

        private static IConfigurationRoot _config;
        static ConfigHelper()
        {
            _config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();
        }
        public static string TestEnvironment
        {
            get { return GetSetting(); }
        }

        public static string BrowserType
        {
            get { return GetSetting(); }
        }

        public static string DriversPath
        {
            get { return GetSetting(); }
        }

        public static string ScreenshotsPath
        {
            get { return GetSetting(); }
        }

        public static bool UsePrivateMode
        {
            get { return GetSetting().ToLower() == "true"; }
        }

        public static bool KillChromeDriversOnStart
        {
            get { return GetSetting().ToLower() == "true"; }
        }

        public static bool KillChromeDriversOnStop
        {
            get { return GetSetting().ToLower() == "true"; }
        }

        public static string VantageCRMHost
        {
            get { return GetEnvironmentSetting(); }
        }

        #region Logins

        public static string Login_LegalProUsername
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_LegalProPassword
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_TriagePassword
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_TriageUsername
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_BusinessRequestor1Username
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_BusinessRequestor1Password
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_BusinessRequestor2Username
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_BusinessRequestor2Password
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Logins

        #region Service Request Properties

        public static string ServiceRequestRegion
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ServiceRequestCountry
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ServiceRequestState
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ServiceRequestOpportunity
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ServiceRequestDocumentRequestType
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Service Request Properties

        #region Lookup Users

        public static string LegalProUser1
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string LegalProUser2
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string TriageUser1
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string TriageUser2
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Lookup Users

        private static string GetSetting([CallerMemberName] string memberName = null)
        {
            return _config[memberName];
        }

        private static string GetEnvironmentSetting([CallerMemberName] string memberName = null)
        {
            return _config.GetSection(TestEnvironment)[memberName] ?? _config.GetSection(_allEnvironmentsSection)[memberName];
        }
    }
}
