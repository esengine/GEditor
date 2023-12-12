using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEditor.Helpers
{
    internal class EventBus
    {
        // 用于存储事件订阅者的字典
        private Dictionary<Type, Delegate> _subscribers = new Dictionary<Type, Delegate>();

        // 订阅事件
        public void Subscribe<T>(Action<T> handler)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                _subscribers[typeof(T)] = Delegate.Combine(_subscribers[typeof(T)], handler);
            }
            else
            {
                _subscribers[typeof(T)] = handler;
            }
        }

        // 取消订阅事件
        public void Unsubscribe<T>(Action<T> handler)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                var currentHandler = _subscribers[typeof(T)];
                _subscribers[typeof(T)] = Delegate.Remove(currentHandler, handler);
            }
        }

        // 发布事件
        public void Publish<T>(T eventArgs)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                (_subscribers[typeof(T)] as Action<T>)?.Invoke(eventArgs);
            }
        }
    }
}
