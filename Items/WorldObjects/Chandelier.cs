namespace Cosmic.Items.WorldObjects {
    using Cosmic.WorldObjects;

    public class Chandelier : WorldObjectItem {
        public override void Load() {
            name = "Chandelier";

            sprite = new Sprite(WorldObjectManager.Chandelier.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            WorldObject = WorldObjectManager.Chandelier;

            base.Load();
        }
    }
}