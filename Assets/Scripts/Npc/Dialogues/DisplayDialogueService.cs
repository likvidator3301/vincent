using System;
using Assets.Scripts.Common;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.TextPanel.Repositories;
using UnityEngine;
using UnityEngine.UI;

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

        public static void ShowText(DialogueNode node)
        {
            if (node == null)
                return;
            Debug.Log("Text: " + node.Text);
            foreach(var answer in node.Answers)
            {
                Debug.Log("Answer: " + answer.Key);
            }
        }

        public override void Update()
        {
            if (!dialogueRepository.HasValue)
                return;

            var dialogue = dialogueRepository.Value;

            //ShowText(dialogue.Value);
            newTextEventRepository.SetValue(new NewTextEvent(dialogue.Value));
            dialogueRepository.RemoveValue();
        }
    }
}
