using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace Vantage.Automation.VaultUITest.Helpers
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

        public static bool UsePrivateMode
        {
            get { return GetSetting().ToLower() == "true"; }
        }

        public static string RemoteBrowserType
        {
            get { return GetSetting(); }
        }

        public static string RemoteHubServer
        {
            get { return GetSetting(); }
        }

        public static string VaultCRMHost
        {
            get { return GetEnvironmentSetting(); }
        }

        #region Logins

        public static string Login_AdminUsername
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_AdminPassword
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AdminDisplayedUserName
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Login_NormalUsername
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string Login_NormalPassword
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string NormalDisplayedUserName
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Logins

        #region Entity Specific Fields
        public static string EntityRegion
        {
            get { return GetEnvironmentSetting(); }
        }
        #endregion Entity Specific Fields

        #region Clause Specific Fields

        public static string ClausePhase
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseFunctionalOwner
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string ClauseResponsibleParty
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string ClauseTrackingType
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string ClauseFrequency
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string ClauseFinancialImpact
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseTopicAndDescription
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseObligationOwner
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseObligationOwnerEmailId
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseEscalationOwner
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string ClauseEscalationOwnerEmailId
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string ClauseImpactConsequences
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Clause Specific Fields

        #region Test Entities

        public static string AgreementForClauses
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementForUpload
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementForEdit
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementForRevised
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementPackageForClauses
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementPackageForEdit
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementPackageForRevised
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AccountForAliases
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AccountForEdit
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AccountForLegalHold
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string TestAccount
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string TestAgreementFile
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string TestClause
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Test Entities

        #region Test Subscription Level Entities

        public static string Account_Bronze
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Agreement_Bronze
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string AgreementPackage_Bronze
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementFile_Bronze
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Account_Silver
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Agreement_Silver
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementPackage_Silver
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementFile_Silver
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Account_Gold
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Agreement_Gold
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementPackage_Gold
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementFile_Gold
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Account_Diamond
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Agreement_Diamond
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementPackage_Diamond
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementFile_Diamond
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Account_Platinum
        {
            get { return GetEnvironmentSetting(); }
        }
        public static string Agreement_Platinum
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementPackage_Platinum
        {
            get { return GetEnvironmentSetting(); }
        }

        public static string AgreementFile_Platinum
        {
            get { return GetEnvironmentSetting(); }
        }

        #endregion Test Subscription Level Entities

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
