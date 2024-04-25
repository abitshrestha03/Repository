using Day8First.Models;
using Day8First.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Day8First.Controllers
{
    public class BookController:Controller
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IBookRepository _bookRepository;
        public BookController(IWebHostEnvironment webHostEnvironment,IBookRepository bookRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult CreateBook(Book book,IFormFile? Image)
        {
            int authorId = (int)HttpContext.Session.GetInt32("AuthorId");
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                string productPath = Path.Combine(wwwRootPath, "Image", "Upload");
                string filePath = Path.Combine(productPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                book.Image = @"/Image/Upload/" + fileName;
                book.AuthorId = authorId;
            }
            _bookRepository.InsertBook(book);
            return RedirectToAction("Home", "Author");
        }
        public IActionResult Edit(int id)
        {
            return RedirectToAction("Home", "Author");
        }
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction("Home", "Author");
        }
        [HttpPost]
        public IActionResult EditBook(Book book, IFormFile? Image)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                string productPath = Path.Combine(wwwRootPath, "Image", "Upload");
                string filePath = Path.Combine(productPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                book.Image = @"/Image/Upload/" + fileName;
            }
            _bookRepository.EditBook(book);
            return RedirectToAction("Home", "Author");
        }
    }
}
