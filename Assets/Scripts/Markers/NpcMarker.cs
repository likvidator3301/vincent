using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Npc.Dialogues.Models;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class NpcMarker: MonoBehaviour
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public Dialogue GetDialogue()
        {
            var result = new Dialogue();

            var root = new DialogueNode("Привет! Я красный непись.");

            var node1 = new DialogueNode("Я живу здесь уже 2 часа, а ты?");
            node1.Answers.Add("А мне 69 лет", root);
            node1.Answers.Add("Какого хуя я тебя это должен рассказывать?", root);

            root.Answers.Add("Привет! Я кот. Сколько тебе лет?", node1);

            var node2 = new DialogueNode("У меня все хорошо, как ты?");
            node2.Answers.Add("У меня все плохо, хочу звезду", root);

            root.Answers.Add("Привет! Я кот. Как у тебя дела?", node2);

            result.SetValue(root);

            return result;
        }
    }
}
