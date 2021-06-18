using Spine.Unity;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class PlayerMarker: MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset idle;
        public AnimationReferenceAsset walking;
    }
}