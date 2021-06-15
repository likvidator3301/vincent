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

        public DisplayInventoryService(PlayerInventory inventory, InventoryMarker panel)
        {
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            this.panel = panel ?? throw new ArgumentNullException(nameof(panel));
        }

        public override void Update()
        {
            var items = inventory.GetAll().Select(x => x.InventoryImage).ToArray();
            for(int i = 0; i < Math.Min(panel.transform.childCount, items.Length); i++)
            {
                panel.transform.GetChild(i).GetComponent<Image>().sprite = items[i];
            }
        }
    }
}
