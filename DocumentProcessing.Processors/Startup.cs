using System;
using System.Collections.Generic;
using System.Text;
using DocumentProcessing.Processors;
using DocumentProcessing.Processors.Configs;
using DocumentProcessing.Processors.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly:FunctionsStartup(typeof(Startup))]
namespace DocumentProcessing.Processors
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
           
            services.AddSingleton(new SecureStorageConfiguration
            {
                Account = Environment.GetEnvironmentVariable("SecureStorageConfiguration.Account"),
                Container = Environment.GetEnvironmentVariable("SecureStorageConfiguration.Container")
            });

            services.AddSingleton<INewOrderService, NewOrderService>();
            services.AddSingleton<IBlobService, BlobService>();
        }
    }
}
