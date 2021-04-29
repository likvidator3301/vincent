using System;
using Assets.Scripts.Markers;

namespace Assets.Scripts.Player.NpcInteraction.Repositories
{
    public class InteractWithNpcEvent
    {
        public NpcMarker Marker { get; }

        public InteractWithNpcEvent(NpcMarker marker)
        {
            Marker = marker ?? throw new ArgumentNullException(nameof(marker));
        }
    }
}
