using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace LvivCompany.Bookstore.BusinessLogic
{
    public static class AzureKeyVaultExtensions
    {
        public static IConfigurationBuilder AddAzureKeyVaults(this IConfigurationBuilder builder, IHostingEnvironment env, IConfiguration config)
        {
            if (env.IsStaging())
            {
                builder.AddAzureKeyVault(
                   $"https://{config["azureKeyVaultforDB:vault"]}.vault.azure.net/",
                   config["azureKeyVaultforDB:clientId"],
                   config["azureKeyVaultforDB:clientSecret"]);
            }

            builder.AddAzureKeyVault(
            $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
            config["azureKeyVault:clientId"],
            config["azureKeyVault:clientSecret"]);
            return builder;
        }
    }
}