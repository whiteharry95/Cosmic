namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using Cosmic.Tiles;
    using Cosmic.TileMap;
    using Cosmic.Universes;

    public class BlockItem : Item {
        public Tile tile;

        public override void Load() {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnPrimaryUse() {
            Place(false);
        }

        public override void OnSecondaryUse() {
            Place(true);
        }

        protected virtual void Place(bool wall) {
            foreach (Point tilePosition in Game1.server.netPlayers[0].player.tileSelection) {
                TileMap tileMap = wall ? UniverseManager.universeCurrent.worldCurrent.tileMapWalls : UniverseManager.universeCurrent.worldCurrent.tileMap;

                if (tileMap.tiles[tilePosition.X, tilePosition.Y] == null) {
                    if (!tileMap.GetTilePositionCollisionWithEntity<Character>(tilePosition) || Game1.server.netPlayers[0].player.tileSelectionWalls) {
                        tileMap.tiles[tilePosition.X, tilePosition.Y] = new TileMapTile(tile, tileMap, tilePosition.X, tilePosition.Y);
                        Game1.server.netPlayers[0].player.inventory.RemoveItem(this);
                    }
                }
            }
        }
    }
}