using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Exceptions;
using Assets.Scripts.Inventory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Common
{
    public static class Crutches
    {
        public static class Inventory
        {
            private const string BasePath = "DialogueActions\\InventoryItemsSprites\\";

            private static PlayerInventoryItem FromPath(string path)
            {
                var texture = Resources.Load<Texture2D>(BasePath + path);
                return new PlayerInventoryItem(Guid.NewGuid().ToString(), path,
                    Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero));
            }

            public static Dictionary<string, PlayerInventoryItem> Items = new Dictionary<string, PlayerInventoryItem>
            {
                {"Hat", FromPath("Hat")},
                {"Umbrella", FromPath("Umbrella")},
                {"EmptyJam", FromPath("EmptyJam")}
            };
        }

        public static class World
        {
            private const string BasePath = "DialogueActions\\World\\";

            public static class Add
            {
                public static void SceneTransfer(string name)
                {
                    var transfer = GameObject.Find(name);
                    transfer.GetComponent<SpriteRenderer>().enabled = true;
                    transfer.GetComponent<BoxCollider2D>().enabled = true;
                }

                public static void RecorderForFim()
                {
                    var recordPlayer = GameObject.Find("FimRecorder");
                    recordPlayer.GetComponent<MeshRenderer>().enabled = true;
                }
            }

            public static class Change
            {
                public static void Nest()
                {
                    var nestGO = GameObject.Find("nest_broken");
                    var fixedTexture = Resources.Load<Texture2D>(BasePath + "NestFixed");
                    nestGO.GetComponent<SpriteRenderer>().sprite = Sprite.Create(fixedTexture, new Rect(0f, 0f, fixedTexture.width, fixedTexture.height), new Vector2(0.5f, 0.5f));
                }

                public static void RecordPlayer()
                {
                    var recorderGO = GameObject.Find("RecordPlayer");
                    recorderGO.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            public static class Remove
            {
                public static void Rupert()
                {
                    var rupert = GameObject.Find("Rupert");
                    rupert.GetComponent<MeshRenderer>().enabled = false;
                    rupert.GetComponent<BoxCollider2D>().enabled = false;
                }

                public static void Hat()
                {
                    var hat = GameObject.Find("Hat");
                    hat.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    public sealed class CommandModel
    {
        public string Subject { get; }

        public string Action { get; }

        public string[] Arguments { get; }

        private CommandModel(string subject, string action, string[] arguments)
        {
            Subject = subject;
            Action = action;
            Arguments = arguments;
        }

        public static CommandModel Parse(string fullCommandString)
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
            return new CommandModel(subject, action, arguments);
        }
    }

    public sealed class DialogueActionFactory
    {
        private readonly PlayerInventory playerInventory;

        public DialogueActionFactory(PlayerInventory playerInventory)
        {
            this.playerInventory = playerInventory;
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
            var command = CommandModel.Parse(fullCommandString);

            switch (command.Subject)
            {
                case "Inventory":
                    switch (command.Action)
                    {
                        case "Add":
                            EnsureCountOfArguments(command, 1);
                            return () =>
                            {
                                Debug.Log($"Adding {command.Arguments[0]}");
                                playerInventory.Add(Crutches.Inventory.Items[command.Arguments[0]]);
                            };
                        case "Remove":
                            EnsureCountOfArguments(command, 1);
                            return () =>
                            {
                                playerInventory.RemoveByName(command.Arguments[0]);
                            };
                    }
                    break;
                case "World":
                    switch (command.Action)
                    {
                        case "Add":
                            EnsureCountOfArguments(command, 1);
                            switch (command.Arguments[0])
                            {
                                case "SceneTransferTwinkieHouse":
                                    return () => Crutches.World.Add.SceneTransfer("SceneTransferToHouse");
                                case "SceneTransferShoreToPond":
                                    return () => Crutches.World.Add.SceneTransfer("SceneTransferShoreToPond");
                                case "RecorderForFim":
                                    return Crutches.World.Add.RecorderForFim;
                            }
                            break;
                        case "Change":
                            EnsureCountOfArguments(command, 1);
                            switch (command.Arguments[0])
                            {
                                case "Nest":
                                    return Crutches.World.Change.Nest;
                                case "RecordPlayer":
                                    return Crutches.World.Change.RecordPlayer;
                            }
                            break;
                        case "Remove":
                            EnsureCountOfArguments(command, 1);
                            switch (command.Arguments[0])
                            {
                                case "Rupert":
                                    return Crutches.World.Remove.Rupert;
                                case "Hat":
                                    return Crutches.World.Remove.Hat;
                            }
                            break;
                    }
                    break;
            }

            throw new GameInitializationException($"Cannot parse command {fullCommandString}");
        }

        private void EnsureCountOfArguments(CommandModel command, int targetCount)
        {
            if (command.Arguments.Length != targetCount)
                throw new GameInitializationException(
                    $"Error occured while parsing command: '{command.Subject}.{command.Action}({string.Join(",", command.Arguments)})'." +
                    $" Expected {targetCount} argument, but got {command.Arguments.Length}");
        }
    }

    public sealed class DialogueConditionFactory
    {

        private PlayerInventory playerInventory;

        public DialogueConditionFactory(PlayerInventory playerInventory)
        {
            this.playerInventory = playerInventory;
        }

        public Func<bool> CreateCondition(string conditionString)
        {
            if (conditionString == "")
                return () => true;
            var commandStrings = conditionString.Split('&');
            var commands = new List<Func<bool>>();
            foreach (var command in commandStrings.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                commands.Add(CreateSingleCondition(command));
            }

            return () =>
            {
                return commands.All(x => x());
            };
        }

        private Func<bool> CreateSingleCondition(string conditionString)
        {
            var command = CommandModel.Parse(conditionString);

            if (command.Subject != "Inventory")
                throw new GameInitializationException($"Now we only support conditions for inventory. Command: {conditionString}");

            if (command.Action != "Has")
                throw new GameInitializationException("Now we only support 'has' conditions");

            EnsureCountOfArguments(command, 1);
            return () => playerInventory.HasItem(x => x.Name == command.Arguments[0]);
        }

        private void EnsureCountOfArguments(CommandModel command, int targetCount)
        {
            if (command.Arguments.Length != targetCount)
                throw new GameInitializationException(
                    $"Error occured while parsing command: '{command.Subject}.{command.Action}({string.Join(",", command.Arguments)})'." +
                    $" Expected {targetCount} argument, but got {command.Arguments.Length}");
        }
    }
}
