using System.Collections.Generic;
using Shop.Common;
using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public interface IReadOnlyStorage : INotifyMutation
    {
        public ProductStack this[ProductID pid] { get; }

        public List<ProductID> ToList();
    }
}
