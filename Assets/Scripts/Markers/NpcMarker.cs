using System;
using System.IO;
using System.Linq;
using Assets.Scripts.Common.Extensions;
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

        public Dialogue GetDialogue(PlayerInventory items)
        {
            var result = new Dialogue();

            var json = File.ReadAllText(@".\Assets\resources\Dialogues\" + Name + ".json");
            DialogueNode[] nodes = JsonConvert.DeserializeObject<DialogueNode[]>(json);
            foreach (var node in nodes)
            {
                var links = Link.ParseLine(node.body);
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        var answerNode = nodes.Where(n => n.title == link.NextNodeId).FirstOrDefault();
                        if (answerNode != null)
                        {
                            var answerCondition = answerNode.tags;

                            if (answerCondition != "")
                            {
                                if (Condition.CheckCondition(answerCondition, items))
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
            result.SetValue(root);
            return result;
        }
    }
}
