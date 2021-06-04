using UnityEngine;

namespace Assets.Scripts.Player.SceneTransfer.Repositories
{
    public class TeleportPlayerEvent
    {
        public float X { get; }
        public Assets.Scenes Scene { get; }

        public TeleportPlayerEvent(float x, Assets.Scenes scene)
        {
            X = x;
            Scene = scene;
        }
    }
}
