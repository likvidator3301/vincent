using Assets.Scripts.Npc.Dialogues.Models;

namespace Assets.Scripts.TextPanel.Repositories
{
    public class NewTextEvent
    {
        public DialogueNode node;

        public NewTextEvent (DialogueNode node)
        {
            this.node = node;
        }
    }
}
