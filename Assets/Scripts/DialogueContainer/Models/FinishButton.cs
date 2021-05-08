using Assets.Scripts.Markers;
using UnityEngine;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class FinishButton
    {
        public GameObject GameObject { get; }

        public FinishDialogueButtonMarker Marker { get; }

        public FinishButton(GameObject gameObject)
        {
            GameObject = gameObject;
            Marker = gameObject.GetComponent<FinishDialogueButtonMarker>();
        }
    }
}