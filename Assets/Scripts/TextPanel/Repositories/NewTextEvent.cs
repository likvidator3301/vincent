using Assets.Scripts.Npc.Dialogues.Models;
using System;

namespace Assets.Scripts.TextPanel.Repositories
{
    public class NewTextEvent
    {
        public DialogueNode node;

        public NewTextEvent (DialogueNode node)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }
    }
}
