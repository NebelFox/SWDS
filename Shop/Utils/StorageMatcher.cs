using System.Collections.Generic;
using Shop.Models;

namespace Shop.Utils
{
    public static class StorageMatcher
    {
        public static IEnumerable<Product> IntersectProducts(Storage lhs, Storage rhs)
        {
            var set = new HashSet<Product>(lhs);
            set.IntersectWith(rhs);
            return set;
        }

        public static IEnumerable<Product> Except(Storage lhs, Storage rhs)
        {
            var set = new HashSet<Product>(lhs);
            set.ExceptWith(rhs);
            return set;
        }
        
        public static IEnumerable<Product> SymmetricExcept(Storage lhs, Storage rhs)
        {
            var set = new HashSet<Product>(lhs);
            set.SymmetricExceptWith(rhs);
            return set;
        }
    }
}
