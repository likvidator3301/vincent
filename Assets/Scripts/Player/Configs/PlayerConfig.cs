using System;

namespace Assets.Scripts.Player.Configs
{
    public class PlayerConfig
    {
        public PlayerConfig(int speed, float movementCriticalDistance, float interactCriticalDistance)
        {
            if (speed <= 0)
                throw new ArgumentException("Speed cannot be less or equal than zero");
            if (movementCriticalDistance <= 0)
                throw new ArgumentException("CriticalDistance cannot be less or equal than zero");
            if (interactCriticalDistance <= 0)
                throw new ArgumentException("InteractCriticalDistance cannot be less or equal than zero");
            Speed = speed;
            MovementCriticalDistance = movementCriticalDistance;
            InteractCriticalDistance = interactCriticalDistance;
        }

        public int Speed { get; }

        public float MovementCriticalDistance { get; }

        public float InteractCriticalDistance { get; }
    }
}
