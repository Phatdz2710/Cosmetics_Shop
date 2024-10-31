using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmetics_Shop.Services.Interfaces
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> action);
        void Unsubscribe<T>(Action<T> action);
        void Publish<T>(T message);
    }
}
