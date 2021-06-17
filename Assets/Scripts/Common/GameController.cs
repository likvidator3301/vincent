using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DialogueContainer;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.PickupableItem;
using Assets.Scripts.PickupableItem.Configs;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.NpcInteraction.Repositories;
using Assets.Scripts.Player.PickUp.Repositories;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Repositories;
using JetBrains.Annotations;
using UnityEngine;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine.UI;

namespace Assets.Scripts.Common
{
    public class GameController: MonoBehaviour
    {
        private IServiceProvider serviceProvider;
        private List<ControllerBase> controllers;

        [UsedImplicitly]
        public void Start()
        {
            controllers = new List<ControllerBase>();

            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            
            CreateControllers();
            StartControllers();
        }

        private void StartControllers()
        {
            foreach (var controller in controllers) 
                controller.Start();
        }

        //likvidator: здесь регистрируем всё, что лежит в слое репозиториев
        private void ConfigureServices(IServiceCollection services)
        {
            //todo(likvidator): читать из конфигурации
            services.AddSingleton(_ => new PlayerConfig(.69f, .1f, 1));

            services.AddSingleton<DirectionHelper>();
            services.AddSingleton<MovementEventRepository>();
            services.AddSingleton<PickupEventRepository>();

            services.AddSingleton<AddToInventoryEventRepository>();
            services.AddSingleton<PlayerInventory>();

            services.AddSingleton<InteractWithNpcEventRepository>();
            services.AddSingleton<StartDialogueEventRepository>();
            services.AddSingleton<NewTextEventRepository>();
            services.AddSingleton<DialogueRepository>();
            services.AddSingleton<IconForDialogueRepository>();
            services.AddSingleton<FinishDialogueEventRepository>();

            services.AddSingleton<InteractWithSceneTransferEventRepository>();
            services.AddSingleton<TeleportPlayerEventRepository>();
            services.AddSingleton<TransferToSceneEventRepository>();

            services.AddSingleton<DialogueParser>();
            services.AddSingleton<DialogueActionFactory>();
            services.AddSingleton<DialogueConditionFactory>();
        }

        private void CreateControllers()
        {
            CreatePlayerController();
            CreateInteractiveObjectControllers();
            CreateInventoryController();
            CreateNpcControllers();
            CreateDialogueContainerController();
            CreateSceneTransferControllers();
        }

        private void CreateNpcControllers()
        {
            var npcs = FindObjectsOfType(typeof(NpcMarker)) as NpcMarker[];

            if (npcs == null)
                return;

            foreach (var npc in npcs)
            {
                var controller = new NpcController(npc.gameObject, serviceProvider);
                controllers.Add(controller);
            }
        }

        private void CreateDialogueContainerController()
        {
            var dialogueContainerMarker = FindObjectOfType<DialogueContainerMarker>() as DialogueContainerMarker;

            var controller = new DialogueContainerController(dialogueContainerMarker.gameObject, serviceProvider, dialogueContainerMarker);

            controllers.Add(controller);
        }

        private void CreateInventoryController()
        {
            var inventories = FindObjectsOfType(typeof(InventoryMarker)) as InventoryMarker[];

            if (inventories == null || inventories.Length == 0)
                throw new GameInitializationException("Inventory not found");

            if (inventories.Length > 1)
                throw new GameInitializationException($"Found '{inventories.Length}' inventories. Expected one inventory");

            var inventory = inventories.First();
            var controller = new InventoryController(inventory.gameObject, serviceProvider);

            controllers.Add(controller);
        }

        private void CreateInteractiveObjectControllers()
        {
            var interactiveObjects = FindObjectsOfType(typeof(PickupableItemMarker)) as PickupableItemMarker[];

            if (interactiveObjects == null)
                return;

            foreach (var interactiveObject in interactiveObjects)
            {
                var config = PickupableItemConfig.FromMarker(interactiveObject);
                var controller = new PickupableItemController(interactiveObject.gameObject, serviceProvider, config);
                controllers.Add(controller);
            }
        }

        private void CreatePlayerController()
        {
            var players = FindObjectsOfType(typeof(PlayerMarker)) as PlayerMarker[];

            if (players == null || players.Length == 0)
                throw new GameInitializationException("Player not found");

            if (players.Length > 1)
                throw new GameInitializationException($"Found '{players.Length}' players. Expected one player");

            var player = players.First().gameObject;
            var playerController = new PlayerController(player, serviceProvider);
            controllers.Add(playerController);
        }

        private void CreateSceneTransferControllers()
        {
            var sceneTransfers = FindObjectsOfType<SceneTransferMarker>();

            foreach (var sceneTransferMarker in sceneTransfers)
            {
                var sceneTransferController = new SceneTransferController(sceneTransferMarker,
                    sceneTransferMarker.gameObject, serviceProvider);
                controllers.Add(sceneTransferController);
            }
        }

        [UsedImplicitly]
        private void Update()
        {
            foreach (var controller in controllers)
                controller.Update();
        }

        [UsedImplicitly]
        private void FixedUpdate()
        {
            foreach (var controller in controllers)
                controller.Update();
        }
    }
}
