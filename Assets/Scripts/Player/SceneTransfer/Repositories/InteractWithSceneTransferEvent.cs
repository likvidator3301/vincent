using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Markers;

namespace Assets.Scripts.Player.SceneTransfer.Repositories
{
    public class InteractWithSceneTransferEvent
    {
        public SceneTransferMarker Marker { get; }

        public InteractWithSceneTransferEvent(SceneTransferMarker marker)
        {
            Marker = marker ?? throw new ArgumentNullException(nameof(marker));
        }
    }
}
