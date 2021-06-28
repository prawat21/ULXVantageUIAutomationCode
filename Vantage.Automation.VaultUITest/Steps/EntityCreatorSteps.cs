using System;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Context;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Steps
{
    [Binding]
    public class EntityCreatorSteps : BaseSteps
    {
        private readonly UIContext _uiContext;
        private readonly EntityCreator _entityCreator;

        public EntityCreatorSteps(UIContext context, ScenarioContext scenarioContext) : base(scenarioContext)
        {
            _uiContext = context;
            _entityCreator = new EntityCreator(context, scenarioContext);
        }

        [StepDefinition("I create a '(.*)' Account and save it as '(.*)'")]
        public void CreateAccount(string level, string saveName)
        {
            string accountName = "TestAccount " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            _entityCreator.GenerateAccount(accountName, level);
            ScenarioContext.Add(saveName, accountName);
        }

        [StepDefinition("I create a '(.*)' Account with Parent '(.*)' and save it as '(.*)'")]
        public void CreateAccountWithParent(string level, string savedParent, string saveName)
        {
            string accountName = "TestAccount " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string parentName = ScenarioContext.Get<string>(savedParent);
            _entityCreator.GenerateAccount(accountName, level, parentName);
            ScenarioContext.Add(saveName, accountName);
        }

        [StepDefinition("I create an Agreement with Account '(.*)' and save it as '(.*)'")]
        public void CreateAgreement(string account, string saveName)
        {
            string agreementName = "TestAgreement " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            account = ScenarioContext.Get<string>(account);
            _entityCreator.GenerateAgreement(agreementName, account);
            ScenarioContext.Add(saveName, agreementName);
        }

        [StepDefinition("I create an Agreement with Agreement Package '(.*)' and Account '(.*)' and save it as '(.*)'")]
        public void CreateAgreementWithPackage(string agreementPackage, string account, string saveName)
        {
            string agreementName = "TestAgreement " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            account = ScenarioContext.Get<string>(account);
            agreementPackage = ScenarioContext.Get<string>(agreementPackage);
            _entityCreator.GenerateAgreement(agreementName, account, agreementPackage);
            ScenarioContext.Add(saveName, agreementName);
        }

        [StepDefinition("I create an Agreement Package with Account '(.*)' and save it as '(.*)'")]
        public void CreateAgreementPackage(string account, string saveName)
        {
            string agreementPackageName = "TestAgreementPackage " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            account = ScenarioContext.Get<string>(account);
            _entityCreator.GenerateAgreementPackage(agreementPackageName, account);
            ScenarioContext.Add(saveName, agreementPackageName);
        }

        [StepDefinition("I create an Agreement File with Agreement '(.*)' and Agreement Package '(.*)' and save it as '(.*)'")]
        public void CreateAgreementFile(string agreement, string agreementPackage, string saveName)
        {
            string agreementFileName = "TestAgreementFile " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            agreement = ScenarioContext.Get<string>(agreement);
            agreementPackage = ScenarioContext.Get<string>(agreementPackage);
            _entityCreator.GenerateAgreementFile(agreementFileName, agreementPackage, agreement);
            ScenarioContext.Add(saveName, agreementFileName);
        }

        [StepDefinition("I create an Agreement File with (.*) Files and Agreement '(.*)' and Agreement Package '(.*)' and save it as '(.*)'")]
        public void CreateAgreementFile(int numFiles, string agreement, string agreementPackage, string saveName)
        {
            string agreementFileName = "TestAgreementFile " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            agreement = ScenarioContext.Get<string>(agreement);
            agreementPackage = ScenarioContext.Get<string>(agreementPackage);
            _entityCreator.GenerateAgreementFile(agreementFileName, agreementPackage, agreement, numFiles);
            ScenarioContext.Add(saveName, agreementFileName);
        }

        [StepDefinition("I create a Clause with Agreement '(.*)' and Agreement Package '(.*)' and Agreement File '(.*)' and save it as '(.*)'")]
        public void CreateAgreementFile(string agreement, string agreementPackage, string agreementFile, string saveName)
        {
            string clauseName = "TestClause " + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            agreement = ScenarioContext.Get<string>(agreement);
            agreementPackage = ScenarioContext.Get<string>(agreementPackage);
            agreementFile = ScenarioContext.Get<string>(agreementFile);
            _entityCreator.GenerateClause(clauseName, agreement, agreementPackage, agreementFile);
            ScenarioContext.Add(saveName, clauseName);
        }
    }
}
