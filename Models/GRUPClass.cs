namespace GRU_P.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Exiled.API.Enums;

    public class GRUPClass
    {
        public int MaxPlayers { get; set; }
        public int Priority { get; set; }
        public int Health { get; set; }
        public List<string> SpawnItems { get; set; }
        public Dictionary<AmmoType, ushort> SpawnAmmo { get; set; }

        public string GetName()
        {
            return Plugin.Singleton.Config.Classes.FirstOrDefault(x => x.Value == this).Key ?? string.Empty;
        }
    }
}