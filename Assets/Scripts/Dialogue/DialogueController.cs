using System;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;


namespace Assets.Scripts.Dialogue
{
    public class DialogueController : ControllerBase
    {
        private readonly List<ServiceBase> services;

        public DialogueController(GameObject gameObject, IServiceProvider serviceProvider): base(gameObject, serviceProvider)
        {
            services = new List<ServiceBase>();
        }

        [UsedImplicitly]
        public override void Update()
        {
            foreach(var service in services)
                service.Update();
        }

        [UsedImplicitly]
        public override void FixedUpdate()
        {
            foreach (var service in services)
                service.FixedUpdate();
        }
    }
}
