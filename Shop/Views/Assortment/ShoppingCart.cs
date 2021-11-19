using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Common;
using Shop.Models.Products;

namespace Shop.Views.Assortment
{
    public class ShoppingCart: IReadOnlyList<CartItem>, INotifyMutation
    {
        private event Action MutationEvent;
        private IList<CartItem> _content;

        public ShoppingCart()
        {
            _content = new List<CartItem>();
        }

        public void Add(ProductID pid, int count)
        {
            MutationEvent?.Invoke();
            _content.Add(new CartItem(pid, count));
        }

        public void Remove(int index)
        {
            MutationEvent?.Invoke();
            _content.RemoveAt(index);
        }

        public void Clear()
        {
            MutationEvent?.Invoke();
            _content.Clear();
        }

        public IEnumerator<CartItem> GetEnumerator() => _content.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _content.Count;

        public CartItem this[int index] => _content[index];
        
        public void Subscribe(Action onMutation)
        {
            MutationEvent += onMutation;
        }

        public void Unsubscribe(Action onMutation)
        {
            MutationEvent -= onMutation;
        }
    }
}
