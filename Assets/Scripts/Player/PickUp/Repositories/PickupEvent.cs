using System;
using UnityEngine;

namespace Assets.Scripts.Player.PickUp.Repositories
{
    public class PickupEvent
    {
        public string Id { get; }

        public GameObject GameObject { get; }

        public PickupEvent(string id, GameObject gameObject)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            GameObject = gameObject;
        }
    }
}
