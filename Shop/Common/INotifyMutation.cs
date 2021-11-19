using System;

namespace Shop.Common
{
    public interface INotifyMutation
    {
        public void Subscribe(Action onMutation);

        public void Unsubscribe(Action onMutation);
    }
}
