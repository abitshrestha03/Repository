using Day8First.Models;

namespace Day8First.Repository
{
    public interface IAuthorRepository
    {
        void Create(Author author);
        bool Authenticate(Author author);
        void InsertGenreWithBook(Genre genre);
        List<Author> RetrieveAuthors();
    }
}
