using System;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using Map = Exiled.Events.Handlers.Map;

namespace GRU_P
{
    using System.Linq;
    using Exiled.API.Enums;
    using GRU_P.Models;

    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;

        public override string Author { get; } = "moddedmcplayer";
        public override string Name { get; } = "GRU-P";
        public override Version Version { get; } = new Version(1, 2, 0);
        public override Version RequiredExiledVersion { get; } = new Version(8, 0, 0);
        public override PluginPriority Priority { get; } = PluginPriority.Higher;

        public EventHandlers? EventHandler;

        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents(); 
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Singleton = null!;
            UnRegisterEvents();
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            EventHandler = new EventHandlers(this);
            Player.Died += EventHandler.OnDied;
            Player.ChangingRole += EventHandler.OnChangingRole;
            Server.RespawningTeam += EventHandler.OnSpawn;
            Server.EndingRound += EventHandler.OnEndingRound;
            Server.RoundStarted += EventHandler.OnRoundStarted;
            Map.AnnouncingScpTermination += EventHandler.OnAnnouncingScpTerminationEvent;
            Player.Escaping += EventHandler.OnEscaping;

            Config.CustomCard.Register();
        }

        private void UnRegisterEvents()
        {
            Player.Died -= EventHandler.OnDied;
            Player.ChangingRole -= EventHandler.OnChangingRole;
            Server.RespawningTeam -= EventHandler.OnSpawn;
            Server.EndingRound -= EventHandler.OnEndingRound;
            Server.RoundStarted -= EventHandler.OnRoundStarted;
            Map.AnnouncingScpTermination -= EventHandler.OnAnnouncingScpTerminationEvent;
            Player.Escaping -= EventHandler.OnEscaping;
            EventHandler = null;

            Config.CustomCard.Unregister();
        }

        public static GRUPClass? GetClass(string name)
        {
            return Singleton.Config.Classes.FirstOrDefault(x => string.Equals(x.Key, name, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }
}