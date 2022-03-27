using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using MEC;
using UnityEngine;

namespace GRU_P
{
    public static class API
    {
        private static Plugin plugin = Plugin.Singleton;
        private static Config cfg = Plugin.Singleton.Config;
            
        public static bool IsGRUP(Player player) => player != null && player.SessionVariables.ContainsKey("IsGRUP");
        public static string GetGRUPType(Player player) => player.SessionVariables["IsGRUP"].ToString();
        public static List<Player> GetGRUPPlayers() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsGRUP")).ToList();
        public static int CountRoles(Team team) => Player.List.Count(x => x.Role.Team == team && !x.SessionVariables.ContainsKey("IsNPC"));


        public static void SpawnPlayer(Player player, string type)
        {
            player.SessionVariables.Add("IsGRUP", type);
            player.Role.Type = RoleType.Tutorial;
            player.Health = 100;
            player.MaxHealth = 100;
            player.UnitName = $"GRUP-{UnityEngine.Random.Range(1, 50)}";
            player.CustomInfo = $"{player.Nickname} - GRU-P {type}";

            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            
            Timing.CallDelayed(0.4f, () =>
            {
                switch (type)
                {
                    case "agent":
                        player.ResetInventory(cfg.SpawnItemsAgent);
                        foreach (var ammo in cfg.SpawnAmmoAgent)
                        {
                            player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                        }
                        break;
                    case "trooper":
                        player.ResetInventory(cfg.SpawnItemsTrooper);
                        foreach (var ammo in cfg.SpawnAmmoTrooper)
                        {
                            player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                        }
                        break;
                    case "commissar":
                        player.ResetInventory(cfg.SpawnItemsCommissar);
                        foreach (var ammo in cfg.SpawnAmmoCommissar)
                        {
                            player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                        }
                        break;
                }
            });
            Timing.CallDelayed(1.7f, () => player.Position = new Vector3(-31f, 989f, -50f));
            Timing.CallDelayed(1.0f, () => player.Broadcast(7, $"Youre now a GRU-P {type}, for more information type .grup-help in the console"));
        }

        public static void SpawnSquad(int size)
        {
            List<Player> spec = Player.List.Where(x => x.Role.Team == Team.RIP && !x.IsOverwatchEnabled).ToList();
            spec = spec.OrderBy(x => x.ReferenceHub.characterClassManager.DeathTime).ToList();
            int spawned = 0;
            string type = "trooper";
            SpawnPlayer(spec[spec.Count-1], "commissar");
            spec.RemoveAt(spec.Count-1);
            while(spawned <= size && spec.Count > 0)
            {
                SpawnPlayer(spec[0], type);
                spec.RemoveAt(0);
                spawned++;
                if (spawned > 4)
                {
                    type = "agent";
                }
            }
        }
        
        public static void DestroyGRUP(Player player)
        {
            player.SessionVariables.Remove("IsGRUP");
            player.MaxHealth = default;
            player.Health = default;
            player.CustomInfo = string.Empty;
            player.UnitName = string.Empty;

            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.UnitName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
        }
    }
}