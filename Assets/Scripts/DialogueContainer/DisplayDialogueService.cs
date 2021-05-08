using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Models;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Npc.Dialogues.Repositories;

namespace Assets.Scripts.DialogueContainer
{
    public class DisplayDialogueService: ServiceBase
    {
        private readonly DialogueRepository dialogueRepository;
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly DialogueModel dialogueModel;

        public DisplayDialogueService(DialogueRepository dialogueRepository, NewTextEventRepository newTextEventRepository, DialogueModel dialogueModel)
        {
            this.dialogueRepository = dialogueRepository ?? throw new ArgumentNullException(nameof(dialogueRepository));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.dialogueModel = dialogueModel ?? throw new ArgumentNullException(nameof(dialogueModel));
        }

        public override void Update()
        {
            if (!dialogueRepository.HasValue)
                return;

            var dialogue = dialogueRepository.Value;

            dialogueModel.GameObject.SetActive(true);

            newTextEventRepository.SetValue(new NewTextEvent(dialogue.Value));
            dialogueRepository.RemoveValue();
        }
    }
}
