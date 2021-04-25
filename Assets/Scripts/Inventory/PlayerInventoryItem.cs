using System;

namespace Assets.Scripts.Inventory
{
    public class PlayerInventoryItem
    {
        public string Id { get; }

        public string Name { get; }

        public PlayerInventoryItem(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override bool Equals(object obj)
        {
            if (obj is PlayerInventoryItem other)
                return Id == other.Id && Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + 17 * Name.GetHashCode();
        }
    }
}
