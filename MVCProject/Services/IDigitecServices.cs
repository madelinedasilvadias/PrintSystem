﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MVCProject.Models;

namespace MVCProject.Services
{
    public interface IDigitecServices
    {
        Task<List<StudentM>> GetStudents();
        Task<StudentM> GetStudent(int id);
        Task<List<TransactionM>> GetTransactions();
        Task RechargeAccount(int studentId, decimal amount);
        Task AddTransaction(TransactionM transaction);
    }
}