using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MVCProject.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MVCProject.Services
{
    public class SchoolServices : ISchoolServices
    {
        private readonly HttpClient _client;
        private readonly ILogger<SchoolServices> _logger;
        private readonly string _studentsBaseUrl = "https://localhost:7016/api/Students";
        private readonly string _transactionsBaseUrl = "https://localhost:7016/api/Transactions";

        public SchoolServices(HttpClient client, ILogger<SchoolServices> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<List<StudentM>> GetStudents()
        {
            var response = await _client.GetAsync(_studentsBaseUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var students = JsonSerializer.Deserialize<List<StudentM>>(responseBody, options);
            return students;
        }

        public async Task<StudentM> GetStudent(int id)
        {
            var response = await _client.GetAsync($"{_studentsBaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var student = JsonSerializer.Deserialize<StudentM>(responseBody, options);
            return student;
        }

        public async Task<List<TransactionM>> GetTransactions()
        {
            var response = await _client.GetAsync(_transactionsBaseUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var transactions = JsonSerializer.Deserialize<List<TransactionM>>(responseBody, options);
            return transactions;
        }

        public async Task AddTransaction(TransactionM transaction)
        {
            var response = await _client.PostAsJsonAsync(_transactionsBaseUrl, transaction);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddStudent(StudentM student)
        {
            var response = await _client.PostAsJsonAsync(_studentsBaseUrl, student);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error adding student: {responseBody}");
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task RechargeAccount(int studentId, decimal amount)
        {
            var response = await _client.PostAsJsonAsync($"{_studentsBaseUrl}/{studentId}/recharge", new { amount });
            response.EnsureSuccessStatusCode();
        }

        public async Task<decimal> GetBalance(int accountId)
        {
            var response = await _client.GetAsync($"{_studentsBaseUrl}/balance/{accountId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<decimal>(responseBody);
        }

        public async Task Print(int accountId, int numberOfPages)
        {
            var printRequest = new PrintRequestM
            {
                AccountID = accountId,
                NumberOfPages = numberOfPages
            };

            var response = await _client.PostAsJsonAsync($"{_transactionsBaseUrl}/print", printRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
