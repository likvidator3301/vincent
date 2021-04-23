using System;
using System.Transactions;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.PickUp.Configs;
using Assets.Scripts.Player.PickUp.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.PickUp.Services
{
    public class PickupService: ServiceBase
    {
        private readonly PickupEventRepository pickupEventRepository;
        private readonly MovementEventRepository movementEventRepository;
        private readonly AddToInventoryEventRepository addToInventoryEventRepository;
        private readonly Transform player;
        private readonly PickupConfig config;

        public PickupService(PickupEventRepository pickupEventRepository, MovementEventRepository movementEventRepository, AddToInventoryEventRepository addToInventoryEventRepository, Transform player, PickupConfig config)
        {
            this.pickupEventRepository = pickupEventRepository ?? throw new ArgumentNullException(nameof(pickupEventRepository));
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.addToInventoryEventRepository = addToInventoryEventRepository ?? throw new ArgumentNullException(nameof(addToInventoryEventRepository));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.player = player;
        }

        public override void Update()
        {
            if (!pickupEventRepository.HasEvent)
                return;

            var pickupEvent = pickupEventRepository.Event;

            var gameObject = pickupEvent.GameObject;

            if (PositionHelper.GetDistance(player, gameObject.transform) <= config.CriticalDistance)
            {
                pickupEventRepository.RemoveEvent();
                movementEventRepository.RemoveEvent();

                addToInventoryEventRepository.SetEvent(new AddToInventoryEvent(pickupEvent.Id));
            }
        }
    }
}
