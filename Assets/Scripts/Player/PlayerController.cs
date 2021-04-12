using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;


namespace Assets.Scripts.Player
{
    public class PlayerController : ControllerBase
    {
        private readonly List<ServiceBase> services;

        public PlayerController(GameObject gameObject, IServiceProvider serviceProvider): base(gameObject, serviceProvider)
        {
            services = new List<ServiceBase>();
        }

        [UsedImplicitly] 
        public override void Start()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            directionHelper.Direction = Direction.Right;

            AddMovementService();
            AddDirectionService();

            foreach (var service in services) 
                service.Start();
        }

        private void AddMovementService()
        {
            var movementConfig = ServiceProvider.GetService<MovementConfig>();
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();

            var player = GameObject;

            var service = new MovementService(movementConfig, player.transform, directionHelper);
            services.Add(service);
        }

        private void AddDirectionService()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            var player = GameObject;

            var service = new DirectionService(player.transform, directionHelper);
            services.Add(service);
        }

        [UsedImplicitly]
        public override void Update()
        {
            foreach(var service in services)
                service.Update();
        }

        [UsedImplicitly]
        public override void FixedUpdate()
        {
            foreach (var service in services)
                service.FixedUpdate();
        }
    }
}
