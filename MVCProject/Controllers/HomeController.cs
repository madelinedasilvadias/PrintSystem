using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System.Diagnostics;
using MVCProject.Services;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDigitecServices _digitecServices;

        public HomeController(ILogger<HomeController> logger, IDigitecServices digitecServices)
        {
            _logger = logger;
            _digitecServices = digitecServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Students()
        {
            var students = await _digitecServices.GetStudents();
            return View(students);
        }

        public async Task<IActionResult> RechargeAccount(int studentId, decimal amount)
        {
            await _digitecServices.RechargeAccount(studentId, amount);
            return RedirectToAction("StudentDetails", new { id = studentId });
        }

        public async Task<IActionResult> StudentDetails(int id)
        {
            var student = await _digitecServices.GetStudent(id);
            return View(student);
        }

        public async Task<IActionResult> Transactions()
        {
            var transactions = await _digitecServices.GetTransactions();
            return View(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddAmount(decimal amount, int accountId)
        {
            var transaction = new TransactionM
            {
                AccountID = accountId,
                Amount = amount,
                Date = DateTime.Now
            };
            await _digitecServices.AddTransaction(transaction);
            return RedirectToAction("Transactions");
        }

        [HttpPost]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _digitecServices.GetBalance(accountId);
            ViewBag.Balance = balance;
            var transactions = await _digitecServices.GetTransactions();
            return View("Transactions", transactions);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
