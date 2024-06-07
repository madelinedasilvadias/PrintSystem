using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using DAL.Models;

namespace MVCProject.Models
{
    public class TransactionM
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Account Account { get; set; }

        public void AddMoney(decimal amount)
        {
            Account.StudentBalance += amount;
        }

        public void MakePayment(decimal amount)
        {
            if (Account.StudentBalance >= amount)
            {
                Account.StudentBalance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient balance");
            }
        }


    }
}
