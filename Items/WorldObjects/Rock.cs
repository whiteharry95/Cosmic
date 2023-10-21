namespace Cosmic.Items.WorldObjects {
    using Cosmic.WorldObjects;

    public class Rock : WorldObjectItem {
        public override void Load() {
            name = "Rock";

            sprite = new Sprite(WorldObjectManager.Rock.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            WorldObject = WorldObjectManager.Rock;

            base.Load();
        }
    }
}