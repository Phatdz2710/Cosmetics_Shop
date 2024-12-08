using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.Interfaces
{
    /// <summary>
    /// Defines a service for event aggregation, enabling components to publish and subscribe to events without tight coupling.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Subscribes to a specific type of event.
        /// </summary>
        /// <typeparam name="T">The type of event to subscribe to.</typeparam>
        /// <param name="action">
        /// The action to execute when the event of type <typeparamref name="T"/> is published.
        /// </param>
        void Subscribe<T>(Action<T> action);

        /// <summary>
        /// Unsubscribes from a specific type of event.
        /// </summary>
        /// <typeparam name="T">The type of event to unsubscribe from.</typeparam>
        /// <param name="action">
        /// The action that was previously subscribed to the event of type <typeparamref name="T"/>.
        /// </param>
        void Unsubscribe<T>(Action<T> action);

        /// <summary>
        /// Publishes an event to all subscribers.
        /// </summary>
        /// <typeparam name="T">The type of event to publish.</typeparam>
        /// <param name="message">
        /// The message of type <typeparamref name="T"/> to send to all subscribers.
        /// </param>
        void Publish<T>(T message);
    }

}
