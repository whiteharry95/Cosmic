namespace Cosmic.Items.Blocks
{
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using Cosmic.Entities;
    using Cosmic.Worlds;

    public class BlockItem : Item {
        protected Tile tile;

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
            for (int y = 0; y < EntityManager.Player.TileSelection.GetLength(1); y++) {
                for (int x = 0; x < EntityManager.Player.TileSelection.GetLength(0); x++) {
                    if (EntityManager.Player.TileSelection[x, y]) {
                        WorldTileMap tileMap = wall ? EntityManager.Player.world.TileMapWalls : EntityManager.Player.world.TileMap;

                        Point tileSelectionElementPosition = EntityManager.Player.TileSelectionPosition + new Point(x, y);

                        if (tileSelectionElementPosition.X < 0 || tileSelectionElementPosition.Y < 0 || tileSelectionElementPosition.X >= tileMap.Width || tileSelectionElementPosition.Y >= tileMap.Height) {
                            continue;
                        }

                        if (tileMap.GetTile(tileSelectionElementPosition.X, tileSelectionElementPosition.Y) == null) {
                            if (!tileMap.GetTilePositionCollisionWithEntity<Entity>(tileSelectionElementPosition.X, tileSelectionElementPosition.Y, entity => !(entity is Hitbox) && !(entity is DamageText)) || wall) {
                                tileMap.PlaceTile(tileSelectionElementPosition.X, tileSelectionElementPosition.Y, tile);
                                EntityManager.Player.Inventory.RemoveItem(this);
                            }
                        }
                    }
                }
            }
        }
    }
}