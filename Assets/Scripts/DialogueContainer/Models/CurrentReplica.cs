using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class CurrentReplica
    {
        public GameObject GameObject { get; }

        public Text Text { get; }

        public CurrentReplica(GameObject gameObject)
        {
            GameObject = gameObject;
            Text = gameObject.GetComponent<Text>();
        }
    }
}