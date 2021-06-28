using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class ServiceCatalogSteps
    {
        private readonly UIContext _uiContext;
        private readonly RoutingRuleContext _routingRuleContext;
        private readonly AdvancedFindHelper _advancedFindHelper;

        private const string _nameKey = "Name";
        private const string _parentServiceCatalogKey = "Parent Service Catalog";
        private const string _routingFactorKey = "Routing Factor";
        private const string _routeToTypeKey = "Route To Type";
        private const string _priorityOverrideKey = "Priority Override";
        private const string _routeToPersonKey = "Route To Person";
        private const string _routeToTeamKey = "Route To Team";
        private const string _routeRejectedToPersonKey = "Route Rejected To Person";
        private const string _routeRejectedToTeamKey = "Route Rejected To Team";
        private const string _documentRequiredKey = "Document Required";
        private const string _srMandatoryFieldsKey = "SR Catalog specific mandatory fields";
        private const string _srSpecificSectionKey = "SR Catalog specific section name";
        private const string _privilegedKey = "Privileged";
        private const string _serviceCatalogKey = "Service Catalog";
        private const string _directorOfInvestigationKey = "Director of Investigation";

        public ServiceCatalogSteps(UIContext context, RoutingRuleContext routingRuleContext)
        {
            _routingRuleContext = routingRuleContext;
            _uiContext = context;
            _advancedFindHelper = new AdvancedFindHelper(context);
        }

        [StepDefinition("I fill out the following fields for Service Catalog")]
        public void FillOutFields(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            var keyValues2 = new Dictionary<string, string>();
            foreach (string key in keyValues.Keys)
            {
                keyValues2.Add(key, ReplaceCatalogValues(keyValues[key]));
            }

            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityFillOutFields(keyValues2, false);
        }

        [StepDefinition("I fill out the following BFP fields for Service Catalog")]
        public void FillOutFieldsBPF(Table table)
        {
            var keyValues = table.Header.ToArray().ToDictionary(h => h, h => table.Rows[0][h]);
            var keyValues2 = new Dictionary<string, string>();
            foreach (string key in keyValues.Keys)
            {
                keyValues2.Add(key, ReplaceCatalogValues(keyValues[key]));
            }

            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.BusinessProcessFlowFillOutFields(keyValues2);
        }

        [Then("I verify that the Service Catalog required fields are present")]
        public void ServiceCatalogRequiredFieldsArePresent()
        {
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntityUnderlyingFieldsAreRequired(_routingRuleContext.SpecificMandatoryFields);
        }

        [Then("I verify that the Service Catalog specific sections are present")]
        public void ServiceCatalogSpecificSectionsArePresent()
        {
            FieldFiller fieldFiller = new FieldFiller(_uiContext);
            fieldFiller.EntitySectionsArePresent(_routingRuleContext.SpecificSection);
        }

        [Then("I verify that the Service Request was routed to '(.*)'")]
        public void VerifyServiceRequesOwner(string expectedOwner)
        {
            var actualOwner = _uiContext.XrmApp.Entity.GetHeaderContainerValue("Assignee");
            Assert.IsFalse(string.IsNullOrEmpty(actualOwner), "Service Request does not have an Owner");

            expectedOwner = ReplaceCatalogValues(expectedOwner);
            Assert.IsTrue(actualOwner.Contains(expectedOwner),
                "Expected Service Request Owner {0} to contain {1}" +
                "\nRequest Type: {2}" +
                "\nSub Type: {3}" +
                "\nCountry: {4}"
                , actualOwner, expectedOwner, _routingRuleContext.RequestType, _routingRuleContext.SubType, _routingRuleContext.Country);
        }

        [StepDefinition("I find a Service Catalog that routes to Team")]
        public void ServiceCatalogRoutesToTeam()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routingFactorKey].ToString() == "Service Catalog"
                                                              && x[_routeToTypeKey].ToString() == "Team"
                                                              && x[_priorityOverrideKey].ToString() == "No"
                                                              && !string.IsNullOrEmpty(x[_routeToTeamKey].ToString())
                                                              && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                              && x[_documentRequiredKey].ToString() == "No"
                                                              && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                      && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes to Team");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToTeam = catalog[_routeToTeamKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that routes to Person")]
        public void ServiceCatalogRoutesToPerson()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routingFactorKey].ToString() == "Service Catalog"
                                                              && x[_routeToTypeKey].ToString() == "Person"
                                                              && x[_priorityOverrideKey].ToString() == "No"
                                                              && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_routeToPersonKey].ToString())
                          && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes to Person");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToPerson = catalog[_routeToPersonKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that routes to Team with Priority")]
        public void ServiceCatalogRoutesToTeamWithPriority()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routingFactorKey].ToString() == "Service Catalog"
                                                              && x[_routeToTypeKey].ToString() == "Team"
                                                              && x[_priorityOverrideKey].ToString() == "Yes"
                                                              && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_routeToTeamKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                         && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes to Team with Priority");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToTeam = catalog[_routeToTeamKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that routes to Person with Priority")]
        public void ServiceCatalogRoutesToPersonWithPriority()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routingFactorKey].ToString() == "Service Catalog"
                                                              && x[_routeToTypeKey].ToString() == "Person"
                                                              && x[_priorityOverrideKey].ToString() == "Yes"
                                                              && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_routeToPersonKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                         && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes to Person with Priority");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToPerson = catalog[_routeToPersonKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that routes rejection to Team")]
        public void ServiceCatalogRoutesToRejectionTeam()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Team"
                                                                 && !string.IsNullOrEmpty(x[_routeRejectedToTeamKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                          && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes rejection to Team");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToTeam = catalog[_routeRejectedToTeamKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that routes rejection to Person")]
        public void ServiceCatalogRoutesToRejectionPerson()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Person"
                                                                 && !string.IsNullOrEmpty(x[_routeRejectedToPersonKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                          && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that routes rejection to Person");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToPerson = catalog[_routeRejectedToPersonKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that has no routing")]
        public void ServiceCatalogHasNoRouting()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => string.IsNullOrEmpty(x[_routeToTypeKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && x[_documentRequiredKey].ToString() != "Yes"
                          && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && !string.IsNullOrEmpty(y[_routeToTypeKey].ToString()))
                          && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that has no routing");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
        }

        [StepDefinition("I find a Parent Service Catalog that has mandatory fields and section")]
        public void ParentServiceCatalogMandatoryFeilds()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_srSpecificSectionKey].ToString()));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Parent Service Catalog that has mandatory fields and section");
            }
            _routingRuleContext.RequestType = catalog[_nameKey].ToString();
            _routingRuleContext.SpecificMandatoryFields = catalog[_srMandatoryFieldsKey].ToString();
            _routingRuleContext.SpecificSection = catalog[_srSpecificSectionKey].ToString();
        }

        [StepDefinition("I find a Child Service Catalog that has mandatory fields and section")]
        public void ChildServiceCatalogMandatoryFeilds()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && !string.IsNullOrEmpty(x[_srSpecificSectionKey].ToString()));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Child Service Catalog that has mandatory fields and section");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.SpecificMandatoryFields = catalog[_srMandatoryFieldsKey].ToString();
            _routingRuleContext.SpecificSection = catalog[_srSpecificSectionKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that is Privileged")]
        public void PrivilegedServiceCatalog()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && x[_privilegedKey].ToString() == "Yes"
                         && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_privilegedKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that is Privileged");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that is not Privileged")]
        public void NotPrivilegedServiceCatalog()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && x[_privilegedKey].ToString() == "No"
                        && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_privilegedKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that is not Privileged");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
        }

        [StepDefinition("I find a Service Catalog that requires Document")]
        public void ServiceCatlogRequiresDocument()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                                 && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                                 && x[_documentRequiredKey].ToString() == "Yes"
                        && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));
            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that requires a Document");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
        }

        [StepDefinition("I find a Service Catalog with a Sub Status")]
        public void ServiceCatlogWithSubStatus()
        {
            _advancedFindHelper.GetServiceCatalogs();
            _advancedFindHelper.GetSubStatuses();

            var subStatus = RoutingRuleContext.SubStatuses.FirstOrDefault(x => !string.IsNullOrEmpty(x[_serviceCatalogKey].ToString())
                            && RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_serviceCatalogKey].ToString() && !string.IsNullOrEmpty(y[_parentServiceCatalogKey].ToString()))
                            && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_serviceCatalogKey].ToString() && !string.IsNullOrEmpty(y[_srMandatoryFieldsKey].ToString()))
                            && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_serviceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes")
                            && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_serviceCatalogKey].ToString() && RoutingRuleContext.ServiceCatalogs.Any(z => z[_nameKey].ToString() == y[_parentServiceCatalogKey].ToString() && z[_documentRequiredKey].ToString() == "Yes"))
                            && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_serviceCatalogKey].ToString() && RoutingRuleContext.ServiceCatalogs.Any(z => z[_nameKey].ToString() == y[_parentServiceCatalogKey].ToString() && !string.IsNullOrEmpty(z[_parentServiceCatalogKey].ToString()))));

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_nameKey].ToString() == subStatus[_serviceCatalogKey].ToString());
            if (subStatus == null || catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog that has a sub status");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.SubStatus = subStatus[_nameKey].ToString();
        }

        [StepDefinition("I find a Service Catalog without a Sub Status")]
        public void ServiceCatlogWithoutSubStatus()
        {
            _advancedFindHelper.GetServiceCatalogs();
            _advancedFindHelper.GetSubStatuses();

             var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => 
                                                        !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString())
                                                     && string.IsNullOrEmpty(x[_srMandatoryFieldsKey].ToString())
                                                     && x[_documentRequiredKey].ToString() != "Yes"
              && !RoutingRuleContext.SubStatuses.Any(y => y[_serviceCatalogKey].ToString() == x[_nameKey].ToString())
              && !RoutingRuleContext.ServiceCatalogs.Any(y => y[_nameKey].ToString() == x[_parentServiceCatalogKey].ToString() && y[_documentRequiredKey].ToString() == "Yes"));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog without a sub status");
            }
            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
        }

        [StepDefinition("I find a Service Catalog with Director of Investigation")]
        public void ServiceCatlogWithDirectorOfInvestigation()
        {
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => !string.IsNullOrEmpty(x[_directorOfInvestigationKey].ToString()) 
                                                                            && !string.IsNullOrEmpty(x[_parentServiceCatalogKey].ToString()));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog with Director of Investigation");
            }

            _routingRuleContext.RequestType = catalog[_parentServiceCatalogKey].ToString();
            _routingRuleContext.SubType = catalog[_nameKey].ToString();
            _routingRuleContext.RouteToPerson = catalog[_directorOfInvestigationKey].ToString();
        }

        private string ReplaceCatalogValues(string value)
        {
            if (value.Contains("{"))
            {
                value = Regex.Replace(value, @"\{(.*?)\}", delegate (Match match)
                {
                    string field = match.ToString();
                    if (field.Length > 2)
                    {
                        field = GetRoutingValue(field.Substring(1, field.Length - 2));
                    }
                    return string.IsNullOrEmpty(field) ? match.ToString() : field;
                });
            }
            return value;
        }

        private string GetRoutingValue(string fieldName)
        {
            try
            {
                return _routingRuleContext.GetType().GetField(fieldName).GetValue(_routingRuleContext).ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
