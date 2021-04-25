using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using Assets.Scripts.Player.PickUp.Configs;
using Assets.Scripts.Player.PickUp.Repositories;
using Assets.Scripts.Player.PickUp.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;


namespace Assets.Scripts.Player
{
    public class PlayerController : ControllerBase
    {
        public PlayerController(GameObject gameObject, IServiceProvider serviceProvider): base(gameObject, serviceProvider)
        { }

        [UsedImplicitly] 
        public override void Start()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            directionHelper.Direction = Direction.Right;
            AddMovementService();
            AddDirectionService();
            AddMouseControlService();
            AddPickUpService();

            foreach (var service in Services) 
                service.Start();
        }

        private void AddPickUpService()
        {
            var pickupEventRepository = ServiceProvider.GetService<PickupEventRepository>();
            var movementEventRepository = ServiceProvider.GetService<MovementEventRepository>();
            var addToInventoryEventRepository = ServiceProvider.GetService<AddToInventoryEventRepository>();

            var player = GameObject;

            var config = ServiceProvider.GetService<PickupConfig>();

            var service = new PickupService(pickupEventRepository, movementEventRepository,
                addToInventoryEventRepository, player.transform, config);

            Services.Add(service);
        }

        private void AddMouseControlService()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            var movementHelper = ServiceProvider.GetService<MovementEventRepository>();

            var pickupEventRepository = ServiceProvider.GetService<PickupEventRepository>();

            var player = GameObject;

            var service = new MouseControlService(player.transform, movementHelper, directionHelper, pickupEventRepository);
            Services.Add(service);
        }

        private void AddMovementService()
        {
            var movementConfig = ServiceProvider.GetService<MovementConfig>();
            var movementHelper = ServiceProvider.GetService<MovementEventRepository>();

            var player = GameObject;

            var service = new MovementService(movementConfig, player.transform, movementHelper);
            Services.Add(service);
        }

        private void AddDirectionService()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            var player = GameObject;

            var service = new DirectionService(player.transform, directionHelper);
            Services.Add(service);
        }

        [UsedImplicitly]
        public override void Update()
        {
            foreach(var service in Services)
                service.Update();
        }

        [UsedImplicitly]
        public override void FixedUpdate()
        {
            foreach (var service in Services)
                service.FixedUpdate();
        }
    }
}
