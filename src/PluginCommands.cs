﻿using AetherCompass.UI;
using Dalamud.Game.Command;

namespace AetherCompass
{
    public static class PluginCommands
    {
        public const string MainCommand = "/aethercompass";

        public static void AddCommands(Plugin host)
        {
            Plugin.CommandManager.AddHandler(
                MainCommand, new CommandInfo((cmd, args) => ProcessMainCommand(host, cmd, args))
                {
                    HelpMessage = "Toggle the plugin between enabled/disabled when no options provided\n" +
                    "\tOptions:\n" +
                    $"\t\ton: Enable the plugin\n" +
                    $"\t\toff: Disable the plugin\n" +
                    $"\t\tmark: Toggle enabled/disabled for marking detected objects on screen\n" +
                    $"\t\tdetail: Toggle enabled/disabled for showing Object Detail Window\n" +
                    $"\t\tconfig: Open the Configuration window",
                    ShowInHelp = true
                });

            // TODO: commands
        }

        public static void RemoveCommands()
        {
            Plugin.CommandManager.RemoveHandler(MainCommand);
        }

        private static void ProcessMainCommand(Plugin host, string command, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                host.Enabled = !host.Enabled;
                return;
            }
            var argList = args.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
            if (argList.Length == 0)
            {
                host.Enabled = !host.Enabled;
                return;
            }
            switch (argList[0])
            {
                case "on":
                    host.Enabled = true;
                    return;
                case "off":
                    host.Enabled = false;
                    return;
                case "mark":
                    host.Config.ShowScreenMark = !host.Config.ShowScreenMark;
                    return;
                case "detail":
                    host.Config.ShowDetailWindow = !host.Config.ShowDetailWindow;
                    return;
                case "config":
                    host.InConfig = true;
                    return;
                default:
                    Chat.PrintErrorChat($"Unknown command args: {args}");
                    return;
            }

        }

    }
}
