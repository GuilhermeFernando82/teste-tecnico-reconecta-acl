using Apivscode2.Interfaces;
using Apivscode2.Models;

namespace Apivscode2.Services
{
    public class FinancialService : IFinancialService
    {
        public async Task<FinancialResponseDTO> GetFinancialSituationAsync(string document)
        {
            await Task.Delay(1200);

            var random = new Random();
            var score = random.Next(300, 900);

            string situation;
            decimal limit;

            if (score < 400)
            {
                situation = "Restrito";
                limit = 0;
            }
            else if (score < 650)
            {
                situation = "Regular";
                limit = 1000;
            }
            else
            {
                situation = "Aprovado";
                limit = 5000;
            }

            return new FinancialResponseDTO
            {
                Document = document,
                Score = score,
                Situation = situation,
                CreditLimit = limit
            };
        }
    }
}
