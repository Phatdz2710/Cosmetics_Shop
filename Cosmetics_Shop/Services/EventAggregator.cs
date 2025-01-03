
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
    /// Implements the event aggregator pattern to manage event publication and subscription.  
    /// </summary>  
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<Delegate>> _subscriptions = new Dictionary<Type, List<Delegate>>();

        /// <summary>  
        /// Publishes an event to all subscribers of the event type.  
        /// </summary>  
        /// <typeparam name="T">The type of event to publish.</typeparam>  
        /// <param name="message">The event message to publish.</param>  
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
        /// Subscribes to an event of a specific type.  
        /// </summary>  
        /// <typeparam name="T">The type of event to subscribe to.</typeparam>  
        /// <param name="action">The action to execute when the event is published.</param>  
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
        /// Unsubscribes from an event of a specific type.  
        /// </summary>  
        /// <typeparam name="T">The type of event to unsubscribe from.</typeparam>  
        /// <param name="action">The action that was previously subscribed to the event.</param>  
        public void Unsubscribe<T>(Action<T> action)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var actions))
            {
                actions.Remove(action);
            }
        }
    }
}