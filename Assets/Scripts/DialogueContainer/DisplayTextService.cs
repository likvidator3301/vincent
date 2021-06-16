using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Models;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.DialogueContainer
{
    public class DisplayTextService : ServiceBase
    {
        private const int ScrollRectWidth = 818;
        private readonly DialogueModel dialogueModel;
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly DialogueContainerMarker dialogueContainerMarker;

        public DisplayTextService(NewTextEventRepository newTextEventRepository, DialogueModel dialogueModel, DialogueContainerMarker dialogueContainerMarker)
        {
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.dialogueContainerMarker = dialogueContainerMarker ?? throw new ArgumentNullException(nameof(dialogueContainerMarker));
            this.dialogueModel = dialogueModel ?? throw new ArgumentNullException(nameof(dialogueModel));
        }

        private void ShowDialogueState(Node node)
        {
            dialogueModel.CurrentReplica.Text.text = node.Text;
            var posY = 75;
            var dy = -75;
            foreach (var answer in node.Answers)
            {
                if (!answer.ShowingCondition())
                    continue;
                MakeButton(answer.Text, answer.Node, posY);
                posY += dy;
            }
        }

        private void MakeButton(string answer, Node nextNode, int y)
        {
            var buttonGameObject = Object.Instantiate(dialogueContainerMarker.NextReplicaButtonPrefab, dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.GameObject.transform);
            buttonGameObject.gameObject.transform.localPosition = new Vector3(0, y);
            var buttonModel = new NextReplicaButton(buttonGameObject);
            buttonModel.Text.text = answer;
            buttonGameObject.GetComponent<NextReplicaDialogueButtonMarker>().Node = nextNode;
            dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.Buttons.Add(buttonModel);
        }

        private void DestroyButtons()
        {
            foreach (var button in dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.Buttons) Object.Destroy(button.GameObject);
            dialogueModel.NextReplicaNextReplicaButtonsContainerContainer.Buttons.Clear();
        }

        public override void Update()
        {
            if (!newTextEventRepository.HasValue)
                return;

            var currentNode = newTextEventRepository.Value.Node;
            currentNode.Command();
            DestroyButtons();
            ShowDialogueState(currentNode);
            newTextEventRepository.RemoveValue();
        }
    }
}