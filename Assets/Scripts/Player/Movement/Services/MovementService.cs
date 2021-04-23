using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Services
{
    public class MovementService : ServiceBase
    {
        private readonly MovementConfig config;
        private readonly Transform player;
        private readonly MovementEventRepository movementEventRepository;

        public MovementService(MovementConfig config, Transform player, MovementEventRepository movementEventRepository)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.player = player;
        }


        public override void Update()
        {
            if (!movementEventRepository.HasEvent)
                return;

            var destination = movementEventRepository.Event.Destination;

            if (PositionHelper.GetDistance(destination, player.position) <= config.CriticalDistance)
                movementEventRepository.RemoveEvent();

            player.position += destination.x > player.position.x
                ? new Vector3(config.Speed, 0).WithDeltaTime()
                : new Vector3(-config.Speed, 0).WithDeltaTime();
        }
    }
}
