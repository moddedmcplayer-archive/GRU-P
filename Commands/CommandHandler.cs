using System;
using CommandSystem;
using Exiled.API.Features;
using GRU_P.Commands.Subcmds;

namespace GRU_P.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CommandHandler : ParentCommand
    {
        public CommandHandler() => LoadGeneratedCommands();
        
        public override string Command => "grup";
        public override string[] Aliases => Array.Empty<string>();
        public override string Description => "Parent command for GRU-P";
        
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Spawn());
            RegisterCommand(new SpawnTeam());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            response = "\nPlease enter a valid subcommand:\n";
            foreach (var command in AllCommands)
                response += $"- {command.Command} ({string.Join(", ", Aliases)})";
            return false;
        }
    }
}