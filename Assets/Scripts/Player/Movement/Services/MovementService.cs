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
        private readonly DirectionHelper directionHelper;
        private readonly Transform player;

        public MovementService(MovementConfig config, Transform player, DirectionHelper directionHelper)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper));
            this.player = player; //lividator: не проверяю на нулл, потому что юнити не умеет проверять игровые объекты и их свойства на нулл (они всегда не нулл)
        }

        
        public override void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                player.position += Vector3.left.WithDeltaTime() * config.Speed;
                directionHelper.Direction = Direction.Left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                player.position += Vector3.right.WithDeltaTime() * config.Speed;
                directionHelper.Direction = Direction.Right;
            }
        }
    }
}
