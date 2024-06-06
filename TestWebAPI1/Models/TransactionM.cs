using TestWebAPI1.Models;

namespace WebAPINormal.Models
{
    public class TransactionM
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
