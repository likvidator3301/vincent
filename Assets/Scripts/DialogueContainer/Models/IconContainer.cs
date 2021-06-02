using UnityEngine;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class IconContainer
    {
        public GameObject GameObject { get; }

        public Icon Icon { get; }

        public IconContainer(GameObject gameObject, Icon icon)
        {
            GameObject = gameObject;
            Icon = icon;
        }
    }
}