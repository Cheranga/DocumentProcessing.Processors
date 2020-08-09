using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using DocumentProcessing.Processors.Configs;
using Microsoft.Extensions.Logging;

namespace DocumentProcessing.Processors.Services
{
    public class BlobService : IBlobService
    {
        private readonly ILogger<BlobService> _logger;
        private readonly SecureStorageConfiguration _storageConfiguration;

        public BlobService(SecureStorageConfiguration storageConfiguration, ILogger<BlobService> logger)
        {
            _storageConfiguration = storageConfiguration;
            _logger = logger;
        }

        public async Task<string> GetBlobContentAsync(string blobName)
        {
            try
            {
                var blobEndpoint = $"https://{_storageConfiguration.Account}.blob.core.windows.net";

                var blobServiceClient = new BlobServiceClient(new Uri(blobEndpoint), new DefaultAzureCredential());

                var blobClient = blobServiceClient.GetBlobContainerClient(_storageConfiguration.Container).GetBlobClient(blobName);
                var downloadInformation = await blobClient.DownloadAsync();
                if (downloadInformation?.Value?.Content == null)
                {
                    return null;
                }

                string content;
                using (var reader = new StreamReader(downloadInformation.Value.Content, true))
                {
                    content = await reader.ReadToEndAsync();
                }

                return content;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error when getting content from the blob.");
            }

            return string.Empty;
        }
    }
}
