using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class InventoryItem: MonoBehaviour
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name;
        public Sprite Sprite;
    }
}