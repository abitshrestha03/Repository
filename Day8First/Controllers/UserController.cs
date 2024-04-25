using Day8First.Models;
using Day8First.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day8First.Controllers
{
    public class UserController:Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        public UserController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        public IActionResult Home(string Title,string Language,string Genre,string Author)
        {
            List<Author> AuthorList = _authorRepository.RetrieveAuthors();
            ViewBag.AuthorList = new SelectList(AuthorList, "Username", "Username");
            IEnumerable<Book> books = _bookRepository.RetrieveBook();
            var distinctLanguages = books.Select(book => book.Language).Distinct().ToList();
            var distinctGenres = books.Select(genre => genre.Genre.Name).Distinct().ToList();
            var distinctYear = books.Select(year => year.PublishedDate.Year).Distinct().ToList();
            ViewBag.LanguageList = new SelectList(distinctLanguages);
            ViewBag.YearList = new SelectList(distinctYear);
            ViewBag.Genres = new SelectList(distinctGenres);
            IEnumerable<Book> reversedData = books.Reverse();
            if (!string.IsNullOrEmpty(Title))
            {
                reversedData = reversedData.Where(x => x.Title.StartsWith(Title, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Author))
            {
                reversedData = reversedData.Where(x => x.Author.Username.StartsWith(Author, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Language))
            {
                reversedData = reversedData.Where(x => x.Language.StartsWith(Language, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(Genre))
            {
                reversedData=reversedData.Where(x=>x.Genre.Name.StartsWith(Genre,StringComparison.OrdinalIgnoreCase));
            }
            var filteredData = reversedData.ToList();
            if (filteredData.Any())
            {
                return View(filteredData);
            }
            return View(books);
        }
    }
}
