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
    public class NpcImageService : ServiceBase
    {
        private readonly Image npcImage;
        private readonly NewTextEventRepository newTextEventRepository;

        public NpcImageService(NewTextEventRepository newTextEventRepository, Image npcImage)
        {
            this.npcImage = npcImage ?? throw new ArgumentNullException(nameof(npcImage));
            this.newTextEventRepository = newTextEventRepository ?? throw new ArgumentNullException(nameof(newTextEventRepository));
        }

        public override void Update()
        {
            if (!newTextEventRepository.HasValue)
                return;
            npcImage.sprite = newTextEventRepository.Value.npcSprite;
        }
    }
}
