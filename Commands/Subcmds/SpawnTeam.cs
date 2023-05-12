using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace GRU_P.Commands.Subcmds
{
    using PlayerRoles;

    public class SpawnTeam : ICommand
    {
        public string Command { get; } = "spawnTeam";
        public string[] Aliases { get; } = {"sT"};
        public string Description { get; } = "Spawns a GRU-P Squad";

        private Config cfg = Plugin.Singleton.Config;
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("grup.spawnteam"))
            {
                response = "You don't have permission to execute this command. Required permission: grup.spawnteam";
                return false;
            }
            
            int spectators = Player.List.Where(x => x.Role.Team == Team.Dead && !x.IsOverwatchEnabled).ToList()
                .Count;
            if (arguments.Count == 0)
            {
                if (spectators == 0)
                {
                    response = "No spawnable spectators found!";
                    return false;
                }

                if (spectators > cfg.maxSquadSize)
                {
                    API.SpawnSquad(cfg.maxSquadSize);
                    response = "Spawned a GRU-P squad!";
                }
                API.SpawnSquad(spectators);
                response = "Spawned a GRU-P squad!";
                return true;
            }

            int size = int.Parse(arguments.At(0));
            if (size > spectators)
            {
                API.SpawnSquad(spectators);
                response = $"Spawned a GRU-P squad with {spectators} players";
                return true;
            }
            API.SpawnSquad(size);
            response = $"Spawned a GRU-P squad with {size} players";
            return true;
        }
    }
}