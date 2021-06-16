using System;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using Assets.Scripts.Player.NpcInteraction;
using Assets.Scripts.Player.NpcInteraction.Repositories;
using Assets.Scripts.Player.PickUp.Repositories;
using Assets.Scripts.Player.PickUp.Services;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using Assets.Scripts.Player.SceneTransfer.Service;
using Assets.Scripts.Scenes.Repositories;
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
            AddNpcInteractionService();
            AddTeleportPlayerService();
            AddInteractWithSceneTransferService();

            foreach (var service in Services) 
                service.Start();
        }

        private void AddNpcInteractionService()
        {
            var interactEventRepository = ServiceProvider.GetService<InteractWithNpcEventRepository>();
            var movementEventRepository = ServiceProvider.GetService<MovementEventRepository>();
            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();

            var player = GameObject;

            var config = ServiceProvider.GetService<PlayerConfig>();

            var service = new NpcInteractionService(config, movementEventRepository, interactEventRepository,
                startDialogueEventRepository, player.transform);

            Services.Add(service);
        }

        private void AddPickUpService()
        {
            var pickupEventRepository = ServiceProvider.GetService<PickupEventRepository>();
            var movementEventRepository = ServiceProvider.GetService<MovementEventRepository>();
            var addToInventoryEventRepository = ServiceProvider.GetService<AddToInventoryEventRepository>();

            var player = GameObject;

            var config = ServiceProvider.GetService<PlayerConfig>();

            var service = new PickupService(pickupEventRepository, movementEventRepository,
                addToInventoryEventRepository, player.transform, config);

            Services.Add(service);
        }

        private void AddMouseControlService()
        {
            var directionHelper = ServiceProvider.GetService<DirectionHelper>();
            var movementHelper = ServiceProvider.GetService<MovementEventRepository>();

            var pickupEventRepository = ServiceProvider.GetService<PickupEventRepository>();
            var interactEventRepository = ServiceProvider.GetService<InteractWithNpcEventRepository>();
            var finishDialogueRepository = ServiceProvider.GetService<FinishDialogueEventRepository>();
            var interactWithSceneTransferRepository = ServiceProvider.GetService<InteractWithSceneTransferEventRepository>();
            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();
            var newTextEventRepository = ServiceProvider.GetService<NewTextEventRepository>();

            var playerConfig = ServiceProvider.GetService<PlayerConfig>();

            var player = GameObject;

            var service = new MouseControlService(player.transform, movementHelper, directionHelper, pickupEventRepository, 
                interactEventRepository, newTextEventRepository, finishDialogueRepository, interactWithSceneTransferRepository, startDialogueEventRepository, playerConfig);
            Services.Add(service);
        }

        private void AddInteractWithSceneTransferService()
        {
            var movementHelper = ServiceProvider.GetService<MovementEventRepository>();

            var interactWithSceneTransferRepository = ServiceProvider.GetService<InteractWithSceneTransferEventRepository>();
            var playerConfig = ServiceProvider.GetService<PlayerConfig>();
            var transferToSceneEventRepository = ServiceProvider.GetService<TransferToSceneEventRepository>();

            var player = GameObject;

            var service = new InteractWithSceneTransferService(movementHelper, playerConfig,
                interactWithSceneTransferRepository, transferToSceneEventRepository, player.transform);

            Services.Add(service);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
        }

        private void AddTeleportPlayerService()
        {
            var player = GameObject;
            var teleportPlayerEventRepository = ServiceProvider.GetService<TeleportPlayerEventRepository>();

            var service = new TeleportPlayerService(player.transform, teleportPlayerEventRepository);

            Services.Add(service);
        }

        private void AddMovementService()
        {
            var movementConfig = ServiceProvider.GetService<PlayerConfig>();
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
