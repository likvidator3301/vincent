using System.Collections.Generic;
using Assets.Scripts.Common.Events;
using Newtonsoft.Json;

namespace Assets.Scripts.Npc.Dialogues.Models
{
    [JsonObject]
    public class DialogueNode
    {
        public string Text { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        public Dictionary<string, DialogueNode> Answers { get; }

        public DialogueNode(string text)
        {
            Text = text;
            Answers = new Dictionary<string, DialogueNode>();
        }

        public void SetText()
        {
            Text = Body.Split('\n')[0];
        }
    }

    public class Dialogue: AbstractSingleObjectRepository<DialogueNode>
    { }
}
