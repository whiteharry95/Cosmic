namespace Cosmic.Items.Blocks {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using Cosmic.TileMap;
    using Cosmic.Entities;

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
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                TileMap tileMap = wall ? EntityManager.player.world.tileMapWalls : EntityManager.player.world.tileMap;

                if (tilePosition.X < 0 || tilePosition.Y < 0 || tilePosition.X >= tileMap.Width || tilePosition.Y >= tileMap.Height) {
                    continue;
                }

                if (tileMap.GetTile(tilePosition.X, tilePosition.Y) == null) {
                    if (!tileMap.GetTilePositionCollisionWithEntity<Entity>(tilePosition.X, tilePosition.Y, entity => !(entity is Hitbox) && !(entity is DamageText)) || wall) {
                        tileMap.PlaceTile(tilePosition.X, tilePosition.Y, tile);
                        EntityManager.player.inventory.RemoveItem(this);
                    }
                }
            }
        }
    }
}