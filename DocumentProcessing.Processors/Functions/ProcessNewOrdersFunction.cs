using System.Threading.Tasks;
using DocumentProcessing.Processors.DTO;
using DocumentProcessing.Processors.Services;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DocumentProcessing.Processors.Functions
{
    public class ProcessNewOrdersFunction
    {
        private readonly ILogger<ProcessNewOrdersFunction> _logger;
        private readonly INewOrderService _newOrderService;

        public ProcessNewOrdersFunction(INewOrderService newOrderService, ILogger<ProcessNewOrdersFunction> logger)
        {
            _newOrderService = newOrderService;
            _logger = logger;
        }

        [FunctionName(nameof(ProcessNewOrdersFunction))]
        public async Task ProcessNewOrderAsync(
            [ServiceBusTrigger("%ProcessDocumentTopic%", "%NewOrdersSubscription%")]
            ProcessDocumentMessage message,
            MessageReceiver messageReceiver, string lockToken)
        {
            if (message == null)
            {
                _logger.LogError("Message is null.");
                await messageReceiver.DeadLetterAsync(lockToken, "Message is null");
                return;
            }

            if (string.IsNullOrWhiteSpace(message.RequestReferenceId) && message.Request == null)
            {
                _logger.LogError("Message must contain a request reference id or the document request itself.");
                await messageReceiver.DeadLetterAsync(lockToken, "Message must contain a request reference id or the document request itself.");
                return;
            }

            var status = await _newOrderService.HandleAsync(message);
            if (status)
            {
                await messageReceiver.CompleteAsync(lockToken);
            }
            else
            {
                await messageReceiver.DeadLetterAsync(lockToken, "Processing of the new orders failed.");
            }
        }
    }
}