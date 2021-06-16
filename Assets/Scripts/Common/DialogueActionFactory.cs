using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;

namespace Assets.Scripts.Common
{
    public sealed class DialogueActionFactory
    {
        private readonly PlayerInventory playerInventory;
        private readonly Dictionary<string, InventoryItem> inventoryItemsByName;

        public DialogueActionFactory(PlayerInventory playerInventory, InventoryItem[] inventoryItems)
        {
            this.playerInventory = playerInventory;
            inventoryItemsByName = inventoryItems.ToDictionary(x => x.Name);
        }

        public Action CreateAction(string commandString)
        {
            if (commandString == "")
                return () => { };
            var commandStrings = commandString.Split(';');
            var commands = new List<Action>();
            foreach (var command in commandStrings.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                commands.Add(CreateSingleAction(command));
            }

            return () =>
            {
                foreach (var command in commands)
                {
                    command();
                }
            };
        }

        private Action CreateSingleAction(string fullCommandString)
        {
            var commandString = fullCommandString.Substring(0,
                fullCommandString.IndexOf("(", StringComparison.InvariantCultureIgnoreCase));
            var subject = commandString.Split('.')[0];
            var action = commandString.Split('.')[1];
            var argumentsString = fullCommandString.Substring(
                fullCommandString.IndexOf("(", StringComparison.InvariantCultureIgnoreCase) + 1,
                fullCommandString.Length - fullCommandString.IndexOf("(", StringComparison.InvariantCultureIgnoreCase) -
                2);
            var arguments = argumentsString.Split();

            switch (subject)
            {
                case "Inventory":
                    switch (action)
                    {
                        case "Add":
                            if (arguments.Length != 1)
                                throw new GameInitializationException(
                                    $"Error occured while parsing command: '{fullCommandString}'. Expected one argument, but got {arguments.Length}");
                            var inventoryItem = inventoryItemsByName[arguments[0]];
                            return () =>
                            {
                                playerInventory.Add(new PlayerInventoryItem(inventoryItem.Id, inventoryItem.Name,
                                    inventoryItem.Sprite));
                            };
                        case "Remove":
                            if (arguments.Length != 1)
                                throw new GameInitializationException(
                                    $"Error occured while parsing command: '{fullCommandString}'. Expected one argument, but got {arguments.Length}");
                            return () =>
                            {
                                playerInventory.RemoveByName(arguments[0]);
                            };
                    }
                    break;
            }

            throw new Exception($"Cannot parse command {fullCommandString}");
        }
    }

    public sealed class DialogueConditionFactory
    {
        public Func<bool> CreateCondition(string conditionString)
        {
            return () => true;
        }
    }
}
