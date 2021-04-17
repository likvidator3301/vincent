using System;
using Assets.Scripts.Common;
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
                var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var movementEvent = new MovementEvent(destination);
                movementHelper.SetEvent(movementEvent);
                var playerX = player.position.x;
                var destinationX = destination.x;

                directionHelper.Direction = destinationX > playerX ? Direction.Right : Direction.Left;
            }
        }
    }
}
