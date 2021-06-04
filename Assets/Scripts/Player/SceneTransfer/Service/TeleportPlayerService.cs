using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.SceneTransfer.Service
{
    public class TeleportPlayerService: ServiceBase
    {
        private readonly Transform playerTransform;
        private readonly TeleportPlayerEventRepository teleportPlayerEventRepository;

        public TeleportPlayerService(Transform playerTransform, TeleportPlayerEventRepository teleportPlayerEventRepository)
        {
            this.playerTransform = playerTransform ?? throw new ArgumentNullException(nameof(playerTransform));
            this.teleportPlayerEventRepository = teleportPlayerEventRepository ??
                                                 throw new ArgumentNullException(nameof(teleportPlayerEventRepository));
        }

        public override void Update()
        {
            if (!teleportPlayerEventRepository.HasValue)
                return;

            var x = teleportPlayerEventRepository.Value.X;
            playerTransform.position = new Vector3(x, playerTransform.position.y, playerTransform.position.z);

            var cameraSceneManager = Camera.main.GetComponent<CameraSceneManager>();
            cameraSceneManager.GoToScene(teleportPlayerEventRepository.Value.Scene);

            teleportPlayerEventRepository.RemoveValue();
        }
    }
}
