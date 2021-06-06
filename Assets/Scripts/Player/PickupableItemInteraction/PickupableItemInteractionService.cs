using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.PickupableItemInteraction.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.PickupableItemInteraction
{
    public class PickupableItemInteractionService: ServiceBase
    {
        private readonly PlayerConfig config;
        private readonly MovementEventRepository movementEventRepository;
        private readonly InteractWithPickupableItemEventRepository interactWithPickupableItemEventRepository;
        private readonly StartDialogueEventRepository startDialogueEventRepository;
        private readonly Transform player;

        public PickupableItemInteractionService(
            PlayerConfig config,
            MovementEventRepository movementEventRepository,
            InteractWithPickupableItemEventRepository interactWithNpcEventRepository,
            StartDialogueEventRepository startDialogueEventRepository,
            Transform player)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.interactWithPickupableItemEventRepository = interactWithNpcEventRepository ?? throw new ArgumentNullException(nameof(interactWithNpcEventRepository));
            this.startDialogueEventRepository = startDialogueEventRepository ?? throw new ArgumentNullException(nameof(startDialogueEventRepository));
            this.player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public override void Update()
        {
            if (!interactWithPickupableItemEventRepository.HasValue)
                return;

            var interactEvent = interactWithPickupableItemEventRepository.Value;

            var gameObject = interactEvent.Marker.gameObject;

            if (PositionHelper.GetDistance(player, gameObject.transform) <= config.InteractCriticalDistance)
            {
                interactWithPickupableItemEventRepository.RemoveValue();
                movementEventRepository.RemoveValue();

                startDialogueEventRepository.SetValue(new StartDialogueEvent(interactEvent.Marker.Id));
            }
        }
    }
}
