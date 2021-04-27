using System.Collections.Generic;
using Assets.Scripts.Common.Events;

namespace Assets.Scripts.Npc.Dialogues.Models
{
    public class DialogueNode
    {
        public string Text { get; }

        public Dictionary<string, DialogueNode> Answers { get; }

        public DialogueNode(string text)
        {
            Text = text;
            Answers = new Dictionary<string, DialogueNode>();
        }
    }

    public class Dialogue: AbstractSingleObjectRepository<DialogueNode>
    { }
}
