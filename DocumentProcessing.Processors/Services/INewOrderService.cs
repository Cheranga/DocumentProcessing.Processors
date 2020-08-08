using System.Threading.Tasks;
using DocumentProcessing.Processors.DTO;

namespace DocumentProcessing.Processors.Services
{
    public interface INewOrderService
    {
        Task<bool> HandleAsync(ProcessDocumentMessage message);
    }
}