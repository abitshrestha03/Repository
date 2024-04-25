using Day8First.Data;
using Day8First.Models;
using Microsoft.EntityFrameworkCore;

namespace Day8First.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Book> RetrieveBook()
        {
            List<Book> books = _dbContext.Books.Include(book=>book.Genre).ToList();
            return books;
        }
        public void InsertBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        public void DeleteBook(int id)
        {
            _dbContext.Remove(_dbContext.Books.FirstOrDefault(a=>a.Id==id));
            _dbContext.SaveChanges();
        }

        public Book GetSingleBook(int id)
        {
            Book editBook = _dbContext.Books.FirstOrDefault(b=>b.Id==id);
            return editBook;
        }

        public void EditBook(Book book)
        {
            Book editedBook = _dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
            editedBook.Title = book.Title;
            editedBook.Language = book.Language;
            editedBook.PublishedDate = book.PublishedDate;
            editedBook.GenreId = book.GenreId;
            editedBook.Image = book.Image;
            _dbContext.SaveChanges();
        }
    }
}
