using System;
using System.Threading.Tasks;
using DocumentProcessing.Processors.DTO;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DocumentProcessing.Processors.Services
{
    public class NewOrderService : INewOrderService
    {
        private readonly IBlobService _blobService;
        private readonly ILogger<NewOrderService> _logger;

        public NewOrderService(IBlobService blobService, ILogger<NewOrderService> logger)
        {
            _blobService = blobService;
            _logger = logger;
        }

        public async Task<bool> HandleAsync(ProcessDocumentMessage message)
        {
            ProcessDocumentRequest processDocumentRequest;
            if (!string.IsNullOrWhiteSpace(message.RequestReferenceId))
            {
                var content = await _blobService.GetBlobContentAsync(message.RequestReferenceId);
                if (string.IsNullOrWhiteSpace(content))
                {
                    return false;
                }

                processDocumentRequest = JsonConvert.DeserializeObject<ProcessDocumentRequest>(content);
                
            }
            else
            {
                processDocumentRequest = message.Request;
            }
            //
            // TODO: Handle the new order
            //

            _logger.LogInformation($"Request handled successfully: {JsonConvert.SerializeObject(processDocumentRequest)}");

            return true;
        }
    }
}