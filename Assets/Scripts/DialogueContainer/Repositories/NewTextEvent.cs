using System;
using Assets.Scripts.Npc.Dialogues.Models;

namespace Assets.Scripts.DialogueContainer.Repositories
{
    public class NewTextEvent
    {
        public NewTextEvent(DialogueNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public DialogueNode Node { get; }
    }
}