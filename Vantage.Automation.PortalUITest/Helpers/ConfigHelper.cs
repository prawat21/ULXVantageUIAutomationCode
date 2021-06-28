using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace Vantage.Automation.PortalUITest.Helpers
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

        public static string DriversPath
        {
            get { return GetSetting(); }
        }

        public static string PortalHost
        {
            get { return GetEnvironmentSetting(); }
        }

        public static TimeSpan DefaultTimeOutInSeconds
        {
            get { return TimeSpan.FromSeconds(int.Parse(GetEnvironmentSetting())); }
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

        #endregion Service Request Properties


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
