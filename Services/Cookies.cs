using HerbShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace HerbShop.Services
{
    public class Cookies : ICookies
    {
        private ISession _session;
        private readonly string _userId = "userId",
            _cart = "cart",
            _inCartQuantity = "inCartQuantity",
            _viewOnPopup = "viewOnPopup",
            _isGDPR = "isGdpr";

        public Cookies(IHttpContextAccessor httpContext)
        {
            _session = httpContext.HttpContext.Session;
        }

        public int UserId
        {
            get => (int)_session.GetInt32(_userId);
            set { if (value != -1) _session.SetInt32(_userId, value); else _session.Remove(_userId); }
        }
        public Cart Cart
        {
            get //=> JsonConvert.DeserializeObject<Cart>(_session.GetString(_cart));
            {
                var cart = _session.GetString(_cart);
                return JsonConvert.DeserializeObject<Cart>(cart);
            }
            set
            {
                if (value != null)
                {
                    _session.SetString(_cart, JsonConvert.SerializeObject(value));
                    int inCart = value.Items.Sum((item)=> item.QuantityInCart);
                    _session.SetInt32(_inCartQuantity, inCart);
                }
                else
                {
                    _session.Remove(_cart);
                    _session.SetInt32(_inCartQuantity, 0);
                }
            }
        }
        public int InCartQuantity
        {
            get => _session.GetInt32(_inCartQuantity) == null ? 0 : (int)_session.GetInt32(_inCartQuantity);
        }
        public string ViewOnPopup
        {
            get => _session.GetString(_viewOnPopup);
            set { if (value != null) _session.SetString(_viewOnPopup, value); else _session.Remove(_viewOnPopup); }
        }
        public bool IsGDPR
        {
            get => Convert.ToBoolean(_session.GetInt32(_isGDPR));
            set => _session.SetInt32(_isGDPR, Convert.ToInt32(value));
        }

        public bool IsSet(CookiesName name)
        {
            return name switch
            {
                CookiesName.UserId => _session.Keys.Contains(_userId),
                CookiesName.Cart => _session.Keys.Contains(_cart),
                CookiesName.ViewOnPopup => _session.Keys.Contains(_viewOnPopup),
                CookiesName.GDPR => _session.Keys.Contains(_isGDPR),
                _ => false,
            };
        }
        public void Remove(CookiesName name)
        {
            switch (name)
            {
                case CookiesName.UserId:
                    UserId = -1; break;
                case CookiesName.Cart:
                    Cart = null; break;
                case CookiesName.ViewOnPopup:
                    ViewOnPopup = null; break;
                case CookiesName.GDPR:
                    IsGDPR = false; break;
                default:
                    break;
            }
        }
    }
}
