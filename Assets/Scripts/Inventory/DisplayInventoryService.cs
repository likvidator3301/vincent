using System;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class DisplayInventoryService: ServiceBase
    {
        private readonly PlayerInventory inventory;
        private readonly InventoryMarker panel;
        private readonly Sprite transparent;

        public DisplayInventoryService(PlayerInventory inventory, InventoryMarker panel)
        {
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            this.panel = panel ?? throw new ArgumentNullException(nameof(panel));

            var texture = Resources.Load<Texture2D>("transparent");
            transparent = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
        }

        public override void Update()
        {
            var items = inventory.GetAll().Select(x => x.InventoryImage).ToArray();

            for(var i = 0; i < panel.transform.childCount; i++)
                panel.transform.GetChild(i).GetComponent<Image>().sprite = i < items.Length ? items[i] : transparent;
        }
    }
}
