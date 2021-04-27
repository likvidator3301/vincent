namespace Assets.Scripts.Common.Events
{
    public abstract class AbstractSingleObjectRepository<T> where T: class
    {
        public T Value { get; private set; }

        public bool HasValue { get; private set; }

        public void SetValue(T @event)
        {
            Value = @event;
            HasValue = true;
        }

        public void RemoveValue()
        {
            HasValue = false;
            Value = null;
        }
    }
}