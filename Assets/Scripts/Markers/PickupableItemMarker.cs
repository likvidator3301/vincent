using Assets.Scripts.Common.Extensions;
using Assets.Scripts.Inventory;
using Assets.Scripts.Npc.Dialogues.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Markers
{
    public class PickupableItemMarker: MonoBehaviour
    {
        //likvidator: пока так, пока не придумаем обертку над конфигурацией
        //likvidator: хотя я думаю, что мы по такому пути и пойдем и не будет что-то свое писать
        public string Id { get; } = Guid.NewGuid().ToString();
        public Sprite InventoryImage;
        public string Name;

        public TextAsset DialogueFile;

        public Dialogue GetDialogue(PlayerInventory playerInventory)
        {
            return Dialogue.GetDialogue(playerInventory, DialogueFile);
        }
    }
}