using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.PickUp.Repositories;
using Assets.Scripts.Player.PickupableItemInteraction.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.PickUp.Services
{
    public class PickupService: ServiceBase
    {
        private readonly PickupEventRepository pickupEventRepository;
        private readonly InteractWithPickupableItemEventRepository pickupableItemEventRepository;
        private readonly MovementEventRepository movementEventRepository;
        private readonly AddToInventoryEventRepository addToInventoryEventRepository;
        private readonly Transform player;
        private readonly PlayerConfig config;

        public PickupService(
            PickupEventRepository pickupEventRepository, 
            MovementEventRepository movementEventRepository, 
            AddToInventoryEventRepository addToInventoryEventRepository,
            InteractWithPickupableItemEventRepository pickupableItemEventRepository,
            Transform player, 
            PlayerConfig config)
        {
            this.pickupEventRepository = pickupEventRepository ?? throw new ArgumentNullException(nameof(pickupEventRepository));
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.addToInventoryEventRepository = addToInventoryEventRepository ?? throw new ArgumentNullException(nameof(addToInventoryEventRepository));
            this.pickupableItemEventRepository = pickupableItemEventRepository;
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.player = player;
        }

        public override void Update()
        {
            if (!pickupEventRepository.HasValue)
                return;

            var pickupEvent = pickupEventRepository.Value;

            var gameObject = pickupEvent.Marker.gameObject;

            if (PositionHelper.GetDistance(player, gameObject.transform) <= config.InteractCriticalDistance)
            {
                pickupEventRepository.RemoveValue();
                movementEventRepository.RemoveValue();

                addToInventoryEventRepository.SetValue(new AddToInventoryEvent(pickupEvent.Marker.Id));
                pickupableItemEventRepository.SetValue(new InteractWithPickupableItemEvent(pickupEvent.Marker));
            }
        }
    }
}
