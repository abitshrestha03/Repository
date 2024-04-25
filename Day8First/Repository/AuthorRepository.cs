using Day8First.Data;
using Day8First.Models;

namespace Day8First.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly SessionService _sessionService;
        public AuthorRepository(ApplicationDbContext dbContext,SessionService sessionService)
        {
            _dbContext = dbContext;
            _sessionService = sessionService;
        }

        public bool Authenticate(Author author)
        {
            var IsAuthorEmailFound = _dbContext.Authors.FirstOrDefault(a => a.Email == author.Email);
            if (IsAuthorEmailFound == null)
            {
                return false;
            }
            var IsPasswordCorrect = _dbContext.Authors.FirstOrDefault(a => a.Password == author.Password);
            if (IsPasswordCorrect == null) {
                return false;
            }
            _sessionService.SetAuthorSessionValue(IsAuthorEmailFound);
            return true;
        }

        public void Create(Author author)
        {
            _dbContext.Authors.Add(author);
            Save();
        }

        public void InsertGenreWithBook(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public List<Author> RetrieveAuthors()
        {
            List<Author> authors = _dbContext.Authors.ToList();
            return authors;
        }
    }
}
