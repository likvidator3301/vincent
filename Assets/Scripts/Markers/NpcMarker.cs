using System;
using System.IO;
using System.Linq;
using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Npc.Dialogues.Models;
using Newtonsoft.Json;
using Spine.Unity;
using UnityEngine;

namespace Assets.Scripts.Markers
{
    public class NpcMarker: MonoBehaviour
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name;
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset idle;
        public Sprite IconForDialogue;
        public TextAsset DialogueFile;
    }
}
