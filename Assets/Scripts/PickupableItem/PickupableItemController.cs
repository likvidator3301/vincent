using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.PickupableItem.Configs;
using Assets.Scripts.PickupableItem.Services;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.PickupableItem
{
    public class PickupableItemController: ControllerBase
    {
        private readonly PickupableItemConfig config;
        private readonly Dialogue dialogue;

        public PickupableItemController(GameObject gameObject, IServiceProvider serviceProvider, PickupableItemConfig config) : base(gameObject, serviceProvider)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            var marker = gameObject.GetComponent<PickupableItemMarker>();
            var dialogueParser = ServiceProvider.GetService<DialogueParser>();
            dialogue = dialogueParser.FromFile(marker.DialogueFile);
        }

        public override void Start()
        {
            AddPickupService();
            AddStartDialogueService();

            foreach (var service in Services) 
                service.Start();
        }

        private void AddStartDialogueService()
        {
            var marker = GameObject.GetComponent<PickupableItemMarker>();
            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();
            var iconForDialogueRepository = ServiceProvider.GetService<IconForDialogueRepository>();
            var id = marker.Id;
            var vincentSprite = Resources.Load<Sprite>("vincent");
            var startDialogueService = new StartItemDialogueService(startDialogueEventRepository,
                dialogueRepository, id, vincentSprite, iconForDialogueRepository, dialogue);

            Services.Add(startDialogueService);
        }

        public override void Update()
        {
            foreach (var service in Services)
                service.Update();
        }

        private void AddPickupService()
        {
            var repository = ServiceProvider.GetService<AddToInventoryEventRepository>();
            var inventory = ServiceProvider.GetService<PlayerInventory>();
            var service = new AddToInventoryService(GameObject, config, repository, inventory);
            Services.Add(service);
        }
    }
}
