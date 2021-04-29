using System;
using Assets.Scripts.Common;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using UnityEngine;

namespace Assets.Scripts.Npc.Dialogues
{
    public class StartDialogueService: ServiceBase
    {
        private readonly Dialogue dialogue;
        private readonly StartDialogueEventRepository startDialogueEventRepository;
        private readonly DialogueRepository dialogueRepository;
        private readonly string npcId;

        public StartDialogueService(Dialogue dialogue, StartDialogueEventRepository startDialogueEventRepository, DialogueRepository dialogueRepository, string npcId)
        {
            this.dialogue = dialogue ?? throw new ArgumentNullException(nameof(dialogue));
            this.startDialogueEventRepository = startDialogueEventRepository ?? throw new ArgumentNullException(nameof(startDialogueEventRepository));
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.npcId = npcId ?? throw new ArgumentNullException(nameof(npcId));
        }

        public override void Update()
        {
            if (!startDialogueEventRepository.HasValue)
                return;

            var startDialogueEvent = startDialogueEventRepository.Value;
            
            if (startDialogueEvent.NpcId != npcId)
                return;

            Debug.Log("Типа начал разговаривать");

            dialogueRepository.SetValue(dialogue);
            startDialogueEventRepository.RemoveValue();
        }
    }
}
