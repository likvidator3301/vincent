using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using Assets.Scripts.Scenes.Repositories;
using UnityEngine;

namespace Assets.Scripts.Scenes.Services
{
    public class TransferToSceneService: ServiceBase
    {
        private readonly SceneTransferModel model;
        private readonly TransferToSceneEventRepository transferToSceneEventRepository;
        private readonly TeleportPlayerEventRepository teleportPlayerEventRepository;

        public TransferToSceneService(SceneTransferModel model,
                                      TransferToSceneEventRepository transferToSceneEventRepository,
                                      TeleportPlayerEventRepository teleportPlayerEventRepository)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.transferToSceneEventRepository = transferToSceneEventRepository ?? throw new ArgumentNullException(nameof(transferToSceneEventRepository));
            this.teleportPlayerEventRepository = teleportPlayerEventRepository ?? throw new ArgumentNullException(nameof(teleportPlayerEventRepository));
        }

        public override void Update()
        {
            if (!transferToSceneEventRepository.HasValue || transferToSceneEventRepository.Value.MarkerId != model.Id)
                return;

            var spawnPoint = model.SpawnPoint;
            teleportPlayerEventRepository.SetValue(new TeleportPlayerEvent(spawnPoint.position.x, model.Scene));

            transferToSceneEventRepository.RemoveValue();
        }
    }
}
