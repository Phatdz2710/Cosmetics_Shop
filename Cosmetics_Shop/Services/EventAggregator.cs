using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<Delegate>> _subscriptions = new Dictionary<Type, List<Delegate>>();
        public void Publish<T>(T message)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                foreach (var action in actions.Cast<Action<T>>())
                {
                    action(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action)
        {
            if (!_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                actions = new List<Delegate>();
                _subscriptions[typeof(T)] = actions;
            }
            actions.Add(action);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                actions.Remove(action);
            }
        }
    }
}
