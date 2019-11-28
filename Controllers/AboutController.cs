using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HerbShop.Services;

namespace HerbShop.Controllers
{
    [Route("about")]
    public class AboutController : Controller
    {
        private HerbsDbContext _context;
        
        public AboutController(HerbsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var herbs = _context.Herbs.ToList();
            ViewData["herbs"] = herbs;
            return View();
        }
    }
}
