using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class SceneTransferMarker: MonoBehaviour
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        [UsedImplicitly]
        public Assets.Scenes Scene;

        [UsedImplicitly]
        public GameObject SpawnPoint;
    }
}
