using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Movement.Helpers
{
    public class MovementEvent
    {
        public MovementEvent(Vector3 destination)
        {
            Destination = destination;
        }

        public Vector3 Destination { get; }
    }
}
