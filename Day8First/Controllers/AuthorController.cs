using Day8First.Data;
using Day8First.Models;
using Day8First.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day8First.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        public AuthorController(ApplicationDbContext dbContext, IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }
        public IActionResult Home()
        {
            var isAuthor = IsAuthor();
            if (isAuthor)
            {
                List<Book> books = _bookRepository.RetrieveBook();
                var GenreList = _dbContext.Genres.ToList();
                var AuthorList = _dbContext.Authors.ToList();
                ViewBag.GenreList = new SelectList(GenreList, "Name", "Name");
                ViewBag.AuthorList = new SelectList(AuthorList, "Username", "Username");
                return View(books);
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }
        public IActionResult Genre()
        {
            var isAuthor = IsAuthor();
            if (isAuthor)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }
        [HttpPost]
        public IActionResult CreateGenre(Genre genre)
        {
            var isAuthor = IsAuthor();
            if (isAuthor)
            {
                int AuthorId = (int)HttpContext.Session.GetInt32("AuthorId");
                foreach (var Data in genre.Books)
                {
                    Data.AuthorId = AuthorId;
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            _authorRepository.InsertGenreWithBook(genre);
            return RedirectToAction("Genre", "Author");
        }
        public IActionResult Edit(int id)
        {
            var isAuthor = IsAuthor();
            if (isAuthor)
            {
            List<Book> books = _bookRepository.RetrieveBook();
            var GenreList = _dbContext.Genres.ToList();
            ViewBag.GenreList = new SelectList(GenreList, "id", "Name");
            Book ToEditBook = _bookRepository.GetSingleBook(id);
            return View(ToEditBook);
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "User");
        }
        bool IsAuthor()
        {
            var IsAuthor = HttpContext.Session.GetString("IsAuthor");
            if (IsAuthor == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
