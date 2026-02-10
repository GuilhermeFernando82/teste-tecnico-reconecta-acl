using Apivscode2.Models;
using System.Threading.Tasks;

namespace Apivscode2.Interfaces
{
    public interface IProducerRepository
    {
        Task<IEnumerable<ProducerResponseDTO>> SearchProducerAsync();
        Task<ProducerResponseDTO> SearcProducerByIdAsync(int id);
        Task<bool> UpdateProducer(ProducerRequestDTO request, int id);
        Task<bool> DeleteCustomerAsync(int id);
        Task<bool> AddProducerAsync(ProducerRequestDTO request);


    }
}