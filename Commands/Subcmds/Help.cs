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
            foreach (var msg in Plugin.Singleton.Config.HelpCommandMessages)
            {
                ply.Broadcast(8, msg.Replace("%ticketdiff%", Plugin.Singleton.Config.DifferenceInfluence.ToString()));
            }
            response = "Displayed help message";
            return true;
        }
    }
}