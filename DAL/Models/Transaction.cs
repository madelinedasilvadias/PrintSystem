using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal StudentBalance { get; set; }
        public Account Account { get; set; }

        public void AddMoney(decimal amount)
        {
            StudentBalance += amount;
        }

        public void MakePayment(decimal amount)
        {
            if (StudentBalance >= amount)
            {
                StudentBalance -= amount;
            }
            else
            {
                throw new InvalidOperationException("Insufficient balance");
            }
        }

        public decimal GetStudentBalance()
        {
            return StudentBalance;
        }

        public decimal GetAmount()
        {
            return Amount;
        }
    }

}

