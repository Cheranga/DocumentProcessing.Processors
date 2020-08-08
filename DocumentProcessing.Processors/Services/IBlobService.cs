using System.Threading.Tasks;

namespace DocumentProcessing.Processors.Services
{
    public interface IBlobService
    {
        Task<bool> UploadBlobAsync(string documentId, string data);
        Task<string> GetBlobContentAsync(string blobName);
    }
}