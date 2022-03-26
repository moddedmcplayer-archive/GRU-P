using System;
using CommandSystem;

namespace GRU_P.Commands.Subcmds
{
    public class SpawnTeam : ICommand
    {
        public string Command { get; } = "spawnTeam";
        public string[] Aliases { get; } = {"sT"};
        public string Description { get; } = "Spawns a GRU-P Squad";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "b";
            return false;
        }
    }
}