using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace GRU_P.Commands.Subcmds
{
    using GRU_P.Models;

    public class Spawn : ICommand
    {
        public string Command { get; } = "spawn";
        public string[] Aliases { get; } = {"s"};
        public string Description { get; } = "Makes player a GRU-P Agent";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("grup.spawn"))
            {
                response = "You don't have permission to execute this command. Required permission: grup.spawn";
                return false;
            }

            GRUPClass? @class;
            if (arguments.Count == 0)
            {
                if ((@class = Plugin.GetClass("Commissar")) == null)
                {
                    response = "Cannot find Commissar class, please specify type!";
                    return false;
                }
                Player ply = Player.Get(sender);

                if (API.IsGRUP(ply))
                {
                    if (API.GetGRUPType(ply).ToLower() == "commissar")
                    {
                        response = $"You are already a GRU-P commissar!";
                        return false;
                    }
                    ply.SessionVariables.Remove("IsGRUP");
                }

                API.SpawnPlayer(ply, @class);
                response = "You are now a GRU-P commissar.";
                return true;
            }

            string type = arguments.At(0).ToLower();
            if (arguments.Count == 1)
            {
                Player ply = Player.Get(sender);
                if ((@class = Plugin.GetClass(type)) == null)
                {
                    response = $"Invalid argument. Please enter a valid type! Valid: {string.Join(", ", Plugin.Singleton.Config.Classes.Keys)}";
                    return false;
                }

                if (API.IsGRUP(ply))
                {
                    if (API.GetGRUPType(ply) == type)
                    {
                        response = $"You are already a GRU-P {type}!";
                        return false;
                    }
                    ply.SessionVariables.Remove("IsGRUP");
                }
                API.SpawnPlayer(ply, @class);
                response = $"You are now a GRU-P {type}.";
                return true;
            }

            Player player = Player.Get(arguments.At(1));
            if (player == null)
            {
                response = "Invalid argument. Please give player's id or nickname!";
                return false;
            }

            if ((@class = Plugin.GetClass(type)) == null)
            {
                response = "Invalid argument. Please enter a valid type!";
                return false;
            }

            if (API.IsGRUP(player))
            {
                if (API.GetGRUPType(player) == type)
                {
                    response = $"({player.Id}) {player.Nickname} is already a GRU-P {type}!";
                    return false;
                }
                player.SessionVariables.Remove("IsGRUP");
            }

            API.SpawnPlayer(player, @class);
            response = $"({player.Id}) {player.Nickname} is now a GRU-P {type}.";
            return true;
            
        }
    }
}