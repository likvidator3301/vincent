using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Assets.Scripts.Player.Movement.Helpers;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Npc.Animation
{
    public class AnimationService : ServiceBase
    {
        private SkeletonAnimation skeletonAnimation;
        private AnimationReferenceAsset idle;
        private NpcMarker npc;
        private string state;
        private string currentAnimation;

        public AnimationService(SkeletonAnimation skeletonAnimation, NpcMarker npc)
        {
            this.skeletonAnimation = skeletonAnimation ?? throw new ArgumentNullException(nameof(skeletonAnimation));
            this.idle = npc.idle ?? throw new ArgumentNullException(nameof(idle));
            this.npc = npc ?? throw new ArgumentNullException(nameof(npc));
        }

        public override void Start()
        {
            state = "Idle";
            SetState(state);
        }

        public override void Update()
        {
            SetState("Idle");
        }

        private void SetState(string currentState)
        {
            switch(currentState)
            {
                case "Idle": 
                    { 
                        SetAimation(idle, true, 1f); 
                        break; 
                    }
            }
        }

        private void SetAimation(AnimationReferenceAsset animation, bool loop, float timeScale)
        {
            if (animation.name.Equals(currentAnimation))
                return;
            skeletonAnimation.state.SetAnimation(0, animation, loop);
            currentAnimation = animation.name;
        }
    }
}