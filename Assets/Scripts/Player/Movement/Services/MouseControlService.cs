using System;
using System.Diagnostics;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Services
{
    public sealed class MouseControlService: ServiceBase
    {
        private readonly MovementHelper movementHelper;
        private readonly DirectionHelper directionHelper;
        private readonly Transform player;

        public MouseControlService(Transform player, MovementHelper movementHelper, DirectionHelper directionHelper)
        {
            this.movementHelper = movementHelper ?? throw new ArgumentNullException(nameof(movementHelper));
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper)); 
            this.player = player;
        }

        public override void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Ground))
                    return;

                var destination = MouseHelper.GetPositionUnderMouse();

                var movementEvent = new MovementEvent(destination);
                movementHelper.SetEvent(movementEvent);

                SetDirection();
            }
        }

        private void SetDirection()
        {
            var playerX = player.position.x;
            var destinationX = movementHelper.MovementEvent.Destination.x;

            directionHelper.Direction = destinationX > playerX ? Direction.Right : Direction.Left;
        }
    }
}
