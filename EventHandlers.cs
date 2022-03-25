using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Player = Exiled.Events.Handlers.Player;

namespace GRU_P
{
    public class EventHandlers
    {
        private Config cfg;

        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            bool scpAlive = API.CountRoles(Team.SCP) > 0;
            bool grupAlive = API.GetSHPlayers().Count > 0;

            if (grupAlive && scpAlive)
            {
                ev.IsAllowed = false;
            }
        }
        
        public void OnDied(DiedEventArgs ev)
        {
            if (API.IsGRUP(ev.Target))
            {
                API.DestroyGRUP(ev.Target);
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (API.IsGRUP(ev.Killer) && ev.Target.Role.Team == Team.SCP)
            {
                ev.IsAllowed = false;
                ev.Target.Kill(ev.Handler.Type, $"{ev.Target.Role.Type} was killed by GRUP");
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsGRUP(ev.Player) && ev.NewRole != RoleType.Tutorial)
                API.DestroyGRUP(ev.Player);
        }
        
        public EventHandlers(Plugin plugin)
        {
            cfg = plugin.Config;
        }
    }
}