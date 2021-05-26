using System.Collections.Generic;
using Assets.Scripts.Common.Events;

namespace Assets.Scripts.Npc.Dialogues.Models
{
    public class DialogueNode
    {
        public string Text { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string tags { get; set; }

        public Dictionary<string, DialogueNode> Answers { get; }

        public DialogueNode(string text)
        {
            Text = text;
            Answers = new Dictionary<string, DialogueNode>();
        }

        public void SetText()
        {
            Text = "";
            foreach (var c in body)
            {
                if (c != '\n')
                    Text += c;
                else
                    break;
            }
        }
    }

    public class Dialogue: AbstractSingleObjectRepository<DialogueNode>
    { }
}
