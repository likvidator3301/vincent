using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Services
{
    public class MovementService : ServiceBase
    {
        private readonly MovementConfig config;
        private readonly Transform player;
        private readonly MovementHelper movementHelper;

        public MovementService(MovementConfig config, Transform player, MovementHelper movementHelper)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.movementHelper = movementHelper ?? throw new ArgumentNullException(nameof(movementHelper));
            this.player = player; //lividator: не проверяю на нулл, потому что юнити не умеет проверять игровые объекты и их свойства на нулл (они всегда не нулл)
        }


        public override void Update()
        {
            if (!movementHelper.IsInMovement || movementHelper.MovementEvent == null)
                return;

            var destination = movementHelper.MovementEvent.Destination;

            if (Math.Abs(destination.x - player.position.x) <= config.CriticalDistance)
                movementHelper.Stop();

            player.position += destination.x > player.position.x
                ? new Vector3(config.Speed, 0).WithDeltaTime()
                : new Vector3(-config.Speed, 0).WithDeltaTime();
        }
    }
}
