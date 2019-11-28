using HerbShop.Models;
using HerbShop.Services;
using HerbShop.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
            ViewData["title"] = "Koszyk";
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
            ViewData["title"] = "Zamówienie";
            return View();
        }
        [HttpPost("order")]
        public IActionResult PostOrder([FromForm]OrderPost orderPost)
        {
            var order = new Order
            {
                UserId = _cookies.UserId,
                Total = _cookies.Cart.Amount,
                Description = orderPost.Description,
                FirstName = orderPost.FirstName,
                LastName = orderPost.LastName,
                Address = orderPost.Address,
                CreatedOn = DateTime.Now,
                Products = JsonConvert.SerializeObject(_cookies.Cart.Items)
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            _cookies.Remove(CookiesName.Cart);
            return RedirectPermanent(Routing.AccountIndex);
        }
    }
}
