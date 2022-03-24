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
                API.DestroySH(ev.Target);
                return;
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsGRUP(ev.Player) && ev.NewRole != RoleType.Tutorial)
                API.DestroySH(ev.Player);
        }
        
        public EventHandlers(Plugin plugin)
        {
            cfg = plugin.Config;
        }
    }
}