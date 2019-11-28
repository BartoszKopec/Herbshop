using HerbShop.Models;

namespace HerbShop.Services
{
    public interface ICookies
    {
        int UserId
        {
            get; set;
        }

        Cart Cart
        {
            get; set;
        }

        int InCartQuantity
        {
            get;
        }

        string ViewOnPopup
        {
            set; get;
        }

        bool IsGDPR
        {
            set; get;
        }

        bool IsSet(CookiesName name);

        void Remove(CookiesName name);
    }
}
