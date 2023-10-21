namespace Cosmic.WorldObjects {
    using Cosmic.Assets;
    using Cosmic.Items;

    public class Chandelier : WorldObject {
        public override void Load() {
            sprite = new Sprite(TextureManager.WorldObjects_Chandelier, Sprite.OriginPreset.MiddleCentre);

            life = 45;

            item = ItemManager.Chandelier;

            placeType = PlaceType.Ceiling;
        }
    }
}