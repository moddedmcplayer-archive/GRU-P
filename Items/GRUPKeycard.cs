using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using Player = Exiled.Events.Handlers.Player;

namespace GRU_P.Items
{
    public class GRUPKeycard : CustomItem
    {
        public override uint Id { get; set; } = 1;
        
        public override string Name { get; set; } = "GRU-P-Keycard";
        
        public override string Description { get; set; } =
            "A facility manager keycard with the same access level as a chaos hacking device.";
        
        public override float Weight { get; set; } = 1f;
        
        public override SpawnProperties SpawnProperties { get; set; }

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.InteractingDoor += OnInteractingDoor;
            base.UnsubscribeEvents();
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!Check(ev.Player.CurrentItem))
            {
                return;
            }

            if (!Plugin.Singleton.Config.KeycardPermissionsList.Contains((KeycardPermissions) ev.Door.RequiredPermissions.RequiredPermissions) && (KeycardPermissions) ev.Door.RequiredPermissions.RequiredPermissions != KeycardPermissions.None)
            {
                ev.IsAllowed = false;
            }
            else
            {
                ev.IsAllowed = true;
            }
        }
    }
}