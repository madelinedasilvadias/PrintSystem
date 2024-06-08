using System.Collections.Generic;
using System.Threading.Tasks;
using MVCProject.Models;

namespace MVCProject.Services
{
    public interface ISchoolServices
    {
        Task<List<StudentM>> GetStudents();
        Task<StudentM> GetStudent(int id);
        Task<List<TransactionM>> GetTransactions();
        Task RechargeAccount(int studentId, decimal amount);
        Task AddTransaction(TransactionM transaction);
        Task<decimal> GetBalance(int accountId);
        Task Print(int accountId, int numberOfPages);
        Task AddStudent(StudentM student);
    }
}