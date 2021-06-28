using System;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class GenerateTestDataSteps : BaseSteps
    {
        private readonly EntityCreator _entityCreator;        

        private const string _testAccountPrefix = "TestAccount_";
        private const string _testAgreementPrefix = "TestAgreement_";
        private const string _testAgreementPackagePrefix = "TestAgreementPackage_";
        private const string _testAgreementFilePrefix = "TestAgreementFile_";

        public GenerateTestDataSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _entityCreator = new EntityCreator(context, scenarioContext);
        }

        [StepDefinition(@"I generate test data for '(.*)' Subscription Level")]
        public void GenerateTestDataForAccountLevel(string subscriptionLevel)
        {
            string postfix = GenerateTestDataCollection(subscriptionLevel);
            string accountName = _testAccountPrefix + postfix;
            string agreementName = _testAgreementPrefix + postfix;
            string agreementPackageName = _testAgreementPackagePrefix + postfix;
            string agreementFileName = _testAgreementFilePrefix + postfix;

            Console.WriteLine($"\"Account_{subscriptionLevel}\": \"{accountName}\",");
            Console.WriteLine($"\"Agreement_{subscriptionLevel}\": \"{agreementName}\",");
            Console.WriteLine($"\"AgreementPackage_{subscriptionLevel}\": \"{agreementPackageName}\",");
            Console.WriteLine($"\"AgreementFile_{subscriptionLevel}\": \"{agreementFileName}\",");
        }

        private string GenerateTestDataCollection(string subscriptionLevel)
        {
            string postfix = string.Concat(subscriptionLevel, "_", DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            string accountName = _testAccountPrefix + postfix;
            string agreementName = _testAgreementPrefix + postfix;
            string agreementPackageName = _testAgreementPackagePrefix + postfix;
            string agreementFileName = _testAgreementFilePrefix + postfix;

            _entityCreator.GenerateAccount(accountName, subscriptionLevel);
            _entityCreator.GenerateAgreement(agreementName, accountName);
            _entityCreator.GenerateAgreementPackage(agreementPackageName, accountName);
            _entityCreator.GenerateAgreementFile(agreementFileName, agreementPackageName, agreementName);

            return postfix;
        }
    }
}
