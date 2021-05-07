using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Npc.Dialogues.Models;
using Assets.Scripts.TextPanel.Repositories;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TextPanel
{
    public class CancelButtonService : ServiceBase
    {
        private readonly NewTextEventRepository newTextEventRepository;
        private readonly ScrollRect scrollRect;
        private readonly ButtonMarker button;

        public CancelButtonService(NewTextEventRepository newTextEventRepository, ScrollRect scrollRect, ButtonMarker button)
        {
            this.scrollRect = scrollRect ?? throw new ArgumentNullException(nameof(scrollRect));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
            this.button = button ?? throw new ArgumentNullException(nameof(button));
        }

        public override void Update()
        {
            if (!newTextEventRepository.HasValue)
                return;
            var currentNode = newTextEventRepository.Value.node;

            if (currentNode.Text == "end")
            {
                button.gameObject.SetActive(false);
                scrollRect.gameObject.SetActive(false);
                newTextEventRepository.RemoveValue();
            }
        }
    }
}
