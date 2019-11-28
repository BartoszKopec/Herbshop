using HerbShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HerbShop.Utils
{
    public class ItemsComparer : EqualityComparer<Item>
    {
        public override bool Equals([AllowNull] Item x, [AllowNull] Item y)
        {
            return x.Id == y.Id;
        }

        public override int GetHashCode([DisallowNull] Item obj)
        {
            return obj.GetHashCode();
        }
    }
}
