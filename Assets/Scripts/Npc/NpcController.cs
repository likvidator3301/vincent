using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.Npc
{
    public class NpcController: ControllerBase
    {
        public NpcController(GameObject gameObject, IServiceProvider serviceProvider) : base(gameObject, serviceProvider)
        {
        }

        public override void Start()
        {
            CreateStartDialogueService();

            foreach (var service in Services)
                service.Start();
        }

        private void CreateStartDialogueService()
        {
            var marker = GameObject.GetComponent<NpcMarker>();
            var playerInventory = ServiceProvider.GetService<PlayerInventory>();

            try
            {
                marker.GetDialogue(playerInventory);
            }
            catch (Exception e)
            {
                throw new GameInitializationException($"An error occurred while trying to load dialogue. {e.Message}");
            }

            if (marker.Name == "Duck")
                return;

            var id = marker.Id;

            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();
            var iconForDialogueRepository = ServiceProvider.GetService<IconForDialogueRepository>();

            var startDialogueService = new StartDialogueService(marker, startDialogueEventRepository, 
                dialogueRepository, id, marker.IconForDialogue, iconForDialogueRepository, playerInventory);

            Services.Add(startDialogueService);
        }

        public override void Update()
        {
            foreach (var service in Services) 
                service.Update();
        }
    }
}
