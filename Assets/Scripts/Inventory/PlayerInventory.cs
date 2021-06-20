﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class PlayerInventory
    {
        private readonly Dictionary<string, PlayerInventoryItem> items;

        public PlayerInventory()
        {
            items = new Dictionary<string, PlayerInventoryItem>();
        }

        public void Add(PlayerInventoryItem item)
        {
            Debug.Log($"Adding {item.Name}");
            items[item.Id] = item;
        }

        public IReadOnlyList<PlayerInventoryItem> GetAll()
        {
            return items.Values.ToList().AsReadOnly();
        }

        public PlayerInventoryItem GetById(string id)
        {
            return items[id];
        }

        public bool HasItemWithId(string id)
        {
            return items.ContainsKey(id);
        }

        public bool HasItem(Func<PlayerInventoryItem, bool> checkFunc)
        {
            var similarItems = items.Values.Where(checkFunc).ToArray();
            return similarItems.Length > 0;
        }

        public void RemoveWithId(string id)
        {
            items.Remove(id);
        }

        public void Remove(PlayerInventoryItem item)
        {
            items.Remove(item.Id);
        }

        public void RemoveByName(string name)
        {
            var targetItem = items.First(x => x.Value.Name == name).Value;
            Remove(targetItem);
        }
    }
}
