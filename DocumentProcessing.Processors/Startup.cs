using System;
using System.Collections.Generic;
using System.Text;
using DocumentProcessing.Processors;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly:FunctionsStartup(typeof(Startup))]
namespace DocumentProcessing.Processors
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            //
            // TODO: Register dependencies.
            //
        }
    }
}
