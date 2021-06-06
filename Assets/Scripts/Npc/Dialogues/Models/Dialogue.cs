using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common.Events;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Inventory;
using Newtonsoft.Json;
using UnityEngine;

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
    {
        public static Dialogue GetDialogue(PlayerInventory playerInventory, TextAsset DialogueFile)
        {
            var jsonContent = DialogueFile.text;

            var nodes = JsonConvert.DeserializeObject<DialogueNode[]>(jsonContent);
            if (nodes == null)
                throw new ArgumentException($"Cannot parse dialogue file: {DialogueFile}");

            foreach (var node in nodes)
            {
                var links = Link.ParseLine(node.Body);
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        var answerNode = nodes.FirstOrDefault(n => n.Title == link.NextNodeId);
                        if (answerNode != null)
                        {
                            var answerCondition = answerNode.Tags;

                            if (answerCondition != "")
                            {
                                if (Condition.CheckCondition(answerCondition, playerInventory))
                                    node.Answers.Add(link.AnswerText, answerNode);
                            }
                            else
                                node.Answers.Add(link.AnswerText, answerNode);
                        }
                    }
                }
                node.SetText();
            }

            var root = nodes[0];

            var dialogue = new Dialogue();
            dialogue.SetValue(root);
            return dialogue;
        }
    }
}
