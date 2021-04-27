using System;
using Assets.Scripts.Markers;

namespace Assets.Scripts.PickupableItem.Configs
{
    public class PickupableItemConfig
    {
        public string Id { get; }

        public string Name { get; }

        public PickupableItemConfig(string id, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        // ReSharper disable once ArrangeObjectCreationWhenTypeEvident | unity не умеет в магию с new()
        public static PickupableItemConfig FromMarker(PickupableItemMarker marker) => new PickupableItemConfig(marker.Id, marker.Name);
    }
}
