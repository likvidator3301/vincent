using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.Npc
{
    public class NpcController: ControllerBase
    {
        private readonly Dialogue dialogue;

        public NpcController(GameObject gameObject, IServiceProvider serviceProvider) : base(gameObject, serviceProvider)
        {
            var marker = GameObject.GetComponent<NpcMarker>();
            var dialogueParser = ServiceProvider.GetService<DialogueParser>();
            dialogue = dialogueParser.FromFile(marker.DialogueFile);
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

            if (marker.Name == "Duck")
                return;

            var id = marker.Id;

            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();
            var iconForDialogueRepository = ServiceProvider.GetService<IconForDialogueRepository>();
            var nameRepository = ServiceProvider.GetService<NameRepository>();

            var startDialogueService = new StartDialogueService(startDialogueEventRepository, 
                dialogueRepository, id, marker.IconForDialogue, iconForDialogueRepository, nameRepository, dialogue);

            Services.Add(startDialogueService);
        }

        public override void Update()
        {
            foreach (var service in Services) 
                service.Update();
        }
    }
}
