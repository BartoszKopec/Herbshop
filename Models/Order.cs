using SQLite;
using System;

namespace HerbShop.Models
{
    public class Order
    {
        [AutoIncrement]
        public int Id { get; set; }
        public long UserId { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public string Products { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}
