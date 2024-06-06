using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MVCProject.Models;

namespace MVCProject.Services
{
    public class DigitecServices : IDigitecServices
    {
        private readonly HttpClient _client;
        private readonly string _studentsBaseUrl = "https://localhost:7016/api/Students";
        private readonly string _transactionsBaseUrl = "https://localhost:7016/api/Transactions"; // URL pour les transactions

        public DigitecServices(HttpClient client)
        {
            _client = client;
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
            var response = await _client.GetAsync(_transactionsBaseUrl); // Utilise l'URL correcte pour les transactions
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

        public async void PostStudent(StudentM student)
        {
            var response = await _client.PostAsJsonAsync(_studentsBaseUrl, student);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }

        public async Task RechargeAccount(int studentId, decimal amount)
        {
            var response = await _client.PostAsJsonAsync($"{_studentsBaseUrl}/{studentId}/recharge", new { amount });
            response.EnsureSuccessStatusCode();
        }
    }
}
