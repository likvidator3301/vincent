using System;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class DisplayInventoryService: ServiceBase
    {
        private readonly PlayerInventory inventory;
        private readonly Text inventoryText;

        public DisplayInventoryService(PlayerInventory inventory, Text inventoryText)
        {
            this.inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            this.inventoryText = inventoryText;
        }

        public override void Update()
        {
            var items = inventory.GetAll().Select(x => x.Name);
            var text = string.Join("\n", items);
            inventoryText.text = text;
        }
    }
}
