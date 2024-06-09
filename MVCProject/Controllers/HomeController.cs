using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System.Diagnostics;
using MVCProject.Services;
using System.Threading.Tasks;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISchoolServices _schoolServices;

        public HomeController(ILogger<HomeController> logger, ISchoolServices schoolServices)
        {
            _logger = logger;
            _schoolServices = schoolServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Students()
        {
            var students = await _schoolServices.GetStudents();
            return View(students);
        }

        public async Task<IActionResult> Transactions()
        {
            var transactions = await _schoolServices.GetTransactions();
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
            await _schoolServices.AddTransaction(transaction);
            return RedirectToAction("Transactions");
        }

        [HttpPost]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            var balance = await _schoolServices.GetBalance(accountId);
            ViewBag.Balance = balance;
            var transactions = await _schoolServices.GetTransactions();
            return View("Transactions", transactions);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentM newStudent)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }

                var students = await _schoolServices.GetStudents();
                return View("Students", students);
            }

            try
            {
                await _schoolServices.AddStudent(newStudent);
                return RedirectToAction("Students");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");

                var students = await _schoolServices.GetStudents();
                return View("Students", students);
            }
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

        public IActionResult PrintSystem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PrintSystem(int accountId, int numberOfPages)
        {
            try
            {
                await _schoolServices.Print(accountId, numberOfPages);
                ViewBag.Message = "Print request successful!";
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            return View();
        }
    }
}
