using System;
using System.Collections.Generic;
using System.IO;
using Vantage.Automation.LegalFlowUITest.Context;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public class AdvancedFindHelper
    {
        private readonly UIContext _uiContext;

        private string _fileNameCountryRoutingRules;
        private string _fileNameCountries;
        private string _fileNameStateProvinces;
        private string _fileNameServiceCatalogs;
        private string _fileNameSubStatuses;

        public AdvancedFindHelper(UIContext context)
        {
            _uiContext = context;

            string envAndDate = string.Concat(ConfigHelper.TestEnvironment, "_", DateTime.UtcNow.ToString("MMdd"));
            _fileNameCountryRoutingRules = string.Concat(envAndDate, "_CountryRoutingRules.dat");
            _fileNameCountries = string.Concat(envAndDate, "_Countries.dat");
            _fileNameStateProvinces = string.Concat(envAndDate, "_StateProvinces.dat");
            _fileNameServiceCatalogs = string.Concat(envAndDate, "_ServiceCatalogs.dat");
            _fileNameSubStatuses = string.Concat(envAndDate, "_SubStatuses.dat");
        }

        public void GetCountryRoutingRules()
        {
            if (RoutingRuleContext.CountryRoutingRules == null)
            {
                if (File.Exists(_fileNameCountryRoutingRules))
                {
                    RoutingRuleContext.CountryRoutingRules = ReadFromBinaryFile<List<Dictionary<string, object>>>(_fileNameCountryRoutingRules);
                }
                else
                {
                    RoutingRuleContext.CountryRoutingRules = _uiContext.XrmApp.AdvancedFind.Find("Country Routing Rules", true);
                    WriteToBinaryFile<List<Dictionary<string, object>>>(_fileNameCountryRoutingRules, RoutingRuleContext.CountryRoutingRules);
                }
            }
            if (RoutingRuleContext.Countries == null)
            {
                if (File.Exists(_fileNameCountries))
                {
                    RoutingRuleContext.Countries = ReadFromBinaryFile<List<Dictionary<string, object>>>(_fileNameCountries);
                }
                else
                {
                    RoutingRuleContext.Countries = _uiContext.XrmApp.AdvancedFind.Find("Countries", false);
                    WriteToBinaryFile<List<Dictionary<string, object>>>(_fileNameCountries, RoutingRuleContext.Countries);
                }
            }
            if (RoutingRuleContext.StateProvinces == null)
            {
                if (File.Exists(_fileNameStateProvinces))
                {
                    RoutingRuleContext.StateProvinces = ReadFromBinaryFile<List<Dictionary<string, object>>>(_fileNameStateProvinces);
                }
                else
                {
                    RoutingRuleContext.StateProvinces = _uiContext.XrmApp.AdvancedFind.Find("State/Provinces", false);
                    WriteToBinaryFile<List<Dictionary<string, object>>>(_fileNameStateProvinces, RoutingRuleContext.StateProvinces);
                }
            }            
        }

        public void GetServiceCatalogs()
        {
            if (RoutingRuleContext.ServiceCatalogs == null)
            {
                if (File.Exists(_fileNameServiceCatalogs))
                {
                    RoutingRuleContext.ServiceCatalogs = ReadFromBinaryFile<List<Dictionary<string, object>>>(_fileNameServiceCatalogs);
                }
                else
                {
                    RoutingRuleContext.ServiceCatalogs = _uiContext.XrmApp.AdvancedFind.Find("Service Catalogs", true);
                    WriteToBinaryFile<List<Dictionary<string, object>>>(_fileNameServiceCatalogs, RoutingRuleContext.ServiceCatalogs);
                }
            }
        }

        public void GetSubStatuses()
        {
            if (RoutingRuleContext.SubStatuses == null)
            {
                if (File.Exists(_fileNameSubStatuses))
                {
                    RoutingRuleContext.SubStatuses = ReadFromBinaryFile<List<Dictionary<string, object>>>(_fileNameSubStatuses);
                }
                else
                {
                    RoutingRuleContext.SubStatuses = _uiContext.XrmApp.AdvancedFind.Find("Sub Statuses", true);
                    WriteToBinaryFile<List<Dictionary<string, object>>>(_fileNameSubStatuses, RoutingRuleContext.SubStatuses);
                }
            }
        }

        private void WriteToBinaryFile<T>(string filePath, T objectToWrite)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
