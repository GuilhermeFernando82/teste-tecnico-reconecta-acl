using Apivscode2.Models;

namespace Apivscode2.Interfaces
{
    public interface IFinancialService
    {
        Task<FinancialResponseDTO> GetFinancialSituationAsync(string document);
    }
}
