using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.Events.EventArgs;
using Respawning;
using DamageHandlerBase = PlayerStatsSystem.DamageHandlerBase;
using Player = Exiled.Events.Handlers.Player;

namespace GRU_P
{
    public class EventHandlers
    {
        private Config cfg;

        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            bool scpAlive = API.CountRoles(Team.SCP) > 0;
            bool grupAlive = API.GetGRUPPlayers().Count > 0;

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
        
        public void OnAnnouncingScpTerminationEvent(AnnouncingScpTerminationEventArgs ev)
        {
            if (API.IsGRUP(ev.Killer))
            {
                ev.IsAllowed = false;
                string scpType = null;
                switch (ev.Player.Role.Type)
                {
                    case RoleType.Scp096:
                        scpType = "SCP 0 9 6";
                        break;
                    case RoleType.Scp049:
                        scpType = "SCP 0 4 9";
                        break;
                    case RoleType.Scp079:
                        scpType = "SCP 0 7 9";
                        break;
                    case RoleType.Scp173:
                        scpType = "SCP 1 7 3";
                        break;
                    case RoleType.Scp93989:
                        scpType = "SCP 9 3 9-8 9";
                        break;
                    case RoleType.Scp93953:
                        scpType = "SCP 9 3 9-5 3";
                        break;
                    case RoleType.Scp106:
                        scpType = "SCP 1 0 6";
                        break;
                }
                Cassie.Message($"{scpType} has been terminated by G R U division P");
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsGRUP(ev.Player) && ev.NewRole != RoleType.Tutorial)
                API.DestroyGRUP(ev.Player);
        }

        public void OnSpawn(RespawningTeamEventArgs ev)
        {
            if ((Respawn.NtfTickets - Respawn.ChaosTickets) < cfg.differenceTickets || (Respawn.ChaosTickets - Respawn.NtfTickets) < cfg.differenceTickets)
            {
                ev.IsAllowed = false;
                API.SpawnSquad(ev.Players.Count);
            }
        }
        
        public EventHandlers(Plugin plugin)
        {
            cfg = plugin.Config;
        }
    }
}