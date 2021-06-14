using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class PlayerInventoryItem
    {
        public string Id { get; }
        public Sprite InventoryImage { get;  }
        public string Name { get; }

        public PlayerInventoryItem(string id, string name, Sprite image)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            InventoryImage = image ?? throw new ArgumentNullException(nameof(image));
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
