namespace Cosmic.Items.WorldObjects {
    using Cosmic.WorldObjects;

    public class Rock : WorldObjectItem {
        public override void Load() {
            name = "Rock";

            sprite = new Sprite(WorldObjectManager.rock.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            worldObject = WorldObjectManager.rock;

            base.Load();
        }
    }
}