namespace Cosmic.Items {
    using Cosmic.Assets;
    using Cosmic.Entities;
    using Cosmic.Worlds;

    public class WorldTeleporter : Item {
        public override void Load() {
            name = "World Teleporter";

            sprite = new Sprite(TextureManager.Items_WorldTeleporter, Sprite.OriginPreset.MiddleCentre);

            useHold = false;
        }

        public override void OnPrimaryUse() {
            int worldCurrentIndex = WorldManager.Worlds.FindIndex(0, world => world == WorldManager.WorldCurrent);

            worldCurrentIndex++;
            worldCurrentIndex %= WorldManager.Worlds.Count;

            if (worldCurrentIndex < 0) {
                worldCurrentIndex += WorldManager.Worlds.Count;
            }

            WorldManager.WorldCurrent = WorldManager.Worlds[worldCurrentIndex];

            EntityManager.player.world = WorldManager.WorldCurrent;
        }
    }
}