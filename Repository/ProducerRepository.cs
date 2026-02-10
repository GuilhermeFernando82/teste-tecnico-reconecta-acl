using Apivscode2.Interfaces;
using Apivscode2.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Apivscode2.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public ProducerRepository(IConfiguration configuration){
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Default");
        }
        public async Task<IEnumerable<ProducerResponseDTO>> SearchProducerAsync()
        {
            string sql = @"
                SELECT 
                    id,
                    name,
                    document,
                    status,
                    date_registration AS dateRegistration
                FROM public.producer;
            ";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryAsync<ProducerResponseDTO>(sql);
        }

        public async Task<ProducerResponseDTO> SearcProducerByIdAsync(int id)
        {
            string sql = @"
                SELECT
                    id,
                    name,
                    document,
                    status,
                    date_registration AS dateRegistration
                FROM public.producer
                WHERE id = @Id;
            ";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<ProducerResponseDTO>(sql, new { Id = id });
        }
        public async Task<bool> UpdateProducer(ProducerRequestDTO request, int id)
        {
            string sql = @"
                UPDATE public.producer
                SET
                    name = @Name,
                    document = @Document,
                    status = @Status,
                    date_registration = @DateRegistration
                WHERE id = @Id;
            ";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new
            {
                request.Name,
                request.Document,
                request.Status,
                request.DateRegistration,
                Id = id
            });

            return rows > 0;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            string sql = @"DELETE FROM public.producer WHERE id = @Id";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new { Id = id });

            return rows > 0;
        }
        public async Task<bool> AddProducerAsync(ProducerRequestDTO request)
        {
            string sql = @"
                INSERT INTO public.producer
                (name, document, status, date_registration)
                VALUES
                (@Name, @Document, @Status, @DateRegistration);
            ";

            using var con = new NpgsqlConnection(connectionString);

            var rows = await con.ExecuteAsync(sql, new
            {
                request.Name,
                request.Document,
                request.Status,
                request.DateRegistration

            });

            return rows > 0;
        }
    }
}