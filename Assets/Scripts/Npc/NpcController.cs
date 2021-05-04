using System;
using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.TextPanel.Repositories;
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
            CreateDisplayDialogueService();

            foreach (var service in Services)
                service.Start();
        }

        private void CreateStartDialogueService()
        {
            var marker = GameObject.GetComponent<NpcMarker>();
            var id = marker.Id;
            var dialogue = marker.GetDialogue();

            var startDialogueEventRepository = ServiceProvider.GetService<StartDialogueEventRepository>();
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();

            var startDialogueService = new StartDialogueService(dialogue, startDialogueEventRepository, dialogueRepository, id);

            Services.Add(startDialogueService);
        }

        private void CreateDisplayDialogueService()
        {
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();
            var newTextEventRepository = ServiceProvider.GetService<NewTextEventRepository>();

            var displayDialogueService = new DisplayDialogueService(dialogueRepository, newTextEventRepository);

            Services.Add(displayDialogueService);
        }

        public override void Update()
        {
            foreach (var service in Services) 
                service.Update();
        }
    }
}
