using UnityEngine;

namespace Assets.Scripts.Common.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 WithDeltaTime(this Vector3 vector)
        {
            return vector * Time.deltaTime;
        }
    }
}
