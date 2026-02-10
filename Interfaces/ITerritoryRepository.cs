using Apivscode2.Models;

namespace Apivscode2.Interfaces
{
    public interface ITerritoryRepository
    {
        Task<IEnumerable<TerritoryResponseDTO>> SearchTerritoryAsync();
        Task<TerritoryResponseDTO> SearchTerritoryByIdAsync(int id);
        Task<bool> AddTerritoryAsync(TerritoryRequestDTO request);
        Task<bool> UpdateTerritoryAsync(TerritoryRequestDTO request, int id);
        Task<bool> DeleteTerritoryAsync(int id);
    }
}