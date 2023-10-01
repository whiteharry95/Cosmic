namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using Cosmic.Tiles;
    using Cosmic.Worlds;

    public class BlockItem : Item {
        public Tile tile;

        public override void Generate() {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnUse() {
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                Tilemap tilemap = EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tilemapWalls : WorldManager.worldCurrent.tilemap;

                if (tilemap.tiles[tilePosition.X, tilePosition.Y] == null) {
                    if (!tilemap.GetTilePositionCollisionWithEntity<CharacterEntity>(tilePosition)) {
                        tilemap.tiles[tilePosition.X, tilePosition.Y] = new TilemapTile(tile, tilemap, tilePosition.X, tilePosition.Y);
                        EntityManager.player.inventory.RemoveItem(this);
                    }
                }
            }
        }
    }
}