using System;
using Assets.Scripts.Common;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.TextPanel.Repositories;
using UnityEngine;

namespace Assets.Scripts.Npc.Dialogues
{
    public class DisplayDialogueService: ServiceBase
    {
        private readonly DialogueRepository dialogueRepository;
        private readonly NewTextEventRepository newTextEventRepository;

        public DisplayDialogueService(DialogueRepository dialogueRepository, NewTextEventRepository newTextEventRepository)
        {
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
        }

        public override void Update()
        {
            if (!dialogueRepository.HasValue)
                return;

            var dialogue = dialogueRepository.Value;

            newTextEventRepository.SetValue(new NewTextEvent(dialogue.Value));
            dialogueRepository.RemoveValue();
        }
    }
}
