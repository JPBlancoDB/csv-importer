using CsvImporter.Common.WebJobs.Abstractions;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.Common.WebJobs.Services
{
    public class KeyVaultServices : IKeyVaultService
    {
        private readonly KeyVaultClient _keyVaultClient;
        private readonly IConfiguration _configuration;

        public KeyVaultServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _keyVaultClient = Initialize();
        }

        public string GetSecret(string key)
        {
            var value = _configuration[key];

            return string.IsNullOrEmpty(value)
                    ? SecretValueFromKeyVault(key)
                    : value;
        }

        private string SecretValueFromKeyVault(string key)
        {
            var keyVaultName = _configuration["KeyVault:Name"];

            var secret = _keyVaultClient.GetSecretAsync($"https://{keyVaultName}.vault.azure.net/", key).GetAwaiter().GetResult();

            return secret.Value;
        }

        private static KeyVaultClient Initialize()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            return new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
        }
    }

}