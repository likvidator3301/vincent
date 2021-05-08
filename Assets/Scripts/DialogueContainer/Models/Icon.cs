using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class Icon
    {
        public GameObject GameObject { get; }

        public Image Image { get; }

        public Icon(GameObject gameObject)
        {
            GameObject = gameObject;
            Image = gameObject.GetComponent<Image>();
        }
    }
}