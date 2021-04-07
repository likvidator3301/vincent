using System;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Configs
{
    public class MovementConfig
    {
        public MovementConfig(int speed)
        {
            if (speed <= 0)
                throw new ArgumentException("Speed cannot be less or equal than zero");

            Speed = speed;
        }

        public int Speed { get; }
    }
}
