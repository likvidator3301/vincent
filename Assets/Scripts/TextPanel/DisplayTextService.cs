using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.TextPanel.Repositories;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TextPanel
{
    public class DisplayTextService : ServiceBase
    {
        private readonly Text dialogueText;
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly ScrollRect scrollRect;
        private readonly ButtonMarker button;
        private List<ButtonMarker> buttons = new List<ButtonMarker>();

        private const int ScrollRectWidth = 818;

        public DisplayTextService(Text dialogueText, NewTextEventRepository newTextEventRepository, ScrollRect scrollRect, ButtonMarker button)
        {
            this.dialogueText = dialogueText ?? throw new ArgumentNullException(nameof(dialogueText));
            this.scrollRect = scrollRect ?? throw new ArgumentNullException(nameof(scrollRect));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.button = button ?? throw new ArgumentNullException(nameof(button));
        }

        private void ShowDialogueState(DialogueNode node)
        {
            var width = scrollRect.flexibleWidth;
            DestroyButtons(buttons);
            dialogueText.text = node.Text;
            var posX = -280;
            var dx = ScrollRectWidth / (node.Answers.Count + 1);
            foreach(var answer in node.Answers)
            {
                var newButton = button.Instantiate();
                newButton.gameObject.transform.localPosition = new Vector3(posX, 115);
                newButton.DialogueNode = answer.Value;
                newButton.ButtonText.text = answer.Key;
                buttons.Add(newButton);
                posX += dx;
            }
            var finishButton = button.Instantiate();
            finishButton.gameObject.transform.localPosition = new Vector3(posX, 115);
            finishButton.ButtonText.text = "Завершить диалог";
            finishButton.DialogueNode = new DialogueNode("end");
            buttons.Add(finishButton);
        }

        private void DestroyButtons(List<ButtonMarker> buttons)
        {
            foreach(var button in buttons)
            {
                button.DestroyButton();
            }
            buttons.Clear();
        }

        public override void Update()
        {
            if (!newTextEventRepository.HasValue)
                return;
            var currentNode = newTextEventRepository.Value.node;
            if (currentNode.Text != "end")
            {
                scrollRect.gameObject.SetActive(true);
                ShowDialogueState(currentNode);
            }
            else
            {
                scrollRect.gameObject.SetActive(false);
                DestroyButtons(buttons);
            }
            newTextEventRepository.RemoveValue();
        }
    }
}
