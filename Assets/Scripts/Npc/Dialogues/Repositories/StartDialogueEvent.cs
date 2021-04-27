using System;

namespace Assets.Scripts.Npc.Dialogues.Repositories
{
    public class StartDialogueEvent
    {
        public string NpcId { get; }

        public StartDialogueEvent(string npcId)
        {
            NpcId = npcId ?? throw new ArgumentNullException(nameof(npcId));
        }
    }
}
