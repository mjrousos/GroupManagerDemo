using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace B2CDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        var builtConfig = config.Build();
                        if (context.HostingEnvironment.IsProduction())
                        {
                            var secretClient = new SecretClient(new Uri(builtConfig["KeyVaultUri"]), new DefaultAzureCredential());
                            config.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
