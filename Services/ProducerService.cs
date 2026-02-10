using Apivscode2.Interfaces;
using Apivscode2.Models;

namespace Apivscode2.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly ITerritoryRepository _territoryRepository;
        private readonly IFinancialService _financialService;

        public ProducerService(
            IProducerRepository producerRepository,
            ITerritoryRepository territoryRepository,
            IFinancialService financialService)
        {
            _producerRepository = producerRepository;
            _territoryRepository = territoryRepository;
            _financialService = financialService;
        }

        public async Task<bool> ActivateProducerAsync(int producerId)
        {
            var producer = await _producerRepository.SearcProducerByIdAsync(producerId);

            if (producer == null)
                throw new Exception("Produtor não encontrado.");

            var territories = await _territoryRepository.SearchTerritoryAsync();

            if (!territories.Any(t => t.ProducerId == producerId))
                throw new Exception("Produtor não possui territórios cadastrados.");

            var financial = await _financialService
                .GetFinancialSituationAsync(producer.Document);

            if (financial.Situation != "Regular" &&
                financial.Situation != "Aprovado")
                throw new Exception("Situação financeira irregular.");

            var request = new ProducerRequestDTO
            {
                Name = producer.Name,
                Document = producer.Document,
                Status = "Ativo"
            };

            return await _producerRepository.UpdateProducer(request, producerId);
        }
    }
}
