using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class Name
    {
        public GameObject GameObject { get; }

        public Text Text { get; }

        public Name(GameObject gameObject)
        {
            GameObject = gameObject;
            Text = gameObject.GetComponent<Text>();
        }
    }
}