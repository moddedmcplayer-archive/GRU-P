using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Features;

namespace GRU_P.Commands.Subcmds
{
    public class Spawn : ICommand
    {
        public string Command { get; } = "spawn";
        public string[] Aliases { get; } = Array.Empty<String>();
        public string Description { get; } = "Makes player a GRU-P Agent";
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 0)
            {
                Player ply = Player.Get(sender);

                if (API.IsGRUP(ply))
                {
                    response = "You are already a GRU-P agent!";
                    return false;
                }

                API.SpawnPlayer(ply);
                response = "You are now a GRU-P agent.";
                return true;
            }

            Player player = Player.Get(arguments.At(0));
            if (player == null)
            {
                response = "Invalid argument. Please give player's id or nickname!";
                return false;
            }

            if (API.IsGRUP(player))
            {
                response = $"({player.Id}) {player.Nickname} is already a GRU-P agent!";
                return false;
            }

            API.SpawnPlayer(player);
            response = $"({player.Id}) {player.Nickname} is now a GRU-P agent.";
            return true;
        }
    }
}