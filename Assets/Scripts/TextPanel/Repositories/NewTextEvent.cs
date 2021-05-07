using Assets.Scripts.Npc.Dialogues.Models;
using System;
using UnityEngine;

namespace Assets.Scripts.TextPanel.Repositories
{
    public class NewTextEvent
    {
        public DialogueNode node;
        public Sprite npcSprite;

        public NewTextEvent (DialogueNode node, Sprite npcSprite)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
            this.npcSprite = npcSprite ?? throw new ArgumentNullException(nameof(npcSprite));
        }

        public NewTextEvent(DialogueNode node)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }
    }
}
