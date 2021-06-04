using System;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Markers;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class SceneTransferModel
    {
        public string Id { get; }
        public Assets.Scenes Scene { get; }
        public Transform SpawnPoint { get; }

        private SceneTransferModel(string id, Assets.Scenes scene, Transform spawnPoint)
        {
            Scene = scene;
            SpawnPoint = spawnPoint;
            Id = id;
        }

        public static SceneTransferModel FromMarker(SceneTransferMarker marker)
        {
            var spawnPoint = marker.SpawnPoint.transform;

            return new SceneTransferModel(marker.Id, marker.Scene, spawnPoint);
        }
    }
}
