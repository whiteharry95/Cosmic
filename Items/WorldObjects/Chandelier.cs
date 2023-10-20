namespace Cosmic.Items.WorldObjects {
    using Cosmic.WorldObjects;

    public class Chandelier : WorldObjectItem {
        public override void Load() {
            name = "Chandelier";

            sprite = new Sprite(WorldObjectManager.chandelier.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            worldObject = WorldObjectManager.chandelier;

            base.Load();
        }
    }
}