namespace Cosmic.Items {
    using Cosmic.Assets;
    using Cosmic.Worlds;

    public class WorldBuilder : Item {
        public override void Load() {
            name = "World Builder";

            sprite = new Sprite(TextureManager.Items_WorldBuilder, Sprite.OriginPreset.MiddleCentre);

            useHold = false;
        }

        public override void OnPrimaryUse() {
            WorldManager.AddWorld<World>(120, 80, true);
        }
    }
}