namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using Cosmic.Tiles;
    using Cosmic.Worlds;
    using Cosmic.TileMap;

    public class BlockItem : Item {
        public Tile tile;

        public override void Load() {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnUse() {
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                TileMap tileMap = EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tileMapWalls : WorldManager.worldCurrent.tileMap;

                if (tileMap.tiles[tilePosition.X, tilePosition.Y] == null) {
                    if (!tileMap.GetTilePositionCollisionWithEntity<Character>(tilePosition) || EntityManager.player.tileSelectionWalls) {
                        tileMap.tiles[tilePosition.X, tilePosition.Y] = new TileMapTile(tile, tileMap, tilePosition.X, tilePosition.Y);
                        EntityManager.player.inventory.RemoveItem(this);
                    }
                }
            }
        }
    }
}