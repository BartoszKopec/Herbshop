using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerbShop.Models
{
    public class OrderPost
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
