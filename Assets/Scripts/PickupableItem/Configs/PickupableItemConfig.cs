using System;
using Assets.Scripts.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PickupableItem.Configs
{
    public class PickupableItemConfig
    {
        public string Id { get; }
        public Sprite itemImage { get; }
        public string Name { get; }

        public PickupableItemConfig(string id, string name, Sprite image)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            itemImage = image ?? throw new ArgumentNullException(nameof(image));
        }

        // ReSharper disable once ArrangeObjectCreationWhenTypeEvident | unity не умеет в магию с new()
        public static PickupableItemConfig FromMarker(PickupableItemMarker marker) => new PickupableItemConfig(marker.Id, marker.Name, marker.InventoryImage);
    }
}
