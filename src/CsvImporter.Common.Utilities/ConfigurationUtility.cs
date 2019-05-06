using CsvImporter.Common.Contracts.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.Common.Utilities
{
    public class ConfigurationUtility
    {
        public static string GetConfiguration(IConfiguration configuration, string configurationKey)
        {
            var configurationValue = configuration[configurationKey];
            
            if(string.IsNullOrEmpty(configurationValue)) throw new ConfigurationNotFoundException(configurationKey);
            
            return configurationValue;
        }
    }
}