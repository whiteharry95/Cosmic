namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using Cosmic.Tiles;
    using Cosmic.Worlds;

    public class BlockItem : Item {
        public Tile tile;

        public override void Load(ContentManager contentManager) {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnUse() {
            foreach (Point tileSelection in EntityManager.player.tileSelection) {
                Tilemap tilemap = EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tilemapWalls : WorldManager.worldCurrent.tilemap;

                if (tilemap.tiles[tileSelection.X, tileSelection.Y] == null) {
                    if (!tilemap.GetTilePositionCollisionWithEntity<CharacterEntity>(tileSelection.X, tileSelection.Y)) {
                        tilemap.tiles[tileSelection.X, tileSelection.Y] = new TilemapTile(tile, tilemap, tileSelection.X, tileSelection.Y);
                        EntityManager.player.inventory.RemoveItem(this);
                    }
                }
            }
        }
    }
}