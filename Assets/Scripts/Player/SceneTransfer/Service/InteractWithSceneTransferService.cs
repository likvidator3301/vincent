using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using Assets.Scripts.Scenes.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.SceneTransfer.Service
{
    public class InteractWithSceneTransferService: ServiceBase
    {
        private readonly MovementEventRepository movementRepository;
        private readonly PlayerConfig config;
        private readonly InteractWithSceneTransferEventRepository interactWithSceneTransferEventRepository;
        private readonly TransferToSceneEventRepository transferToSceneEventRepository;
        private readonly Transform playerTransform;

        public InteractWithSceneTransferService(MovementEventRepository movementRepository,
                                                PlayerConfig config,
                                                InteractWithSceneTransferEventRepository interactWithSceneTransferEventRepository,
                                                TransferToSceneEventRepository transferToSceneEventRepository,
                                                Transform playerTransform)
        {
            this.movementRepository = movementRepository ?? throw new ArgumentNullException(nameof(movementRepository));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.interactWithSceneTransferEventRepository = interactWithSceneTransferEventRepository ??
                                                            throw new ArgumentNullException(
                                                                nameof(interactWithSceneTransferEventRepository));
            this.transferToSceneEventRepository = transferToSceneEventRepository ??
                                                  throw new ArgumentNullException(nameof(transferToSceneEventRepository));
            this.playerTransform = playerTransform ?? throw new ArgumentNullException(nameof(playerTransform));
            
        }

        public override void Update()
        {
            if (!interactWithSceneTransferEventRepository.HasValue)
                return;

            var marker = interactWithSceneTransferEventRepository.Value.Marker;

            if (PositionHelper.GetDistance(playerTransform, marker.gameObject.transform) <=
                config.InteractCriticalDistance)
            {
                movementRepository.RemoveValue();
                interactWithSceneTransferEventRepository.RemoveValue();

                transferToSceneEventRepository.SetValue(new TransferToSceneEvent(marker.Id));
            }
        }
    }
}
