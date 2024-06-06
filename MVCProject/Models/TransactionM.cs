namespace MVCProject.Models
{
    public class TransactionM
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
