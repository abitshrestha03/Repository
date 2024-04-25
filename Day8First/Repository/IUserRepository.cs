using Day8First.Models;

namespace Day8First.Repository
{
    public interface IUserRepository
    {
        void Create(User user);
        bool Authenticate(Author author);
    }
}
