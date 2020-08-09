using System.Threading.Tasks;

namespace DocumentProcessing.Processors.Services
{
    public interface IBlobService
    {
        Task<string> GetBlobContentAsync(string blobName);
    }
}