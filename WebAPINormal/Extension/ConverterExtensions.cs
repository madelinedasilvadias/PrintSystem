using DAL.Models;
using WebAPINormal.Models;

namespace WebAPINormal.Extension
{
    public static class ConverterExtensions
    {
        // Conversions pour Student et StudentM
        public static DAL.Models.Student ToDAL(this WebAPINormal.Models.StudentM studentM)
        {
            return new DAL.Models.Student
            {
                StudentID = studentM.StudentID,
                FirstName = studentM.FirstName,
                LastName = studentM.LastName,
                Email = studentM.Email,
                DateOfBirth = studentM.DateOfBirth,
                Balance = studentM.Balance
            };
        }

        public static WebAPINormal.Models.StudentM ToModel(this DAL.Models.Student student)
        {
            return new WebAPINormal.Models.StudentM
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Balance = student.Balance
            };
        }

        // Conversions pour Transaction et TransactionM
        public static DAL.Models.Transaction ToDAL(this WebAPINormal.Models.TransactionM transactionM)
        {
            return new DAL.Models.Transaction
            {
                TransactionID = transactionM.TransactionID,
                AccountID = transactionM.AccountID,
                Date = transactionM.Date,
                Amount = transactionM.Amount
            };
        }

        public static WebAPINormal.Models.TransactionM ToModel(this DAL.Models.Transaction transaction)
        {
            return new WebAPINormal.Models.TransactionM
            {
                TransactionID = transaction.TransactionID,
                AccountID = transaction.AccountID,
                Date = transaction.Date,
                Amount = transaction.Amount
            };
        }
        
    }
}
