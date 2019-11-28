using HerbShop.Models;
using HerbShop.Services;
using HerbShop.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HerbShop.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly HerbsDbContext _context;
        private readonly ICookies _cookies;

        public AccountController(HerbsDbContext context, ICookies cookies)
        {
            _context = context;
            _cookies = cookies;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (_cookies.IsSet(CookiesName.UserId))
            {
                var user = _context.Users.Find(_cookies.UserId);
                ViewData["title"] = "Twoje konto";
                ViewData["login"] = user.Login;
                return View();
            }
            else
                return Redirect(Routing.AccountLogin);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult Create([FromForm]User user)
        {
            if (_context.Users.Any(u => u.Login == user.Login && u.Password == user.Password))
            {
                ViewData["error"] = _context.Errors.Find("users_register_exists");
                return RedirectPermanent("create");
            }
            else
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                var appUser = _context.Users.First(u => u.Login == user.Login && u.Password == user.Password);
                HttpContext.Session.SetString("userId", appUser.Id.ToString());
                if (_cookies.IsSet(CookiesName.ViewOnPopup))
                {
                    return RedirectPermanent(_cookies.ViewOnPopup);
                }
                else
                    return RedirectPermanent(Routing.AccountIndex);
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            ViewData["title"] = "Logowanie";
            return View();
        }
        [HttpPost("login")]
        public IActionResult Login([FromForm]User user)
        {
            if (_context.Users.Any(u => u.Login == user.Login && u.Password == user.Password))
            {
                var dbUser = _context.Users.First(u => u.Login == user.Login && u.Password == user.Password);
                _cookies.UserId = dbUser.Id;

                if (_cookies.IsSet(CookiesName.ViewOnPopup))
                {
                    return RedirectPermanent(_cookies.ViewOnPopup);
                }
                else
                    return RedirectPermanent(Routing.AccountIndex);
            }
            else
            {
                var error = _context.Errors.Find("users_login_notexists");
                ViewData["error"] = error;
                return View();
            }

        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            _cookies.Remove(CookiesName.UserId);
            return RedirectPermanent(Routing.AccountIndex);
        }
    }
}