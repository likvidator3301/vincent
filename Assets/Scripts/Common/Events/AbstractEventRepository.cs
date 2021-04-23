using System.Collections.Generic;

namespace Assets.Scripts.Common.Events
{
    public abstract class AbstractEventRepository<T> where T : Event
    {
        private readonly Dictionary<string, T> events;

        public AbstractEventRepository()
        {
            events = new Dictionary<string, T>();
        }

        public void Add(T @event)
        {
            var id = @event.GetObjectId();
            events[id] = @event;
        }

        public bool HasEventForObjectWithId(string id)
        {
            return events.ContainsKey(id);
        }

        public T Get(string id)
        {
            return events[id];
        }

        public void Remove(string id)
        {
            events.Remove(id);
        }
    }
}