using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Markers;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Movement.Configs;
using Assets.Scripts.Player.Movement.Helpers;
using Assets.Scripts.Player.Movement.Services;
using JetBrains.Annotations;
using UnityEngine;
using Microsoft.Extensions.DependencyInjection;

namespace Assets.Scripts.Common
{
    public class GameController: MonoBehaviour
    {
        private IServiceProvider serviceProvider;
        private List<ControllerBase> controllers;

        [UsedImplicitly]
        public void Start()
        {
            controllers = new List<ControllerBase>();

            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            
            CreateControllers();
            StartControllers();
        }

        private void StartControllers()
        {
            foreach (var controller in controllers) 
                controller.Start();
        }

        //likvidator: здесь регистрируем всё, что лежит в слое репозиториев
        private void ConfigureServices(IServiceCollection services)
        {
            //todo(likvidator): читать из конфигурации
            services.AddSingleton(sp => new MovementConfig(1, .5f));
            services.AddSingleton<DirectionHelper>();
            services.AddSingleton<MovementHelper>();
        }

        private void CreateControllers()
        {
            CreatePlayerController();
        }

        private void CreatePlayerController()
        {
            var players = FindObjectsOfType(typeof(PlayerMarker)) as PlayerMarker[];

            if (players == null || players.Length == 0)
                throw new GameInitializationException("Player not found");

            if (players.Length > 1)
                throw new GameInitializationException($"Found '{players.Length}' players. Expected one player");

            var player = players.First().gameObject;
            var playerController = new PlayerController(player, serviceProvider);
            controllers.Add(playerController);
        }

        [UsedImplicitly]
        private void Update()
        {
            foreach (var controller in controllers)
                controller.Update();
        }

        [UsedImplicitly]
        private void FixedUpdate()
        {
            foreach (var controller in controllers)
                controller.Update();
        }
    }
}
