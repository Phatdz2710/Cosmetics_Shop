using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services
{
    /// <summary>
    /// Event aggregator service for publishing and subscribing to events without tight coupling.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// Dictionary of event subscriptions.
        /// </summary>
        private Dictionary<Type, List<Delegate>> _subscriptions = new Dictionary<Type, List<Delegate>>();

        /// <summary>
        /// Publishes an event to all subscribers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
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

        /// <summary>
        /// Subscribes to a specific type of event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Subscribe<T>(Action<T> action)
        {
            if (!_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                actions = new List<Delegate>();
                _subscriptions[typeof(T)] = actions;
            }
            actions.Add(action);
        }

        /// <summary>
        /// Unsubscribes from a specific type of event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Unsubscribe<T>(Action<T> action)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                actions.Remove(action);
            }
        }
    }
}
