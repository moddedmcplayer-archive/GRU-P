using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace GRU_P
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        [Description("Items GRU-P agents spawn with")]
        public List<string> SpawnItemsAgent { get; set; } = new List<string>
        {
            "GunAK",
            "KeycardChaosInsurgency",
            "Radio",
            "Medkit",
            "Painkillers",
            "ArmorCombat"
        };
        
        [Description("Ammo types GRU-P agents spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmoAgent { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 90 },
        };
        
        [Description("Items GRU-P troopers spawn with")]
        public List<string> SpawnItemsTrooper { get; set; } = new List<string>
        {
            "GunAK",
            "KeycardChaosInsurgency",
            "Radio",
            "Medkit",
            "Painkillers",
            "ArmorCombat"
        };
        
        [Description("Ammo types GRU-P troopers spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmoTrooper { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 90 },
        };
        
        [Description("Items GRU-P commissar spawn with")]
        public List<string> SpawnItemsCommissar { get; set; } = new List<string>
        {
            "GunAK",
            "KeycardChaosInsurgency",
            "Radio",
            "Medkit",
            "Painkillers",
            "ArmorCombat"
        };
        
        [Description("Ammo types GRU-P commissar spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmoCommissar { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 90 },
        };
    }
}