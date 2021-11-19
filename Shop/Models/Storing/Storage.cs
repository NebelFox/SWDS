using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Models.Products;

namespace Shop.Models.Storing
{
    public class Storage : IStorage
    {
        private event Action MutatedEvent;
        
        private Dictionary<ProductID, ProductStack> _content;
        // private Reservation _reservation;

        public Storage()
        {
            _content = new Dictionary<ProductID, ProductStack>();
            // _reservation = new Reservation();
        }

        public ProductStack this[ProductID pid] => _content[pid];

        public ProductID RegisterProduct(Product product)
        {
            ProductID pid = ProductIDProvider.Instance.Next();
            _content[pid] = new ProductStack(product);
            MutatedEvent?.Invoke();
            return pid;
        }

        public void RemoveProduct(ProductID pid)
        {
            MutatedEvent?.Invoke();
            _content.Remove(pid);
        }

        public void Replenish(ProductID pid, int count = 0)
        {
            MutatedEvent?.Invoke();
            _content[pid].Add(count);
        }

        public void Take(ProductID pid, int count)
        {
            MutatedEvent?.Invoke();
            _content[pid].Remove(count);
        }

        public List<ProductID> ToList()
        {
            return _content.Keys.ToList();
        }

        public void Subscribe(Action action)
        {
            MutatedEvent += action;
        }

        public void Unsubscribe(Action action)
        {
            MutatedEvent -= action;
        }

        public void Reserve(ProductID pid, int count)
        {
            /*if (this[id].Count < count)
                throw new InvalidOperationException("Not enough product instances to reserve");
            _content[id].Remove(count);
            return _reservation.Reserve(id, count);*/
            MutatedEvent?.Invoke();
            _content[pid].Reserve(count);
        }

        public void Retrieve(ProductID pid, int count)
        {
            MutatedEvent?.Invoke();
            _content[pid].Retrieve(count);
            /*Reserve reserve = _reservation.Retrieve(id);
            _content[reserve.ProductID].Add(reserve.Count);*/
        }

        
    }
}
