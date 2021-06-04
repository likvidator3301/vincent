using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Scenes.Repositories
{
    public class TransferToSceneEvent
    {
        public string MarkerId { get; }

        public TransferToSceneEvent(string markerId)
        {
            MarkerId = markerId;
        }
    }
}
