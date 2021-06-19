﻿using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Inventory;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using UnityEngine;

namespace Assets.Scripts.Npc.Dialogues
{
    public class StartDialogueService: ServiceBase
    {
        private readonly StartDialogueEventRepository startDialogueEventRepository;
        private readonly DialogueRepository dialogueRepository;
        private readonly string npcId;
        private readonly Sprite iconForDialogue;
        private readonly IconForDialogueRepository iconForDialogueRepository;
        private readonly NameRepository nameRepository;
        private readonly Dialogue dialogue;

        public StartDialogueService(
            StartDialogueEventRepository startDialogueEventRepository,
            DialogueRepository dialogueRepository,
            string npcId,
            Sprite iconForDialogue,
            IconForDialogueRepository iconForDialogueRepository,
            NameRepository nameRepository,
            Dialogue dialogue)
        {
            this.startDialogueEventRepository = startDialogueEventRepository ?? throw new ArgumentNullException(nameof(startDialogueEventRepository));
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.npcId = npcId ?? throw new ArgumentNullException(nameof(npcId));
            this.iconForDialogue = iconForDialogue;
            this.iconForDialogueRepository = iconForDialogueRepository ?? throw new ArgumentNullException(nameof(iconForDialogueRepository));
            this.nameRepository = nameRepository ?? throw new ArgumentNullException(nameof(nameRepository));
            this.dialogue = dialogue;
        }

        public override void Update()
        {
            if (!startDialogueEventRepository.HasValue)
                return;

            var startDialogueEvent = startDialogueEventRepository.Value;

            if (startDialogueEvent.NpcId != npcId)
                return;

            nameRepository.SetValue(startDialogueEvent.Name);
            iconForDialogueRepository.SetValue(iconForDialogue);
            dialogueRepository.SetValue(dialogue);

            startDialogueEventRepository.RemoveValue();
        }
    }
}
