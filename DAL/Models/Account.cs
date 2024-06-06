using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Account
    {
       
        public int AccountID { get; set; }
        public int StudentID { get; set; }
        public decimal StudentBalance { get; set; }

        public decimal GetBalance()
        {
            return StudentBalance;
        }

        public void SetBalance(decimal balance)
        {
            StudentBalance = balance;
        }
    }
}
