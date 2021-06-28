using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using Vantage.Automation.LegalFlowUITest.Context;
using Vantage.Automation.LegalFlowUITest.Helpers;

namespace Vantage.Automation.LegalFlowUITest.Steps
{
    [Binding]
    public class RoutingCountrySteps
    {
        private readonly UIContext _uiContext;
        private readonly RoutingRuleContext _routingRuleContext;
        private readonly AdvancedFindHelper _advancedFindHelper;

        private const string _serviceCatalogKey = "Service Catalog";
        private const string _routeToTypeKey = "Route To Type";
        private const string _urgentPriorityRouteToPersonKey = "Urgent Priority Route To Person";
        private const string _urgentPriorityRouteToTeamKey = "Urgent Priority Route To Team";
        private const string _routeToPersonKey = "Route To Person";
        private const string _routeToTeamKey = "Route To Team";
        private const string _countryKey = "Country";
        private const string _regionKey = "Region";
        private const string _nameKey = "Name";
        private const string _parentServiceCatalogKey = "Parent Service Catalog";

        public RoutingCountrySteps(UIContext context, RoutingRuleContext routingRuleContext)
        {
            _routingRuleContext = routingRuleContext;
            _uiContext = context;
            _advancedFindHelper = new AdvancedFindHelper(context);
        }

        [StepDefinition("I find a Country that routes to Team")]
        public void CountryRoutesToTeam()
        {
            _advancedFindHelper.GetCountryRoutingRules();
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.CountryRoutingRules.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Team"
                                                              && string.IsNullOrEmpty(x[_urgentPriorityRouteToTeamKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_routeToTeamKey].ToString()));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Country that routes to Team");
            }

            _routingRuleContext.RequestType = GetRequestTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.SubType = GetSubTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.RouteToTeam = catalog[_routeToTeamKey].ToString();
            _routingRuleContext.Country = catalog[_countryKey].ToString();
            _routingRuleContext.Region = GetRegionFromCountry(catalog[_countryKey].ToString());
            _routingRuleContext.State = GetStateFromCountry(catalog[_countryKey].ToString());
        }

        [StepDefinition("I find a Country that routes to Person")]
        public void CountryRoutesToPerson()
        {
            _advancedFindHelper.GetCountryRoutingRules();
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.CountryRoutingRules.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Person"
                                                              && string.IsNullOrEmpty(x[_urgentPriorityRouteToPersonKey].ToString())
                                                              && !string.IsNullOrEmpty(x[_routeToPersonKey].ToString()));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Country that routes to Person");
            }

            _routingRuleContext.RequestType = GetRequestTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.SubType = GetSubTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.RouteToPerson = catalog[_routeToPersonKey].ToString();
            _routingRuleContext.Country = catalog[_countryKey].ToString();
            _routingRuleContext.Region = GetRegionFromCountry(catalog[_countryKey].ToString());
            _routingRuleContext.State = GetStateFromCountry(catalog[_countryKey].ToString());
        }

        [StepDefinition("I find a Country that routes to Team with Priority")]
        public void CountryRoutesToTeamWithPriority()
        {
            _advancedFindHelper.GetCountryRoutingRules();
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.CountryRoutingRules.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Team"
                                                              && !string.IsNullOrEmpty(x[_urgentPriorityRouteToTeamKey].ToString()));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Country that routes to Team with Priority");
            }

            _routingRuleContext.RequestType = GetRequestTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.SubType = GetSubTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.RouteToTeam = catalog[_urgentPriorityRouteToTeamKey].ToString();
            _routingRuleContext.Country = catalog[_countryKey].ToString();
            _routingRuleContext.Region = GetRegionFromCountry(catalog[_countryKey].ToString());
            _routingRuleContext.State = GetStateFromCountry(catalog[_countryKey].ToString());
        }

        [StepDefinition("I find a Country that routes to Person with Priority")]
        public void CountryRoutesToPersonWithPriority()
        {
            _advancedFindHelper.GetCountryRoutingRules();
            _advancedFindHelper.GetServiceCatalogs();

            var catalog = RoutingRuleContext.CountryRoutingRules.FirstOrDefault(x => x[_routeToTypeKey].ToString() == "Person"
                                                              && !string.IsNullOrEmpty(x[_urgentPriorityRouteToPersonKey].ToString()));

            if (catalog == null)
            {
                Assert.Inconclusive("Could not find a Country that routes to Person With Priority");
            }

            _routingRuleContext.RequestType = GetRequestTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.SubType = GetSubTypeFromServiceCatalog(catalog[_serviceCatalogKey].ToString());
            _routingRuleContext.RouteToPerson = catalog[_urgentPriorityRouteToPersonKey].ToString();
            _routingRuleContext.Country = catalog[_countryKey].ToString();
            _routingRuleContext.Region = GetRegionFromCountry(catalog[_countryKey].ToString());
            _routingRuleContext.State = GetStateFromCountry(catalog[_countryKey].ToString());
        }

        private string GetRegionFromCountry(string country)
        {
            var region = RoutingRuleContext.Countries.FirstOrDefault(x => x[_nameKey].ToString() == country);
            if (region == null)
            {
                Assert.Inconclusive("Could not find a Region for country " + country);
            }
            return region[_regionKey].ToString();
        }
        private string GetStateFromCountry(string country)
        {
            var state = RoutingRuleContext.StateProvinces.FirstOrDefault(x => x[_countryKey].ToString() == country);
            return state != null ? state[_nameKey].ToString() : "";
        }

        private string GetRequestTypeFromServiceCatalog(string serviceCatalog)
        {
            var sc = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_nameKey].ToString() == serviceCatalog);
            if (sc == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog for service catalog " + serviceCatalog);
            }
            if (!string.IsNullOrEmpty(sc[_parentServiceCatalogKey].ToString()))
            {
                return sc[_parentServiceCatalogKey].ToString();
            }
            return sc[_nameKey].ToString();
        }

        private string GetSubTypeFromServiceCatalog(string serviceCatalog)
        {
            var sc = RoutingRuleContext.ServiceCatalogs.FirstOrDefault(x => x[_nameKey].ToString() == serviceCatalog);
            if (sc == null)
            {
                Assert.Inconclusive("Could not find a Service Catalog for service catalog " + serviceCatalog);
            }
            if (!string.IsNullOrEmpty(sc[_parentServiceCatalogKey].ToString()))
            {
                return sc[_nameKey].ToString();
            }
            return "";
        }


        private string ReplaceCountryRoutingValues(string value)
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
