using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Student
    {
        public decimal Balance;
        public int StudentID { get; set; }
        public int ClasseID { get; set; }
        public int AccountID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public Classe Classe { get; set; }
        public Account Account { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
