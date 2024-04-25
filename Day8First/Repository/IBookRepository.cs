using Day8First.Models;

namespace Day8First.Repository
{
    public interface IBookRepository
    {
        public List<Book> RetrieveBook();
        public void InsertBook(Book book);
        public void DeleteBook(int id);
        public Book GetSingleBook(int id);
        public void EditBook(Book book);

    }
}
