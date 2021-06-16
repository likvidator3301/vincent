using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using UnityEngine;

namespace Assets.Scripts.PickupableItem.Services
{
    public class StartItemDialogueService: ServiceBase
    {
        private readonly StartDialogueEventRepository startDialogueEventRepository;
        private readonly DialogueRepository dialogueRepository;
        private readonly string itemId;
        private readonly Sprite iconForDialogue;
        private readonly IconForDialogueRepository iconForDialogueRepository;
        private readonly Dialogue dialogue;

        public StartItemDialogueService(
            StartDialogueEventRepository startDialogueEventRepository,
            DialogueRepository dialogueRepository,
            string itemId,
            Sprite iconForDialogue,
            IconForDialogueRepository iconForDialogueRepository,
            Dialogue dialogue)
        {
            this.startDialogueEventRepository = startDialogueEventRepository ?? throw new ArgumentNullException(nameof(startDialogueEventRepository));
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.itemId = itemId ?? throw new ArgumentNullException(nameof(itemId));
            this.iconForDialogue = iconForDialogue;
            this.iconForDialogueRepository = iconForDialogueRepository ?? throw new ArgumentNullException(nameof(iconForDialogueRepository));
            this.dialogue = dialogue;
        }

        public override void Update()
        {
            if (!startDialogueEventRepository.HasValue)
                return;

            var startDialogueEvent = startDialogueEventRepository.Value;

            if (startDialogueEvent.NpcId != itemId)
               return;

            iconForDialogueRepository.SetValue(iconForDialogue);
            dialogueRepository.SetValue(dialogue);

            startDialogueEventRepository.RemoveValue();
        }
    }
}
