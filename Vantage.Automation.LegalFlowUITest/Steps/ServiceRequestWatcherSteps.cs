using System.Linq;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ServiceRequestWatcherSteps
    {
        private readonly UIContext _uiContext;

        public ServiceRequestWatcherSteps(UIContext context)
        {
            _uiContext = context;
        }

        [When("I add watcher '(.*)' to the Service Request")]
        public void IAddAWatcherToTheServiceRequest(string fieldValue)
        {
            StringTransformer transformer = new StringTransformer();
            fieldValue = transformer.Transform(fieldValue);

            _uiContext.XrmApp.Entity.SubGrid.ClickCommand("subgrid_participants", "New Service Request Watcher");

            var dynamicFieldHelper = new DynamicFieldHelper(_uiContext);

            var fieldNameRelatedUser = dynamicFieldHelper.GetUnderlyingFieldName("Related User", true);
            _uiContext.XrmApp.QuickCreate.SetValue(new LookupItem { Name = fieldNameRelatedUser, Value = fieldValue, Index = 0 });

            _uiContext.XrmApp.QuickCreate.Save();
        }

        [When("I open watcher '(.*)'")]
        public void IOpenWatcher(string fieldValue)
        {
            StringTransformer transformer = new StringTransformer();
            fieldValue = transformer.Transform(fieldValue);

            var subGridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems("subgrid_participants").ToArray();
            for (int i = 0; i < subGridItems.Length; i++)
            {
                if (subGridItems[i].GetAttribute<string>("ulx_name") == fieldValue)
                {
                    _uiContext.XrmApp.Entity.SubGrid.OpenSubGridRecord("subgrid_participants", i);
                    break;
                }
            }
        }

        [Then("I verify there is a watcher with Name '(.*)' and Collaborating '(.*)'")]
        public void VerifyWatcher(string watcherName, string collaborator)
        {
            StringTransformer transformer = new StringTransformer();
            watcherName = transformer.Transform(watcherName);

            var subGridItems = _uiContext.XrmApp.Entity.SubGrid.GetSubGridItems("subgrid_participants").ToArray();

            bool watcherExists = subGridItems.Any(x => x.GetAttribute<string>("ulx_name") == watcherName && x.GetAttribute<string>("ulx_participating") == collaborator);
            Assert.IsTrue(watcherExists, "Watcher with name '{0}' and Collaborating '{1}' does not exist", watcherName, collaborator);
        }
    }
}
