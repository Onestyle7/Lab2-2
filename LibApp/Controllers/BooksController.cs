using LibApp.Data;
using LibApp.Models;
using LibApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        public BooksController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: BooksController
        public ActionResult Index()
        {
            var books = _context.Books.Include(b => b.Genre).ToList();
            return View(books);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Genre)
                .SingleOrDefault(b => b.Id == id);

            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Random
        public IActionResult Random()
        {
            var firstBook = new Book() { Author = "Random author", Title = "Random title" };
            
            // Use for alternative ways of passing data to views
            //ViewData["Book"] = firstBook;
            //ViewBag.Book = firstBook;

            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomBookViewModel
            {
                Book = firstBook,
                Customers = customers
            };

            return View(viewModel);
            //return RedirectToAction("Random", "Books");
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Create(NewBookViewModel viewModel)
        {
            if (viewModel.Book.Id == 0)
            {
                //_context.Customers.Add(viewModel.Book);
            }
            else
            {
                var customerInDb = _context.Books.Single(c => c.Id == viewModel.Book.Id);

                customerInDb.Title = viewModel.Book.Title;
                customerInDb.Author = viewModel.Book.Author;
                customerInDb.Genre.Id = viewModel.Book.Genre.Id;
                customerInDb.NumberInStock = viewModel.Book.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Book");
        }
        public IActionResult NewBook(IEnumerable<Genre> genre)
        {
            var NewBook = _context.Books.ToList();
            NewBookViewModel viewModel = new NewBookViewModel { Genre = genre };

            return View();
        }

        private ApplicationDbContext _context;
    }
}
