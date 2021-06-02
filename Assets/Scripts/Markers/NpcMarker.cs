using System;
using System.IO;
using System.Linq;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Npc.Dialogues.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class NpcMarker: MonoBehaviour
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name;
        public Sprite IconForDialogue;
        public string DialogueFilePath;

        public Dialogue GetDialogue(PlayerInventory playerInventory)
        {
            string jsonContent;

            if(File.Exists(DialogueFilePath))
                jsonContent = File.ReadAllText(DialogueFilePath);
            else
                throw new ArgumentException($"Cannot find path file by path '{DialogueFilePath}'");

            var nodes = JsonConvert.DeserializeObject<DialogueNode[]>(jsonContent);
            if (nodes == null)
                throw new ArgumentException($"Cannot parse dialogue file: {DialogueFilePath}");

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
