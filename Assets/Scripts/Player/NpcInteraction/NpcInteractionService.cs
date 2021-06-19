using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.NpcInteraction.Repositories;
using UnityEngine;

namespace Assets.Scripts.Player.NpcInteraction
{
    public class NpcInteractionService: ServiceBase
    {
        private readonly PlayerConfig config;
        private readonly MovementEventRepository movementEventRepository;
        private readonly InteractWithNpcEventRepository interactWithNpcEventRepository;
        private readonly StartDialogueEventRepository startDialogueEventRepository;
        private readonly Transform player;

        public NpcInteractionService(
            PlayerConfig config,
            MovementEventRepository movementEventRepository,
            InteractWithNpcEventRepository interactWithNpcEventRepository,
            StartDialogueEventRepository startDialogueEventRepository,
            Transform player)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.interactWithNpcEventRepository = interactWithNpcEventRepository ?? throw new ArgumentNullException(nameof(interactWithNpcEventRepository));
            this.startDialogueEventRepository = startDialogueEventRepository ?? throw new ArgumentNullException(nameof(startDialogueEventRepository));
            this.player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public override void Update()
        {
            if (!interactWithNpcEventRepository.HasValue)
                return;

            var interactEvent = interactWithNpcEventRepository.Value;

            var gameObject = interactEvent.Marker.gameObject;

            if (PositionHelper.GetDistance(player, gameObject.transform) <= config.InteractCriticalDistance)
            {
                interactWithNpcEventRepository.RemoveValue();
                movementEventRepository.RemoveValue();

                startDialogueEventRepository.SetValue(new StartDialogueEvent(interactEvent.Marker.Id, interactEvent.Marker.Name));
            }
        }
    }
}
