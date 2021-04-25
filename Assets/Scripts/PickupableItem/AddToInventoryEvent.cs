using System;
using Assets.Scripts.Common.Events;

namespace Assets.Scripts.PickupableItem
{
    public class AddToInventoryEvent: Event
    {
        public string ItemId { get; }

        public AddToInventoryEvent(string itemId)
        {
            ItemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
        }

        public override string GetObjectId() => ItemId;
    }
}
