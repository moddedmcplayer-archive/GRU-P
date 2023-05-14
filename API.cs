using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using MEC;
using UnityEngine;

namespace GRU_P
{
    using PlayerRoles;

    public static class API
    {
        private static Config cfg => Plugin.Singleton.Config;
        public static bool IsGRUP(Player player) => player != null && player.SessionVariables.ContainsKey("IsGRUP");
        public static string GetGRUPType(Player player) => player.SessionVariables["IsGRUP"].ToString();
        public static List<Player> GetGRUPPlayers() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsGRUP")).ToList();
        public static int CountRoles(Team team) => Player.List.Count(x => x.Role.Team == team && !x.SessionVariables.ContainsKey("IsNPC"));

        public static int Escaped = 0;

        public static void ModifyKeycard(Player player, CustomItem cItem)
        {
            foreach (Item item in player.Items)
            {
                if (cItem.Check(item))
                {
                    if (item is Keycard keycard)
                    {
                        int perms = 0;
                        for (int i = 0; i < Plugin.Singleton.Config.KeycardPermissionsList.Length; i++)
                        {
                            perms |= (int)Plugin.Singleton.Config.KeycardPermissionsList[i];
                        }
                        keycard.Base.Permissions = (Interactables.Interobjects.DoorUtils.KeycardPermissions)perms;
                    }
                }
            }
        }
        
        public static void SpawnPlayer(Player player, string type)
        {
            player.SessionVariables.Add("IsGRUP", type);
            player.Role.Set(RoleTypeId.Tutorial);
            player.Health = 100;
            player.MaxHealth = 100;
            player.CustomInfo = $"GRUP-{UnityEngine.Random.Range(1, 50)}";
            
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
            Timing.CallDelayed(1.7f, () => player.Position = Plugin.Singleton.Config.SpawnPoint);
            if(Plugin.Singleton.Config.SpawnItemsAgent.Contains("GRU-P-Keycard"))
                Timing.CallDelayed(1, () => ModifyKeycard(player, CustomItem.Get(1u)));
            if (Plugin.Singleton.Config.EnableSpawnMessage)
                Timing.CallDelayed(1.0f, () => player.Broadcast(7, Plugin.Singleton.Config.SpawnMessage.Replace("%type%", type)));
        }

        public static void SpawnSquad(int size, bool silent = false)
        {
            List<Player> spec = Player.List.Where(x => x.Role.Team == Team.Dead && !x.IsOverwatchEnabled).ToList();
            spec = spec.OrderByDescending(x => x.ReferenceHub.roleManager.CurrentRole.ActiveTime).ToList();
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

            if (Plugin.Singleton.Config.EnableSpawnCassie)
            {
                Cassie.Message(Plugin.Singleton.Config.SpawnCassie, false, true, Plugin.Singleton.Config.EnableSubtitles);
            }
        }
        
        public static void DestroyGRUP(Player player)
        {
            player.SessionVariables.Remove("IsGRUP");
            player.MaxHealth = default;
            player.Health = default;
            player.CustomInfo = string.Empty;
            player.CustomInfo = string.Empty;

            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Nickname;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.UnitName;
            player.ReferenceHub.nicknameSync.ShownPlayerInfo |= PlayerInfoArea.Role;
        }
    }
}