using System;

namespace Assets.Scripts.Npc.Dialogues.Repositories
{
    public class StartDialogueEvent
    {
        public string NpcId { get; }
        public string Name { get; set; }

        public StartDialogueEvent(string npcId, string name)
        {
            NpcId = npcId ?? throw new ArgumentNullException(nameof(npcId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
