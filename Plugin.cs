using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Exiled.API;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.Events.Extensions;
using Exiled.Events.Handlers;
using GRU_P.Items;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using Map = Exiled.Events.Handlers.Map;

namespace GRU_P
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Singleton;

        public override string Author { get; } = "moddedmcplayer";
        public override string Name { get; } = "GRU-P";
        public override Version Version { get; } = new Version(1, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(7, 0, 0);

        public EventHandlers EventHandler;

        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents(); 
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Singleton = null;
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
            EventHandler = null;
            
            Config.CustomCard.Unregister();
        }
    }
}