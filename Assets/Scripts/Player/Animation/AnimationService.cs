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

namespace Assets.Scripts.Player.Animation
{
    public class AnimationService : ServiceBase
    {
        private readonly MovementEventRepository movementEventRepository;
        private SkeletonAnimation skeletonAnimation;
        private AnimationReferenceAsset idle;
        private AnimationReferenceAsset walking;
        private PlayerMarker player;
        private string state;
        private string currentAnimation;

        public AnimationService(MovementEventRepository movementEventRepository, 
            SkeletonAnimation skeletonAnimation, PlayerMarker player)
        {
            this.movementEventRepository = movementEventRepository ?? throw new ArgumentNullException(nameof(movementEventRepository));
            this.skeletonAnimation = skeletonAnimation ?? throw new ArgumentNullException(nameof(skeletonAnimation));
            this.idle = player.idle ?? throw new ArgumentNullException(nameof(idle));
            this.walking = player.walking ?? throw new ArgumentNullException(nameof(walking));
            this.player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public override void Start()
        {
            state = "Idle";
            SetState(state);
        }

        public override void Update()
        {
            if (!movementEventRepository.HasValue)
                SetState("Idle");
            else
                SetState("Walking");
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
                case "Walking":
                    {
                        SetAimation(walking, true, 1f);
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