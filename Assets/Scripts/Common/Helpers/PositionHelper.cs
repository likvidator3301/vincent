using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Common.Helpers
{
    public static class PositionHelper
    {
        public static float GetDistance(Transform t1, Transform t2) => GetDistance(t1.position, t2.position);

        public static float GetDistance(Vector3 v1, Vector3 v2) => Math.Abs(v1.x - v2.x);
    }
}
