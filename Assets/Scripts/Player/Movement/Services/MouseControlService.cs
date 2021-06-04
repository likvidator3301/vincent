using System;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Helpers;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Markers;
using Assets.Scripts.Player.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.NpcInteraction.Repositories;
using Assets.Scripts.Player.PickUp.Repositories;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using UnityEngine;
using UnityEngine.UI;
using MouseHelper = Assets.Scripts.Common.Helpers.MouseHelper;

namespace Assets.Scripts.Player.Movement.Services
{
    public sealed class MouseControlService: ServiceBase
    {
        private readonly MovementEventRepository movementEventRepository;
        private readonly DirectionHelper directionHelper;
        private readonly Transform player;
        private readonly PickupEventRepository pickupEventRepository;
        private readonly InteractWithNpcEventRepository interactWithNpcEventRepository;
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly FinishDialogueEventRepository finishDialogueEventRepository;
        private readonly InteractWithSceneTransferEventRepository interactWithSceneTransferEventRepository;
        private readonly PlayerConfig config;
        private Vector3 previousPointClicked;
        private Sprite cursor;

        public MouseControlService(
            Transform player,
            MovementEventRepository movementEventRepository,
            DirectionHelper directionHelper,
            PickupEventRepository pickupEventRepository,
            InteractWithNpcEventRepository interactWithNpcEventRepository,
            NewTextEventRepository newTextEventRepository,
            FinishDialogueEventRepository finishDialogueEventRepository,
            InteractWithSceneTransferEventRepository interactWithSceneTransferEventRepository,
            PlayerConfig config)
        {
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.directionHelper = directionHelper ?? throw new ArgumentNullException(nameof(directionHelper)); 
            this.pickupEventRepository = pickupEventRepository ?? throw new ArgumentNullException(nameof(pickupEventRepository));
            this.player = player;
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.finishDialogueEventRepository = finishDialogueEventRepository ?? throw new ArgumentNullException(nameof(finishDialogueEventRepository));
            this.interactWithSceneTransferEventRepository = interactWithSceneTransferEventRepository ?? throw new ArgumentNullException(nameof(interactWithSceneTransferEventRepository));
            this.interactWithNpcEventRepository = interactWithNpcEventRepository ?? throw new ArgumentNullException(nameof(interactWithNpcEventRepository));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            
            previousPointClicked = new Vector3(0, 0, 0);
        }

        public override void Update()
        {
            if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.PickupableItem) || MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Npc))
                cursor = Resources.Load<Sprite>("pointer");
            else
                cursor = Resources.Load<Sprite>("normal");

            Cursor.SetCursor(cursor.texture, new Vector2(0, 0), CursorMode.Auto);

            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition != previousPointClicked) // у нас происходит двойной клик. Для кнопок это критично
                {                                                // Поэтому я сохраняю предыдущую точку нажатия и сравниваю её с текущей
                    previousPointClicked = Input.mousePosition;
                    movementEventRepository.RemoveValue();
                    pickupEventRepository.RemoveValue();
                    interactWithNpcEventRepository.RemoveValue();
                    newTextEventRepository.RemoveValue();

                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Ground))
                        ProcessMovement();

                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.PickupableItem))
                    {
                        cursor = Resources.Load<Sprite>("pointer");
                        Cursor.SetCursor(cursor.texture, new Vector2(0, 0), CursorMode.Auto);
                        ProcessPickup();
                    }

                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.Npc))
                    {
                        cursor = Resources.Load<Sprite>("pointer");
                        Cursor.SetCursor(cursor.texture, new Vector2(0, 0), CursorMode.Auto);
                        ProcessNpc();
                    }

                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.DialogueButton))
                        ProcessButton();

                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.FinishDialogueButton))
                        ProcessFinishDialogue();
                    
                    if (MouseHelper.IsMouseAboveObjectWithTag(Constants.Tags.SceneTransfer))
                        ProcessSceneTransition();
                }
            }
        }

        private void ProcessFinishDialogue()
        {
            finishDialogueEventRepository.SetValue(new FinishDialogueEvent());
        }

        private void ProcessButton()
        {
            var button = MouseHelper.GetComponentOnGameObjectUnderMouse<NextReplicaDialogueButtonMarker>();
            newTextEventRepository.SetValue(new NewTextEvent(button.Node));
        }

        private void ProcessNpc()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<NpcMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            interactWithNpcEventRepository.SetValue(new InteractWithNpcEvent(marker));

            if (PositionHelper.GetDistance(player.position, destination) > config.InteractCriticalDistance)
                ProcessMovement();
        }

        private void ProcessPickup()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<PickupableItemMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            pickupEventRepository.SetValue(new PickupEvent(marker));

            if (PositionHelper.GetDistance(player.position, destination) > config.InteractCriticalDistance) 
                ProcessMovement();
        }

        private void ProcessSceneTransition()
        {
            var marker = MouseHelper.GetComponentOnGameObjectUnderMouse<SceneTransferMarker>();
            var destination = MouseHelper.GetPositionUnderMouse();

            interactWithSceneTransferEventRepository.SetValue(new InteractWithSceneTransferEvent(marker));

            if (PositionHelper.GetDistance(player.position, destination) > config.InteractCriticalDistance)
                ProcessMovement();
        }

        private void ProcessMovement()
        {
            var destination = MouseHelper.GetPositionUnderMouse();

            var movementEvent = new MovementEvent(destination);
            movementEventRepository.SetValue(movementEvent);

            SetDirection();
        }

        private void SetDirection()
        {
            var playerX = player.position.x;
            var destinationX = movementEventRepository.Value.Destination.x;

            directionHelper.Direction = destinationX > playerX ? Direction.Right : Direction.Left;
        }
    }
}
