using Apivscode2.Interfaces;
using Apivscode2.Models;
using Dapper;
using Npgsql;
using System.Data;

namespace Apivscode2.Repository
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public TerritoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Default");
        }

        public async Task<IEnumerable<TerritoryResponseDTO>> SearchTerritoryAsync()
        {
            string sql = @"
                SELECT 
                    id,
                    producer_id AS ProducerId,
                    name,
                    area,
                    type_territory
                FROM public.territory;
            ";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryAsync<TerritoryResponseDTO>(sql);
        }

        public async Task<TerritoryResponseDTO> SearchTerritoryByIdAsync(int id)
        {
            string sql = @"
                SELECT 
                    id,
                    producer_id AS ProducerId,
                    name,
                    area,
                    type_territory
                FROM public.territory
                WHERE id = @Id;
            ";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<TerritoryResponseDTO>(sql, new { Id = id });
        }

        public async Task<bool> AddTerritoryAsync(TerritoryRequestDTO request)
        {
            string sql = @"
                INSERT INTO public.territory
                (producer_id, name, area, type_territory)
                VALUES
                (@ProducerId, @Name, @Area, @Type);
            ";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new
            {
                request.ProducerId,
                request.Name,
                request.Area,
                request.Type
            });

            return rows > 0;
        }

        public async Task<bool> UpdateTerritoryAsync(TerritoryRequestDTO request, int id)
        {
            string sql = @"
                UPDATE public.territory
                SET
                    producer_id = @ProducerId,
                    name = @Name,
                    area = @Area,
                    type_territory = @Type
                WHERE id = @Id;
            ";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new
            {
                request.ProducerId,
                request.Name,
                request.Area,
                request.Type,
                Id = id
            });

            return rows > 0;
        }

        public async Task<bool> DeleteTerritoryAsync(int id)
        {
            string sql = @"DELETE FROM public.territory WHERE id = @Id;";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new { Id = id });

            return rows > 0;
        }
    }
}
