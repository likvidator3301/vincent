namespace Assets.Scripts.Common.Events
{
    public abstract class AbstractSingleObjectRepository<T> where T: class
    {
        public T Event { get; private set; }

        public bool HasEvent { get; private set; }

        public void SetEvent(T @event)
        {
            Event = @event;
            HasEvent = true;
        }

        public void RemoveEvent()
        {
            HasEvent = false;
            Event = null;
        }
    }
}