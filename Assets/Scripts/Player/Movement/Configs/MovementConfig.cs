using System;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Configs
{
    public class MovementConfig
    {
        public MovementConfig(int speed, float criticalDistance)
        {
            if (speed <= 0)
                throw new ArgumentException("Speed cannot be less or equal than zero");
            if (criticalDistance <= 0)
                throw new ArgumentException("CriticalDistance cannot be less or equal than zero");
            Speed = speed;
            CriticalDistance = criticalDistance;
        }

        public int Speed { get; }

        public float CriticalDistance;
    }
}
