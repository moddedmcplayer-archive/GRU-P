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
            
        public static bool IsGRUP(Player player) => player.SessionVariables.ContainsKey("IsGRUP");
        public static List<Player> GetSHPlayers() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsGRUP")).ToList();
        public static int CountRoles(Team team) => Player.List.Count(x => x.Role.Team == team && !x.SessionVariables.ContainsKey("IsNPC"));


        public static void SpawnPlayer(Player player)
        {
            player.SessionVariables.Add("IsGRUP", null);
            player.Role.Type = RoleType.Tutorial;
            player.Health = 120;
            player.MaxHealth = 120;
            player.UnitName = $"GRU-{UnityEngine.Random.Range(1, 50)}";
            player.CustomInfo = "GRU-P Agent";
            
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.UnitName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo &= ~PlayerInfoArea.Role;
            
            Timing.CallDelayed(0.4f, () =>
            {
                player.ResetInventory(cfg.SpawnItems);
                foreach (var ammo in cfg.SpawnAmmo)
                {
                    player.Ammo[ammo.Key.GetItemType()] = ammo.Value;
                }
            });
            Timing.CallDelayed(1.7f, () => player.Position = new Vector3(0f, 1002f, 8f));
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