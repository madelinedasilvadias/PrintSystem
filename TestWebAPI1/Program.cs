using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TestWebAPI1.Models;

namespace TestWebAPI1
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            try
            {
                // Get all students
                using HttpResponseMessage response = await client.GetAsync("https://localhost:7016/api/students");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var students = JsonSerializer.Deserialize<List<StudentM>>(responseBody, options);
                foreach (var s in students)
                {
                    Console.WriteLine($"ID: {s.Id}");
                    Console.WriteLine($"First Name: {s.FirstName}");
                    Console.WriteLine($"Last Name: {s.LastName}");
                    Console.WriteLine($"Email: {s.Email}");
                    Console.WriteLine($"Date of Birth: {s.DateOfBirth}");
                }

                // Add a new student
                HttpResponseMessage sendingStudent = await client.PostAsJsonAsync("https://localhost:7016/api/students",
                    new StudentM
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "johndoe@example.com",
                        DateOfBirth = new DateTime(1990, 1, 1)
                    });
                string response1 = await sendingStudent.Content.ReadAsStringAsync();
                Console.WriteLine(response1);

                // Add a new transaction for a student
                HttpResponseMessage sendingTransaction = await client.PostAsJsonAsync("https://localhost:7016/api/transactions",
                    new TransactionM
                    {
                        StudentId = 1,
                        Amount = 100.0m,
                        TransactionDate = DateTime.Now
                    });
                string response2 = await sendingTransaction.Content.ReadAsStringAsync();
                Console.WriteLine(response2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("message: {0}", ex.Message);
            }

            Console.ReadLine();
        }
    }
}