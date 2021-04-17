using UnityEngine;

namespace Assets.Scripts.Player.Movement.Helpers
{
    public class MovementHelper
    {
        public MovementEvent MovementEvent { get; private set; }

        public bool IsInMovement { get; private set; }

        public void SetEvent(MovementEvent movementEvent)
        {
            MovementEvent = movementEvent;
            IsInMovement = true;
        }

        public void Stop()
        {
            IsInMovement = false;
            MovementEvent = null;
        }
    }
}
