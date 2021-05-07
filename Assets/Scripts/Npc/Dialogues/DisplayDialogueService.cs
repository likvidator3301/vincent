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
        private readonly Sprite npcSprite;

        public DisplayDialogueService(DialogueRepository dialogueRepository, NewTextEventRepository newTextEventRepository, Sprite npcSprite)
        {
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.npcSprite = npcSprite ?? throw new ArgumentNullException(nameof(npcSprite));
        }

        public override void Update()
        {
            if (!dialogueRepository.HasValue)
                return;

            var dialogue = dialogueRepository.Value;

            newTextEventRepository.SetValue(new NewTextEvent(dialogue.Value, npcSprite));
            dialogueRepository.RemoveValue();
        }
    }
}
