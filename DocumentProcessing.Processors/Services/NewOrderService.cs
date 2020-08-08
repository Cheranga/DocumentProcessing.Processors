using System;
using System.Threading.Tasks;
using DocumentProcessing.Processors.DTO;
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
            // TODO: Handle new orders
            //
            var status = (new Random().Next(3)) % 2 == 0;
            
            return status;
        }
    }
}