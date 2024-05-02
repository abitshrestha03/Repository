using Day8First.Data;
using Day8First.Models;
using Microsoft.EntityFrameworkCore;

namespace Day8First.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly SessionService _sessionService;
        public UserRepository(ApplicationDbContext dbContext,SessionService sessionService)
        {
            _dbContext=dbContext;
            _sessionService = sessionService;
        }

        public bool Authenticate(Author author)
        {
            var IsUserEmailFound = _dbContext.Users.FirstOrDefault(a => a.Email == author.Email);
            if (IsUserEmailFound == null)
            {
                return false;
            }
            var IsPasswordCorrect = _dbContext.Users.Where(IsUserEmailFound.Password==author.Password);
            if (IsPasswordCorrect == null)
            {
                return false;
            }
            _sessionService.SetUserSessionValue(IsUserEmailFound);
            return true;
        }

        public void Create(User user)
        {
            _dbContext.Users.Add(user);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
