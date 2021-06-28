using System;
using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class UserSteps
    {
        private readonly UIContext _uiContext;
        private const string _subgrid_Languages = "subgrid_language";
        private const string _subgrid_Educations = "subgrid_education";
        private const string _subgrid_Qualifications = "subgrid_qualifications";

        public UserSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I go to view Active Users")]
        public void GoToEnabledUsers()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Users");
            _uiContext.XrmApp.Grid.SwitchView("Active Users");
        }

        [When("I go to view Legal Professional Users")]
        public void GoToViewLegalProfessionalUsers()
        {
            _uiContext.XrmApp.Navigation.OpenSubArea("Case", "Users");
            _uiContext.XrmApp.Grid.SwitchView("Legal Professional View");
        }

        [When("I open User '(.*)'")]
        public void IOpenUser(string userName)
        {
            StringTransformer transformer = new StringTransformer();
            userName = transformer.Transform(userName);
            _uiContext.XrmApp.Grid.Search(userName);
            var gridItems = _uiContext.XrmApp.Grid.GetGridItems();
            if (gridItems.Count == 0)
            {
                throw new Exception($"User with name {userName} not found");
            }
            _uiContext.XrmApp.Grid.OpenRecord(0);
        }

        [Then("I verify that the user has a Practice Area")]
        public void IVerifyUserHasPracticeArea()
        {
            var practiceArea = _uiContext.XrmApp.Entity.GetValue(new LookupItem() { Name = new DynamicFieldHelper(_uiContext).GetUnderlyingFieldName("Practice Area") });
            Assert.IsFalse(string.IsNullOrEmpty(practiceArea), "User does not have a practice area");
        }

        [When("I add a Qualification to the User with the following properties")]
        public void AddQualificationToUser(Table table)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand(_subgrid_Qualifications, "New User Qualification");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.QuickCreateFillOutFields(keyValues);

            _uiContext.XrmApp.QuickCreate.Save();
        }

        [When("I add a Language to the User with the following properties")]
        public void AddLanguageToUser(Table table)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand(_subgrid_Languages, "New User Language");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.QuickCreateFillOutFields(keyValues);

            _uiContext.XrmApp.QuickCreate.Save();
        }

        [When("I add an Education to the User with the following properties")]
        public void AddEducationToUser(Table table)
        {
            _uiContext.XrmApp.Entity.SubGrid.ClickCommand(_subgrid_Educations, "New User Education");
            FieldFiller fieldFiller = new FieldFiller(_uiContext);

            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            fieldFiller.QuickCreateFillOutFields(keyValues);

            _uiContext.XrmApp.QuickCreate.Save();
        }

        [Then("I verify that the user has at least (.*) Language Skills")]
        void IVerifyUserHasLanguageSkills(int count)
        {
            var subGridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(_subgrid_Languages);
            Assert.IsTrue(subGridItems.Count >= count, "User has {0} language skills", subGridItems.Count);
        }

        [Then("I verify that the user has at least (.*) Education Skills")]
        void IVerifyUserHasEducationSkills(int count)
        {
            var subGridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(_subgrid_Educations);
            Assert.IsTrue(subGridItems.Count >= count, "User has {0} education skills", subGridItems.Count);
        }

        [Then("I verify that the user has at least (.*) Qualification Skills")]
        void IVerifyUserHasQualificationSkills(int count)
        {
            var subGridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems(_subgrid_Qualifications);
            Assert.IsTrue(subGridItems.Count >= count, "User has {0} qualification skills", subGridItems.Count);
        }

        [Then("I verify that I can run User Activities Report")]
        public void UserActivitiesReport()
        {
            int oldTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            _uiContext.XrmApp.CommandBar.ClickCommand("Run Report", "Activities");
            _uiContext.XrmApp.Dialogs.ReportDialog(true);
            _uiContext.XrmApp.ThinkTime(5000);
            int newTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            Assert.IsTrue(newTabCount > oldTabCount, "Report was not opened");
        }

        [Then("I verify that I can run User Summary Report")]
        public void UserSummaryReport()
        {
            int oldTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            _uiContext.XrmApp.CommandBar.ClickCommand("Run Report", "User Summary");
            _uiContext.XrmApp.Dialogs.ReportDialog(true);
            _uiContext.XrmApp.ThinkTime(5000);
            int newTabCount = _uiContext.WebClient.Browser.Driver.WindowHandles.Count;
            Assert.IsTrue(newTabCount > oldTabCount, "Report was not opened");
        }
    }
}
