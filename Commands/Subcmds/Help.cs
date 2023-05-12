using System;
using CommandSystem;
using Exiled.API.Features;

namespace GRU_P.Commands.Subcmds
{
    public class Help : ICommand
    {
        public string Command { get; } = "help";
        public string[] Aliases { get; } = { "h" };
        public string Description { get; } = "Displays a help message";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);
            ply.Broadcast(8, "The GRU-P are only hostile to SCPs, but neutral to everyone else.");
            ply.Broadcast(8, "The hierarchy is as follows: Commissar > Trooper > Agent.");
            ply.Broadcast(8, $"They spawn when the spawn ticket difference between MTF and CI is less than {Plugin.Singleton.Config.differenceTickets}.");
            response = "Displayed help message";
            return true;
        }
    }
}