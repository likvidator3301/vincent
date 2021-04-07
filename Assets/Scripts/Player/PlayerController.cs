using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts.Common;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using JetBrains.Annotations;
using UnityEngine;


//lividator: я очень сильно за то, чтобы пихать скрипты по неймспейсам. юнити из коробки это не делает, но горячие клавиши от студии позволяют это делать очень быстро
namespace Assets.Scripts.Player
{
    //likvidator: концепция коротко. цепляем контроллер к объекту. контроллер знает, какие сервисы должны работать с объектом, поэтому он их сам добавляет из кода, чтобы их можно было менеджить
    public class PlayerController : MonoBehaviour
    {
        private readonly List<ServiceBase> services;

        public PlayerController()
        {
            services = new List<ServiceBase>();
        }

        [UsedImplicitly] //lividator: говорит студии, что этот код будет вызываться откуда-то извне, поэтому выключает предупредждение о неиспользуемом методе
        private void Start()
        {
            DirectionHelper.Instance.Direction = Direction.Right;
            
            //likvidator: в будущем планирую втащить DI-контейнер
            AddMovementService();
            AddDirectionService();

            foreach (var service in services) 
                service.Start();
        }

        private void AddMovementService()
        {
            var movementConfig = new MovementConfig(1); //todo(likvidator): в будущем читать из файла или из графической обертки, которую мы придумаем и напишем

            var player = gameObject;

            var service = new MovementService(movementConfig, player.transform, DirectionHelper.Instance);
            services.Add(service);
        }

        private void AddDirectionService()
        {
            var player = gameObject;

            var service = new DirectionService(player.transform, DirectionHelper.Instance);
            services.Add(service);
        }

        [UsedImplicitly]
        private void Update()
        {
            foreach(var service in services)
                service.Update();
        }

        [UsedImplicitly]
        private void FixedUpdate()
        {
            foreach (var service in services)
                service.FixedUpdate();
        }
    }
}
