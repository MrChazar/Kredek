using JakubWiesniakLab3.Models;
using JakubWiesniakLab3.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JakubWiesniakLab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookrepository;

        public HomeController(ILogger<HomeController> logger, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookrepository = bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AllBooks() 
        {
            return View(_bookrepository.GetAll());
        }

        public IActionResult BookDetails(int id) 
        {
            return View(_bookrepository.Get(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}