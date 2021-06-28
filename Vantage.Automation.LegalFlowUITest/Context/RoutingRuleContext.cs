
using System.Collections.Generic;

namespace Vantage.Automation.LegalFlowUITest.Context
{
    public class RoutingRuleContext
    {
        public static List<Dictionary<string, object>> ServiceCatalogs = null;
        public static List<Dictionary<string, object>> CountryRoutingRules = null;
        public static List<Dictionary<string, object>> Countries = null;
        public static List<Dictionary<string, object>> StateProvinces = null;
        public static List<Dictionary<string, object>> SubStatuses = null;

        public string RouteToTeam;
        public string RouteToPerson;

        public string RequestType;
        public string SubType;
        public string Region;
        public string Country;
        public string State;

        public string SpecificMandatoryFields;
        public string SpecificSection;

        public string SubStatus;

        public RoutingRuleContext()
        {
        }
    }
}
