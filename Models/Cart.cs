using System.Collections.Generic;

namespace HerbShop.Models
{
    public class Cart
    {
        public List<Item> Items { get; set; } = new List<Item>();

        //public Dictionary<Item, int> Items { get; set; } = new Dictionary<Item, int>();
        public decimal Amount => GetAmount();

        private decimal GetAmount()
        {
            decimal amount=0;
            foreach (var item in Items)
            {
                amount += item.Price*item.QuantityInCart;
            }
            return amount;
        }
    }
}
