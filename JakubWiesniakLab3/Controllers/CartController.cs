using JakubWiesniakLab3.Models;
using JakubWiesniakLab3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JakubWiesniakLab3.Controllers
{
    public class CartController : Controller
    {
        private readonly IBookRepository _bookrepository;
        private const string ItemsList = "ItemsList";

        public CartController(IBookRepository bookRepository)
        {
            _bookrepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sessionItems = HttpContext.Session.GetString(ItemsList);
            // Weryfikacja czy nasza sesja jest pusta jeśli tak tworzymy pusty Enumerable jeśli nie to Deserializujemy na listę naszą sesję
            var items = string.IsNullOrEmpty(sessionItems)
                ? Enumerable.Empty<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(sessionItems);

            return View(items);
        }


        [HttpPost]
        public IActionResult AddItem(int itemId)
        {
            var book = _bookrepository.Get(itemId);

            if (book == null)
                return BadRequest();

            var sessionItems = HttpContext.Session.GetString(ItemsList);
            var items = string.IsNullOrEmpty(sessionItems)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(sessionItems);

            var cartItem = items.FirstOrDefault(i => i.Id == book.Id);

            if (cartItem == null)
            {
                items.Add(new CartItem()
                {
                    Name = book.Name,
                    Price = book.Price,
                    Id = book.Id
                });
            }
            
            var serializedItems = JsonSerializer.Serialize(items);
            HttpContext.Session.SetString(ItemsList, serializedItems);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteItem(int itemId)
        {
            var book = _bookrepository.Get(itemId);

            if (book == null)
                return BadRequest();

            var sessionItems = HttpContext.Session.GetString(ItemsList);
            var items = string.IsNullOrEmpty(sessionItems)
                ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(sessionItems);

            items = items.Where(i => i.Id != book.Id).ToList();

            
            var serializedItems = JsonSerializer.Serialize(items);
            HttpContext.Session.SetString(ItemsList, serializedItems);

            return RedirectToAction("Index");
        }


        public IActionResult CreateOrder()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateOrder([Bind("Phone,City,Address,Id,Date")] Order order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.Id = Guid.NewGuid();
            order.Date = DateTime.Now;
            HttpContext.Session.SetString(ItemsList, "");

            return View("PlacedOrder", order);
        }


    }
}
