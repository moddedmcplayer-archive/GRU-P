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
        public List<string> SpawnItems { get; set; } = new List<string>
        {
            "GunAK",
            "KeycardChaosInsurgency",
            "Radio",
            "Medkit",
            "Painkillers",
            "ArmorCombat"
        };
        
        [Description("Ammo types GRU-P agents spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmo { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 90 },
        };
    }
}