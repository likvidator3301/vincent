using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Markers;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.PickUp.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Services
{
    public sealed class MouseControlService: ServiceBase
    {
        private readonly MovementEventRepository movementEventRepository;
        private readonly DirectionHelper directionHelper;
        private readonly Transform player;
        private readonly PickupEventRepository pickupEventRepository;

        public MouseControlService(Transform player, MovementEventRepository movementEventRepository, DirectionHelper directionHelper, PickupEventRepository addToInventoryEventRepository)
        {
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper)); 
            this.pickupEventRepository = addToInventoryEventRepository ?? throw new ArgumentNullException(nameof(addToInventoryEventRepository));
            this.player = player;
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                movementEventRepository.RemoveEvent();
                pickupEventRepository.RemoveEvent();

                if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Ground))
                    ProcessMovement();

                if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.PickupableItem))
                    ProcessPickup();
            }
        }

        private void ProcessPickup()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<PickupableItemMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            pickupEventRepository.SetEvent(new PickupEvent(marker.Id, marker.gameObject));

            if (PositionHelper.GetDistance(player.position, destination) > marker.CriticalDistance) 
                ProcessMovement();
        }

        private void ProcessMovement()
        {
            var destination = MouseHelper.GetPositionUnderMouse();

            var movementEvent = new MovementEvent(destination);
            movementEventRepository.SetEvent(movementEvent);

            SetDirection();
        }

        private void SetDirection()
        {
            var playerX = player.position.x;
            var destinationX = movementEventRepository.Event.Destination.x;

            directionHelper.Direction = destinationX > playerX ? Direction.Right : Direction.Left;
        }
    }
}
