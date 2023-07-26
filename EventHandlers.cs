using Exiled.API.Features;

namespace GRU_P
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Features.Items;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Server;
    using MEC;
    using PlayerRoles;
    using Respawning;

    public class EventHandlers
    {
        private Config cfg;
        private bool SHSpawned = false;

        
        public void OnRoundStarted()
        {
            SHSpawned = false;
            API.Escaped = 0;
        }

        public void OnEndingRound(EndingRoundEventArgs ev)
        {
            bool scpAlive = API.CountRoles(Team.SCPs) > 0;
            bool grupAlive = API.GetGRUPPlayers().Count > 0;

            if (grupAlive && scpAlive)
            {
                ev.IsRoundEnded = false;
            }
            else if (grupAlive && API.Escaped > 0)
            {
                Map.ShowHint("GRU-P also won!", 7);
            }
        }
        
        public void OnDied(DiedEventArgs ev)
        {
            if (API.IsGRUP(ev.Player))
            {
                API.DestroyGRUP(ev.Player);
            }
        }
        
        public void OnAnnouncingScpTerminationEvent(AnnouncingScpTerminationEventArgs ev)
        {
            if (API.IsGRUP(ev.Attacker) && ev.Player.Role.Team == Team.SCPs)
            {
                ev.IsAllowed = false;
                string scpType = null;
                switch (ev.Player.Role.Type)
                {
                    case RoleTypeId.Scp096:
                        scpType = "SCP 0 9 6";
                        break;
                    case RoleTypeId.Scp049:
                        scpType = "SCP 0 4 9";
                        break;
                    case RoleTypeId.Scp079:
                        scpType = "SCP 0 7 9";
                        break;
                    case RoleTypeId.Scp173:
                        scpType = "SCP 1 7 3";
                        break;
                    case RoleTypeId.Scp939:
                        scpType = "SCP 9 3 9";
                        break;
                    case RoleTypeId.Scp106:
                        scpType = "SCP 1 0 6";
                        break;
                    case RoleTypeId.Scp0492:
                        break;
                }
                if(scpType != null)
                    Cassie.Message($"{scpType} has been terminated by G R U division P");
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsGRUP(ev.Player) && ev.NewRole != RoleTypeId.Tutorial)
                API.DestroyGRUP(ev.Player);
        }

        public void OnSpawn(RespawningTeamEventArgs ev)
        {
            if (!SHSpawned)
            {
                foreach (var ply in Player.List)
                {
                    if (ply.SessionVariables.ContainsKey("IsSH"))
                        SHSpawned = false;
                }
            }

            if(SHSpawned)
                return;

            if ((Respawn.NtfTickets - Respawn.ChaosTickets) < cfg.differenceTickets || (Respawn.ChaosTickets - Respawn.NtfTickets) < cfg.differenceTickets)
            {
                if(UnityEngine.Random.Range(1, 101) > Plugin.Singleton.Config.Chance)
                {
                    ev.IsAllowed = false;
                    RespawnTokensManager.RemoveTokens(SpawnableTeamType.NineTailedFox, 2);
                    Respawn.ChaosTickets += 2;
                    API.SpawnSquad(ev.Players.Count);
                }
            }
        }

        public EventHandlers(Plugin plugin)
        {
            cfg = plugin.Config;
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            if (!API.IsGRUP(ev.Player.Cuffer))
            {
                return;
            }

            ev.IsAllowed = false;
            API.Escaped += 1;
            List<Item> items = ev.Player.Items.ToList();
            API.SpawnPlayer(ev.Player, cfg.Classes[cfg.EscapeClass]);
            Timing.CallDelayed(1f, () =>
            {
                foreach (Item item in items)
                {
                    item.CreatePickup(ev.Player.Position);
                }
            });
        }
    }
}