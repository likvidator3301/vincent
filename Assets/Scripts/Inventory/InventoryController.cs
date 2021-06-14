using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Common;
using Assets.Scripts.Markers;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class InventoryController: ControllerBase
    {
        public InventoryController(GameObject gameObject, IServiceProvider serviceProvider) : base(gameObject, serviceProvider)
        {

        }

        public override void Start()
        {
            AddDisplayInventoryService();

            foreach (var service in Services) 
                service.Start();
        }

        private void AddDisplayInventoryService()
        {
            var inventory = ServiceProvider.GetService<PlayerInventory>();
            var panel = GameObject.GetComponent<InventoryMarker>();

            var service = new DisplayInventoryService(inventory, panel);

            Services.Add(service);
        }

        public override void Update()
        {
            foreach (var service in Services)
                service.Update();
        }
    }
}
