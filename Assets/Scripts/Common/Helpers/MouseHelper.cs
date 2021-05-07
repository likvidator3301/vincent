using UnityEngine;

namespace Assets.Scripts.Common.Helpers
{
    public static class MouseHelper
    {
        public static bool IsMouseAboveObjectWithTag(string tag)
        {
            var hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            
            if (hitInfo.collider != null)
            {
                var clickedOnTag = hitInfo.collider.gameObject.tag;
                if (clickedOnTag == tag)
                    return true;
            }

            return false;
        }

        public static T GetComponentOnGameObjectUnderMouse<T>() where T : MonoBehaviour
        {
            var hitInfo = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (hitInfo.collider != null)
                return hitInfo.collider.gameObject.GetComponent<T>();

            return null;
        }

        public static Vector3 GetPositionUnderMouse() => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
