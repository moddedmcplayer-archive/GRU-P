using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs;
using MEC;

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
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
            base.UnsubscribeEvents();
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            Timing.CallDelayed(1, () => API.modifyKeycard(ev.Player, this));
        }
    }
}