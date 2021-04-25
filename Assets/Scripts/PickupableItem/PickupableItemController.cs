using System;
using Assets.Scripts.Common;
using Assets.Scripts.Inventory;
using Assets.Scripts.PickupableItem.Configs;
using Assets.Scripts.PickupableItem.Services;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.PickupableItem
{
    public class PickupableItemController: ControllerBase
    {
        private readonly PickupableItemConfig config;

        public PickupableItemController(GameObject gameObject, IServiceProvider serviceProvider, PickupableItemConfig config) : base(gameObject, serviceProvider)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override void Start()
        {
            AddPickupService();

            foreach (var service in Services) 
                service.Start();
        }

        public override void Update()
        {
            foreach (var service in Services)
                service.Update();
        }

        private void AddPickupService()
        {
            var repository = ServiceProvider.GetService<AddToInventoryEventRepository>();
            var inventory = ServiceProvider.GetService<PlayerInventory>();
            var service = new AddToInventoryService(GameObject, config, repository, inventory);
            Services.Add(service);
        }
    }
}
