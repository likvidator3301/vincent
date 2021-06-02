using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class NextReplicaButtonsContainer
    {
        public GameObject GameObject { get; }

        public List<NextReplicaButton> Buttons { get; }

        public NextReplicaButtonsContainer(GameObject gameObject)
        {
            GameObject = gameObject;
            Buttons = new List<NextReplicaButton>();
        }
    }

    public class NextReplicaButton
    {
        public GameObject GameObject { get; }

        public Text Text { get; }

        public NextReplicaButton(GameObject gameObject)
        {
            GameObject = gameObject;
            Text = gameObject.transform.Find("Text").GetComponent<Text>();
        }
    }
}