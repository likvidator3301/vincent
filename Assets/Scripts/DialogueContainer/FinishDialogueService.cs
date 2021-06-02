using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Models;
using Assets.Scripts.DialogueContainer.Repositories;
using Object = UnityEngine.Object;

namespace Assets.Scripts.DialogueContainer
{
    public class FinishDialogueService : ServiceBase
    {
        private readonly DialogueModel dialogueModel;
        private readonly FinishDialogueEventRepository finishDialogueEventRepository;

        public FinishDialogueService(FinishDialogueEventRepository finishDialogueEventRepository, DialogueModel dialogueModel)
        {
            this.dialogueModel = dialogueModel ?? throw new ArgumentNullException(nameof(dialogueModel));
            this.finishDialogueEventRepository = finishDialogueEventRepository ??
                                                 throw new ArgumentNullException(nameof(finishDialogueEventRepository));
        }

        public override void Update()
        {
            if (!finishDialogueEventRepository.HasValue)
                return;

            dialogueModel.GameObject.SetActive(false);
            foreach (var button in dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.Buttons)
            {
                Object.Destroy(button.GameObject);
            }
            dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.Buttons.Clear();
            
            finishDialogueEventRepository.RemoveValue();
        }
    }
}