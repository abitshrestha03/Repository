namespace Day8First.Models
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _sessionContxt;
        public SessionService(IHttpContextAccessor sessionContxt)
        {
            _sessionContxt = sessionContxt;
        }
        public void SetAuthorSessionValue(Author author)
        {
            _sessionContxt.HttpContext.Session.SetInt32("AuthorId", (int)author.Id);
            _sessionContxt.HttpContext.Session.SetString("AuthorName", author.Username);
            _sessionContxt.HttpContext.Session.SetString("IsLoggedIn", "true");
            _sessionContxt.HttpContext.Session.SetString("IsAuthor", "true");
        }
        public void SetUserSessionValue(User user)
        {
            _sessionContxt.HttpContext.Session.SetInt32("UserId", (int)user.Id);
            _sessionContxt.HttpContext.Session.SetString("UserName", user.Username);
            _sessionContxt.HttpContext.Session.SetString("IsLoggedIn", "true");
            _sessionContxt.HttpContext.Session.SetString("IsAuthor", "false");
        }
    }
}
