using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.API.Interfaces;
using GRU_P.Items;

namespace GRU_P
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Maximum size of a spawn wave")]
        public int maxSquadSize { get; set; } = 10;

        [Description("The maximum difference between mtf & chaos tickets to trigger GRU-P spawn")]
        public int differenceTickets = 5;

        [Description("Whether or not to display the help message hint on spawn")]
        public bool helpMessage = true;

        [Description("Chance of gru-p spawning")]
        public int Chance = 50;
        [Description("Items GRU-P agents spawn with")]
        public List<string> SpawnItemsAgent { get; set; } = new List<string>
        {
            "KeycardChaosInsurgency",
            "GunFSP9",
            "GunRevolver",
            "GrenadeFlash",
            "Medkit",
            "Painkillers",
            "Radio",
            "ArmorLight"
        };
        
        [Description("Ammo types GRU-P agents spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmoAgent { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato9, 90 },
            { AmmoType.Ammo44Cal, 18 },
        };
        
        [Description("Items GRU-P troopers spawn with")]
        public List<string> SpawnItemsTrooper { get; set; } = new List<string>
        {
            "KeycardChaosInsurgency",
            "GunAK",
            "GrenadeHE",
            "Medkit",
            "Painkillers",
            "Radio",
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
            "KeycardChaosInsurgency",
            "GunLogicer",
            "GunFSP9",
            "GrenadeHE",
            "Medkit",
            "Painkillers",
            "Radio",
            "ArmorHeavy"
        };
        
        [Description("Ammo types GRU-P commissar spawn with")]
        public Dictionary<AmmoType, ushort> SpawnAmmoCommissar { get; set; } = new Dictionary<AmmoType, ushort>()
        {
            { AmmoType.Nato762, 120 },
            { AmmoType.Nato9, 120 }
        };

        [Description("The GRU-P custom keycard")]
        public GRUPKeycard CustomCard = new GRUPKeycard { Type = ItemType.KeycardFacilityManager};

        [Description("The permissions of the Keycard")]
        public KeycardPermissions[] KeycardPermissionsList = new KeycardPermissions[]
        {
            KeycardPermissions.ContainmentLevelOne,
            KeycardPermissions.ContainmentLevelTwo,
            KeycardPermissions.ArmoryLevelOne,
            KeycardPermissions.ArmoryLevelTwo,
            KeycardPermissions.ArmoryLevelThree,
            KeycardPermissions.Checkpoints,
            KeycardPermissions.ExitGates,
            KeycardPermissions.Intercom,
        };
    }
}