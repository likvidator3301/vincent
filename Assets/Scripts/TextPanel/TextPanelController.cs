using System;
using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.TextPanel.Repositories;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TextPanel
{
    public class TextPanelController : ControllerBase
    {
        private readonly ScrollRect scrollRect;
        private readonly ButtonMarker button;

        public TextPanelController(GameObject gameObject, IServiceProvider serviceProvider, ScrollRect scrollRect, ButtonMarker button) : base(gameObject, serviceProvider)
        {
            this.scrollRect = scrollRect;
            this.button = button;
        }

        public override void Start()
        {
            AddDisplayTextService();

            foreach (var service in Services)
                service.Start();
        }

        private void AddDisplayTextService()
        {
            var newTextEventRepository = ServiceProvider.GetService<NewTextEventRepository>();
            var text = GameObject.GetComponent<Text>();
            var service = new DisplayTextService(text, newTextEventRepository, scrollRect, button);

            Services.Add(service);
        }

        public override void Update()
        {
            foreach (var service in Services)
                service.Update();
        }
    }
}
