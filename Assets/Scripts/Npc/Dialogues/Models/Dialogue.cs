using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Events;
using Assets.Scripts.Inventory;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Npc.Dialogues.Models
{

    public class Dialogue: AbstractSingleObjectRepository<Node>
    {
    }

    public sealed class DialogueParser
    {
        private readonly DialogueActionFactory dialogueActionFactory;
        private readonly DialogueConditionFactory dialogueConditionFactory;

        public DialogueParser(DialogueActionFactory dialogueActionFactory, DialogueConditionFactory dialogueConditionFactory)
        {
            this.dialogueActionFactory = dialogueActionFactory ?? throw new ArgumentNullException(nameof(dialogueActionFactory));
            this.dialogueConditionFactory = dialogueConditionFactory ?? throw new ArgumentNullException(nameof(dialogueConditionFactory));
        }


        private sealed class DialogueNodeFileModel
        {
            [JsonProperty("title")]
            public string Id { get; }

            public string Text { get; private set; }

            public string Command { get; private set; }

            public Dictionary<string, string> Answers { get; }

            [JsonConstructor]
            public DialogueNodeFileModel(string title, string body, string tags)
            {
                Id = title;
                Answers = new Dictionary<string, string>();
                Command = tags;
                ParseBody(body);
            }

            private void ParseBody(string body)
            {
                var splatted = body.Split('\n');
                Text = splatted.First();
                foreach (var answer in splatted.Skip(1))
                {
                    if (!answer.StartsWith("[[") || !answer.EndsWith("]]"))
                        throw new Exception($"Cannot parse dialogue node. Id = {Id}. Error in answer='{answer}'");
                    var text = answer.Split('|')[0];
                    var nextNodeId = answer.Split('|')[1];
                    text = text.Substring(2, text.Length - 2);
                    nextNodeId = nextNodeId.Substring(0, nextNodeId.Length - 2);
                    Answers[nextNodeId] = text;
                }
            }
        }

        public Dialogue FromFile(TextAsset dialogueFile)
        {
            try
            {
                var fileNodes = JsonConvert.DeserializeObject<DialogueNodeFileModel[]>(dialogueFile.text);
                if (fileNodes == null)
                    throw new ArgumentException($"Cannot parse dialogue file: {dialogueFile}");

                var fileNodesCache = fileNodes.ToDictionary(x => x.Id);
                if (!fileNodesCache.ContainsKey("Start"))
                    throw new ArgumentException($"Dialogue file {dialogueFile} doesn't contain 'Start' node");

                var root = fileNodesCache["Start"];
                var sorted = TopologicalSort(root, fileNodesCache);

                var dialogueNodesCache = new Dictionary<string, Node>();

                foreach (var fileNode in sorted)
                {
                    var command = dialogueActionFactory.CreateAction(fileNode.Command);
                    var answers = new List<AnswerLink>();
                    foreach (var kv in fileNode.Answers)
                    {
                        answers.Add(ParseAnswer(kv.Key, kv.Value, dialogueNodesCache));
                    }

                    var node = new Node(fileNode.Id, fileNode.Text, command, answers);
                    dialogueNodesCache[node.Id] = node;
                }

                var startNode = dialogueNodesCache["Start"];
                var dialogue = new Dialogue();
                dialogue.SetValue(startNode);

                return dialogue;
            }
            catch(Exception e)
            {
                Debug.Log($"Error occurs while parse file: {dialogueFile.name}");
                throw e;
            }
        }

        private AnswerLink ParseAnswer(string nextNodeId, string answer, Dictionary<string, Node> dialogueNodesCache)
        {
            var isConditionHere = TryExtractCondition(answer, out var conditionString);
            var condition = new Func<bool>(() => true);
            if (isConditionHere) 
                condition = dialogueConditionFactory.CreateCondition(conditionString);
            var text = ExtractText(answer, conditionString);
            return new AnswerLink
            {
                Node = dialogueNodesCache[nextNodeId],
                ShowingCondition = condition,
                Text = text
            };
        }

        private string ExtractText(string answer, string conditionString)
        {
            return conditionString == null ? answer : answer.Replace("<<" + conditionString + ">>", "");
        }

        private bool TryExtractCondition(string answer, out string condition)
        {
            condition = null;
            if (!answer.StartsWith("<<"))
                return false;
            var endOfConditionIndex = answer.IndexOf(">>", StringComparison.InvariantCultureIgnoreCase);
            condition = answer.Substring(2, endOfConditionIndex - 2);
            return true;
        }

        private List<DialogueNodeFileModel> TopologicalSort(DialogueNodeFileModel root, Dictionary<string, DialogueNodeFileModel> cache)
        {
            var stack = new Stack<string>();
            stack.Push(root.Id);
            var result = new List<string>();
            while (stack.Count > 0)
            {
                var nodeId = stack.Peek();
                var node = cache[nodeId];
                if (node.Answers.Count == 0 || node.Answers.Keys.All(x => result.Contains(x)))
                {
                    result.Add(node.Id);
                    stack.Pop();
                }
                else
                {
                    foreach (var answer in node.Answers)
                    {
                        stack.Push(answer.Key);
                    }
                }
            }

            return result.Select(x => cache[x]).ToList();
        }
    }

    public sealed class Node
    {
        public string Id { get; }
        public string Text { get; }
        public Action Command { get; }
        public List<AnswerLink> Answers { get; }

        public Node(string id, string text, Action command, List<AnswerLink> answers)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Answers = answers;
        }
    }

    public sealed class AnswerLink
    {
        public Node Node { get; set; }

        public string Text { get; set; }

        public Func<bool> ShowingCondition { get; set; }
    }
}
