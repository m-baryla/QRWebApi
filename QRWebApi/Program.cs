using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
//bSs-hM-e~tsboD2KK4TXXr7~Pm8xb~oZ23
namespace QRWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfiguration = config.Build();

                    string kvURL = builtConfiguration["EmailPass:KVUrl"];
                    string tenantId = builtConfiguration["EmailPass:TenantID"];
                    string clientId = builtConfiguration["EmailPass:ClientId"];
                    string clientSecret = builtConfiguration["EmailPass:ClientSecretId"];

                    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

                    var clent = new SecretClient(new Uri(kvURL), credential);
                    config.AddAzureKeyVault(clent, new AzureKeyVaultConfigurationOptions());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
