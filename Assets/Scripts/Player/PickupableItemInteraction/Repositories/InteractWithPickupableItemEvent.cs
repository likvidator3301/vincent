using System;
using Assets.Scripts.Markers;

namespace Assets.Scripts.Player.PickupableItemInteraction.Repositories
{
    public class InteractWithPickupableItemEvent
    {
        public PickupableItemMarker Marker { get; }

        public InteractWithPickupableItemEvent(PickupableItemMarker marker)
        {
            Marker = marker ?? throw new ArgumentNullException(nameof(marker));
        }
    }
}
