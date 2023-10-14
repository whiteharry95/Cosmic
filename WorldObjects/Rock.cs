namespace Cosmic.WorldObjects {
    using Cosmic.Assets;
    using Cosmic.Items;

    public class Rock : WorldObject {
        public override void Load() {
            sprite = new Sprite(TextureManager.WorldObjects_Rock, Sprite.OriginPreset.MiddleCentre);

            life = 45;

            item = ItemManager.rock;
        }
    }
}