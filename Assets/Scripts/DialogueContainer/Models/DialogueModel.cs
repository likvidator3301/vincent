using Assets.Scripts.Exceptions;
using UnityEngine;

namespace Assets.Scripts.DialogueContainer.Models
{
    public class DialogueModel
    {
        public GameObject GameObject { get; }

        public NextReplicaButtonsContainer NextReplicaNextReplicaButtonsContainerContainer { get; }

        public FinishButton FinishButton { get; }

        public IconContainer IconContainer { get; }

        public CurrentReplica CurrentReplica { get; }

        private DialogueModel(GameObject gameObject, NextReplicaButtonsContainer nextReplicaNextReplicaButtonsContainerContainer, FinishButton finishButton, IconContainer iconContainer, CurrentReplica currentReplica)
        {
            GameObject = gameObject;
            NextReplicaNextReplicaButtonsContainerContainer = nextReplicaNextReplicaButtonsContainerContainer;
            FinishButton = finishButton;
            IconContainer = iconContainer;
            CurrentReplica = currentReplica;
        }

        public static DialogueModel FromGameObject(GameObject gameObject)
        {
            var finishButtonTransform = gameObject.transform.Find("FinishButton");
            if (finishButtonTransform == null)
                throw new GameInitializationException("Cannot contruct dialogue model. Cannot find FinishButton in DialogueContainer");

            var finishButton = new FinishButton(finishButtonTransform.gameObject);

            var buttonsContainerTransform = gameObject.transform.Find("ButtonsContainer");
            if (buttonsContainerTransform == null)
                throw new GameInitializationException("Cannot contruct dialogue model. Cannot find ButtonsContainer in DialogueContainer");

            var buttonsContainer = new NextReplicaButtonsContainer(buttonsContainerTransform.gameObject);

            var iconContainerTransform = gameObject.transform.Find("IconContainer");
            if (buttonsContainerTransform == null)
                throw new GameInitializationException("Cannot contruct dialogue model. Cannot find IconContainer in DialogueContainer");

            var iconTransform = iconContainerTransform.Find("Icon");
            if (iconTransform == null)
                throw new GameInitializationException("Cannot contruct dialogue model. Cannot find Icon in IconContainer");

            var icon = new Icon(iconTransform.gameObject);
            var iconContainer = new IconContainer(iconContainerTransform.gameObject, icon);

            var currentReplicaTransform = gameObject.transform.Find("CurrentReplica");
            if (currentReplicaTransform == null)
                throw new GameInitializationException("Cannot contruct dialogue model. Cannot find CurrentReplica in DialogueContainer");

            var currentReplica = new CurrentReplica(currentReplicaTransform.gameObject);

            return new DialogueModel(gameObject, buttonsContainer, finishButton, iconContainer, currentReplica);
        }
    }
}
