using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Player.SceneTransfer.Repositories;
using Assets.Scripts.Scenes.Repositories;
using Assets.Scripts.Scenes.Services;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class SceneTransferController: ControllerBase
    {
        private readonly SceneTransferModel model;

        public SceneTransferController(SceneTransferMarker marker, GameObject gameObject, IServiceProvider serviceProvider) : base(gameObject, serviceProvider)
        {
            model = SceneTransferModel.FromMarker(marker);
        }

        public override void Start()
        {
            CreateTransferToSceneService();

            foreach (var service in Services)
            {
                service.Start();
            }
        }

        public override void Update()
        {
            foreach (var service in Services)
            {
                service.Update();
            }
        }

        private void CreateTransferToSceneService()
        {
            var transferToSceneEventRepository = ServiceProvider.GetService<TransferToSceneEventRepository>();
            var teleportPlayerEventRepository = ServiceProvider.GetService<TeleportPlayerEventRepository>();

            var service =
                new TransferToSceneService(model, transferToSceneEventRepository, teleportPlayerEventRepository);

            Services.Add(service);
        }
    }
}
