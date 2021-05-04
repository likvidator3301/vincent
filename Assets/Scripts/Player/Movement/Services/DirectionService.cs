using System;
using Assets.Scripts.Common;
using Assets.Scripts.Player.Movement.Helpers;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Services
{
    public class DirectionService: ServiceBase
    {
        private readonly Transform player;
        private readonly DirectionHelper directionHelper;

        private Direction lastDirection;

        public DirectionService(Transform player, DirectionHelper directionHelper)
        {
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper));
            this.player = player;
        }

        public override void Start()
        {
            lastDirection = directionHelper.Direction;
        }

        public override void Update()
        {
            if (lastDirection != directionHelper.Direction)
                player.localScale = new Vector3(-player.localScale.x, player.localScale.y, player.localScale.z);

            lastDirection = directionHelper.Direction;
        }
    }
}
