using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DocumentProcessing.Processors
{
    public class ProcessNewOrdersFunction
    {
        private readonly ILogger<ProcessNewOrdersFunction> _logger;

        public ProcessNewOrdersFunction(ILogger<ProcessNewOrdersFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName("Function1")]
        public  async Task ProcessNewOrderAsync(
            [ServiceBusTrigger("%ProcessDocumentTopic%", "%NewOrdersSubscription%")]Message message)
        {
            throw new NotImplementedException();
        }
    }
}
