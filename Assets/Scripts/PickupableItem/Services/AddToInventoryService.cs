using System;
using Assets.Scripts.Common;
using Assets.Scripts.Inventory;
using Assets.Scripts.PickupableItem.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.PickupableItem.Services
{
    public class AddToInventoryService: ServiceBase
    {
        private readonly GameObject gameObject;
        private readonly PickupableItemConfig config;
        private readonly AddToInventoryEventRepository addToInventoryEventRepository;
        private readonly PlayerInventory inventory;

        public AddToInventoryService(GameObject gameObject, PickupableItemConfig config, AddToInventoryEventRepository addToInventoryEventRepository, PlayerInventory inventory)
        {
            this.gameObject = gameObject;
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.addToInventoryEventRepository = addToInventoryEventRepository ?? throw new ArgumentNullException(nameof(addToInventoryEventRepository));
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        }

        public override void Update()
        {
            if (!addToInventoryEventRepository.HasValue || addToInventoryEventRepository.Value.ItemId != config.Id)
                return;

            inventory.Add(new PlayerInventoryItem(config.Id, config.Name));

            Object.Destroy(gameObject);

            addToInventoryEventRepository.RemoveValue();
        }
    }
}
