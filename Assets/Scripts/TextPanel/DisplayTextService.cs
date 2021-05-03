using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.TextPanel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TextPanel
{
    class DisplayTextService : ServiceBase
    {
        private readonly Text dialogueText;
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly ScrollRect scrollRect;
        private readonly ButtonMarker button;
        private const int scrollRectWidth = 818;
        private List<ButtonMarker> buttons = new List<ButtonMarker>();

        public DisplayTextService(Text dialogueText, NewTextEventRepository newTextEventRepository, ScrollRect scrollRect, ButtonMarker button)
        {
            this.dialogueText = dialogueText ?? throw new ArgumentNullException(nameof(dialogueText));
            this.scrollRect = scrollRect ?? throw new ArgumentNullException(nameof(scrollRect));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.button = button ?? throw new ArgumentNullException(nameof(button));
        }

        public void ShowDialogueState(DialogueNode node)
        {
            DestroyButtons(buttons);
            dialogueText.text = node.Text;
            int posX = -280;
            var dx = scrollRectWidth / (node.Answers.Count + 1);
            foreach(var answer in node.Answers)
            {
                var newButton = button.Instantiate();
                newButton.gameObject.transform.localPosition = new Vector3(posX, 115);
                newButton.dialogueNode = answer.Value;
                newButton.buttonText.text = answer.Key;
                buttons.Add(newButton);
                posX += dx;
            }
            var endButton = button.Instantiate();
            endButton.gameObject.transform.localPosition = new Vector3(posX, 115);
            endButton.buttonText.text = "Завершить диалог";
            endButton.dialogueNode = new DialogueNode("end");
            buttons.Add(endButton);
        }

        public void DestroyButtons(List<ButtonMarker> buttons)
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
