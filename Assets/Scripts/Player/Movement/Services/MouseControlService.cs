using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Markers;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.NpcInteraction.Repositories;
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
        private readonly InteractWithNpcEventRepository interactWithNpcEventRepository;
        private readonly PlayerConfig config;

        public MouseControlService(
            Transform player,
            MovementEventRepository movementEventRepository,
            DirectionHelper directionHelper,
            PickupEventRepository pickupEventRepository,
            InteractWithNpcEventRepository interactWithNpcEventRepository,
            PlayerConfig config)
        {
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper)); 
            this.pickupEventRepository = pickupEventRepository ?? throw new ArgumentNullException(nameof(pickupEventRepository));
            this.player = player;
            this.interactWithNpcEventRepository = interactWithNpcEventRepository ?? throw new ArgumentNullException(nameof(interactWithNpcEventRepository));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                movementEventRepository.RemoveValue();
                pickupEventRepository.RemoveValue();
                interactWithNpcEventRepository.RemoveValue();

                if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Ground))
                    ProcessMovement();

                if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.PickupableItem))
                    ProcessPickup();

                if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Npc))
                    ProcessNpc();
            }
        }

        private void ProcessNpc()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<NpcMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            interactWithNpcEventRepository.SetValue(new InteractWithNpcEvent(marker));

            if (PositionHelper.GetDistance(player.position, destination) > config.InteractCriticalDistance)
                ProcessMovement();
        }

        private void ProcessPickup()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<PickupableItemMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            pickupEventRepository.SetValue(new PickupEvent(marker));

            if (PositionHelper.GetDistance(player.position, destination) > config.InteractCriticalDistance) 
                ProcessMovement();
        }

        private void ProcessMovement()
        {
            var destination = MouseHelper.GetPositionUnderMouse();

            var movementEvent = new MovementEvent(destination);
            movementEventRepository.SetValue(movementEvent);

            SetDirection();
        }

        private void SetDirection()
        {
            var playerX = player.position.x;
            var destinationX = movementEventRepository.Value.Destination.x;

            directionHelper.Direction = destinationX > playerX ? Direction.Right : Direction.Left;
        }
    }
}
