using System;
using System.Threading.Tasks;
using DocumentProcessing.Processors.DTO;
using Microsoft.Azure.WebJobs.Host.Executors;
using Newtonsoft.Json;

namespace DocumentProcessing.Processors.Services
{
    public class NewOrderService : INewOrderService
    {
        private readonly IBlobService _blobService;

        public NewOrderService(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<bool> HandleAsync(ProcessDocumentMessage message)
        {
            ProcessDocumentRequest processDocumentRequest;
            if (!string.IsNullOrWhiteSpace(message.RequestReferenceId))
            {
                var content = await _blobService.GetBlobContentAsync(message.RequestReferenceId);
                processDocumentRequest = JsonConvert.DeserializeObject<ProcessDocumentRequest>(content);
            }
            else
            {
                processDocumentRequest = message.Request;
            }
            //
            // TODO: Handle the new order
            //
            return true;
        }
    }
}