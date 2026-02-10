namespace Apivscode2.Models
{
    public class FinancialResponseDTO
    {
        public string Document { get; set; }
        public int Score { get; set; }
        public string Situation { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
