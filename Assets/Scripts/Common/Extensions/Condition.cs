using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Common.Extensions
{
    public class Condition
    {
        public static bool CheckCondition(string condition, PlayerInventory items)
        {
            var conditionText = condition.Split('_');

            if (conditionText.Length != 3)
                throw new ArgumentException("number of condition arguments must be 3");

            if(conditionText[0].Equals("inventory", StringComparison.InvariantCultureIgnoreCase) && conditionText[1].Equals("has", StringComparison.InvariantCultureIgnoreCase))
            {
                var item = conditionText[2];
                return items.GetAll().Any(x => x.Name == item);
            }

            return false;
        }
    }
}
