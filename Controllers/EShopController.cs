using HerbShop.Models;
using HerbShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HerbShop.Controllers
{
    [Route("")]
    public class EShopController : Controller
    {
        private readonly HerbsDbContext _context;

        public EShopController(HerbsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["title"] = "Sklep";
            ViewData["items"] = _context.Items.ToList();
            return View();
        }
    }
}