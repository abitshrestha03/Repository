using Day8First.Models;
using Day8First.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Day8First.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUserRepository _userRepository;
        public AuthController(IAuthorRepository authorRepository, IUserRepository userRepository)
        {
            _authorRepository = authorRepository;
            _userRepository = userRepository;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(string Username, string Email, string Password, string IsAuthor)
        {
            if (ModelState.IsValid)
            {
                if (IsAuthor == "0")
                {
                    Author author = new Author();
                    author.Username = Username;
                    author.Email = Email;
                    author.Password = Password;
                    _authorRepository.Create(author);
                    return RedirectToAction("Login", "Auth");
                }
                else if (IsAuthor == "1")
                {
                    User user = new User();
                    user.Username = Username;
                    user.Email = Email;
                    user.Password = Password;
                    _userRepository.Create(user);
                    return RedirectToAction("Login", "Auth");
                }
            }
            return RedirectToAction("Signup", "Auth");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Author author)
        {
            var IsAuthor = _authorRepository.Authenticate(author);
            if (IsAuthor)
            {
                return RedirectToAction("Home", "Author");
            }
            var Isuser = _userRepository.Authenticate(author);
            if (Isuser)
            {
                return RedirectToAction("Home", "User");
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
