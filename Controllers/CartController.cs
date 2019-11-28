using HerbShop.Models;
using HerbShop.Services;
using HerbShop.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HerbShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private ICookies _cookies;
        private HerbsDbContext _context;

        public CartController(ICookies cookies, HerbsDbContext context)
        {
            _cookies = cookies;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("add")]
        public IActionResult AddToCart([FromQuery]int id)
        {
            if (_cookies.IsSet(CookiesName.UserId))
            {
                var cart = new Cart();
                if (_cookies.IsSet(CookiesName.Cart))
                    cart = _cookies.Cart;

                var item = _context.Items.Where(item=> item.Id==id ).First();

                var itemInCart = cart.Items.FirstOrDefault(i => i.Id == id);
                if (itemInCart != null)
                    itemInCart.QuantityInCart++;
                else
                {
                    item.QuantityInCart = 1;
                    cart.Items.Add(item);
                }

                _cookies.Cart = cart;
                return RedirectPermanent(Routing.EShopIndex);
            }
            else
            {
                _cookies.ViewOnPopup = Routing.WithParameters(Routing.CartAdd, ("id", id.ToString()));
                return RedirectPermanent(Routing.AccountLogin);
            }
        }

        [HttpGet("order")]
        public IActionResult Order()
        {
            var cart = _cookies.Cart;
            ViewData["cart"] = cart;
            return View();
        }
    }
}
