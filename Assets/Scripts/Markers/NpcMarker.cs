using System;
using System.IO;
using System.Linq;
using Assets.Scripts.Common.Extensions;
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

        public Dialogue GetDialogue()
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
                            node.Answers.Add(link.AswerText, answerNode);
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
