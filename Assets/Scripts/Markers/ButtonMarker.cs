using Assets.Scripts.Npc.Dialogues.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Markers
{
    public class ButtonMarker: MonoBehaviour
    {
        public Text buttonText;
        public DialogueNode dialogueNode;

        public ButtonMarker Instantiate()
        {
            return Instantiate(this, GameObject.Find("Canvas").transform);
        }

        public void DestroyButton()
        {
            Destroy(gameObject);
        }
    }
}
