using System;
using Assets.Scripts.Markers;
using UnityEngine;

namespace Assets.Scripts.Player.PickUp.Repositories
{
    public class PickupEvent
    {
        public PickupableItemMarker Marker { get; }

        public PickupEvent(PickupableItemMarker marker)
        {
            Marker = marker ?? throw new ArgumentNullException(nameof(marker));
        }
    }
}
