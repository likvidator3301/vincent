using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Models;
using Assets.Scripts.DialogueContainer.Repositories;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.DialogueContainer
{
    public class DialogueContainerController : ControllerBase
    {
        private readonly DialogueContainerMarker dialogueContainerMarker;
        private readonly DialogueModel dialogueModel;

        public DialogueContainerController(GameObject gameObject, IServiceProvider serviceProvider, DialogueContainerMarker dialogueContainerMarker) : base(gameObject, serviceProvider)
        {
            this.dialogueContainerMarker = dialogueContainerMarker;
            dialogueModel = DialogueModel.FromGameObject(gameObject);
        }

        public override void Start()
        {
            AddNpcImageService();
            AddDisplayTextService();
            AddCancelButtonService();
            AddDisplayDialogueService();

            foreach (var service in Services)
                service.Start();

            dialogueModel.GameObject.SetActive(false);
        }

        private void AddNpcImageService()
        {
            var iconForDialogueRepository = ServiceProvider.GetService<IconForDialogueRepository>();
            var service = new NpcImageService(iconForDialogueRepository, dialogueModel.IconContainer.Icon);

            Services.Add(service);
        }

        private void AddDisplayDialogueService()
        {
            var dialogueRepository = ServiceProvider.GetService<DialogueRepository>();
            var nextTextEventRepository = ServiceProvider.GetService<NewTextEventRepository>();

            var service = new DisplayDialogueService(dialogueRepository, nextTextEventRepository, dialogueModel);

            Services.Add(service);
        }

        private void AddDisplayTextService()
        {
            var newTextEventRepository = ServiceProvider.GetService<NewTextEventRepository>();
            var service = new DisplayTextService(newTextEventRepository, dialogueModel, dialogueContainerMarker);

            Services.Add(service);
        }

        private void AddCancelButtonService()
        {
            var finishDialogueEventRepository = ServiceProvider.GetService<FinishDialogueEventRepository>();
            var service = new FinishDialogueService(finishDialogueEventRepository, dialogueModel);

            Services.Add(service);
        }

        public override void Update()
        {
            foreach (var service in Services)
                service.Update();
        }
    }
}