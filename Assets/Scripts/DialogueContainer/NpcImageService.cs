using System;
using Assets.Scripts.Common;
using Assets.Scripts.DialogueContainer.Models;
using Assets.Scripts.DialogueContainer.Repositories;
using UnityEngine.UI;

namespace Assets.Scripts.DialogueContainer
{
    public class NpcImageService : ServiceBase
    {
        private readonly IconForDialogueRepository iconForDialogueRepository;
        private readonly Icon icon;

        public NpcImageService(IconForDialogueRepository iconForDialogueRepository, Icon icon)
        {
            this.icon = icon ?? throw new ArgumentNullException(nameof(icon));
            this.iconForDialogueRepository = iconForDialogueRepository ??
                                             throw new ArgumentNullException(nameof(iconForDialogueRepository));
        }

        public override void Update()
        {
            icon.Image.sprite = !iconForDialogueRepository.HasValue ? null : iconForDialogueRepository.Value;
        }
    }
}