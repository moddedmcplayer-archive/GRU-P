using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using GRU_P.Items;

namespace GRU_P
{
    using GRU_P.Models;
    using UnityEngine;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Maximum size of a spawn wave")]
        public int maxSquadSize { get; set; } = 10;

        [Description("The maximum difference between mtf & chaos influence to trigger GRU-P spawn")]
        public float DifferenceInfluence { get; set; } = 5;

        [Description("Whether or not to display the help message hint on spawn")]
        public bool EnableSpawnMessage { get; set; } = true;

        public string SpawnMessage { get; set; } =
            "Youre now a GRU-P %type%, for more information type [.grup help] in the console";

        [Description("Chance of gru-p spawning")]
        public int Chance { get; set; } = 50;

        public string EscapeClass { get; set; } = "Agent";
        public Dictionary<string, GRUPClass> Classes { get; set; } = new Dictionary<string, GRUPClass>()
        {
            ["Commissar"] = new GRUPClass()
            {
                Priority = 100,
                MaxPlayers = 1,
                Health = 100,
                SpawnItems = new List<string>()
                {
                    "KeycardChaosInsurgency",
                    "GunLogicer",
                    "GunFSP9",
                    "GrenadeHE",
                    "Medkit",
                    "Painkillers",
                    "Radio",
                    "ArmorHeavy"
                },
                SpawnAmmo = new Dictionary<AmmoType, ushort>()
                {
                    { AmmoType.Nato762, 120 },
                    { AmmoType.Nato9, 120 }
                }
            },
            ["Trooper"] = new GRUPClass()
            {
                Priority = 50,
                MaxPlayers = 3,
                Health = 100,
                SpawnItems = new List<string>()
                {
                    "KeycardChaosInsurgency",
                    "GunAK",
                    "GrenadeHE",
                    "Medkit",
                    "Painkillers",
                    "Radio",
                    "ArmorCombat"
                },
                SpawnAmmo = new Dictionary<AmmoType, ushort>()
                {
                    {AmmoType.Nato762, 90},
                }
            },
            ["Agent"] = new GRUPClass()
            {
                Priority = 0,
                MaxPlayers = -1,
                Health = 100,
                SpawnItems = new List<string>()
                {
                    "KeycardChaosInsurgency",
                    "GunFSP9",
                    "GunRevolver",
                    "GrenadeFlash",
                    "Medkit",
                    "Painkillers",
                    "Radio",
                    "ArmorLight"
                },
                SpawnAmmo = new Dictionary<AmmoType, ushort>()
                {
                    {AmmoType.Nato9, 90},
                    {AmmoType.Ammo44Cal, 18},
                }
            }
        };

        [Description("The GRU-P custom keycard")]
        public GRUPKeycard CustomCard { get; set; } = new GRUPKeycard { Type = ItemType.KeycardFacilityManager};

        [Description("The permissions of the Keycard")]
        public KeycardPermissions[] KeycardPermissionsList { get; set; } = new KeycardPermissions[]
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

        public Vector3 SpawnPoint { get; set; } = new Vector3(-36f, 993f, -36f);
        public bool EnableSpawnCassie { get; set; } = true;
        public bool EnableSubtitles { get; set; } = true;
        public string SpawnCassie { get; set; } = "G R U division P squad has entered the facility";
        public List<string> HelpCommandMessages { get; set; } = new List<string>
        {
            "The GRU-P are only hostile to SCPs, but neutral to everyone else.",
            "The hierarchy is as follows: Commissar > Trooper > Agent.",
            $"They spawn when the spawn ticket difference between MTF and CI is less than %ticketdiff%."
        };
    }
}