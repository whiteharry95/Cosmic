namespace Cosmic.Items.WorldObjects {
    using Cosmic.Entities;
    using Cosmic.TileMap;
    using Cosmic.Tiles;
    using Cosmic.WorldObjects;
    using Microsoft.Xna.Framework;
    using System;

    public class WorldObjectItem : Item {
        public WorldObject worldObject;

        public override void Load() {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnPrimaryUse() {
            int tileWidth = (int)Math.Ceiling((float)worldObject.sprite.texture.Width / Tile.Size);
            int tileHeight = (int)Math.Ceiling((float)worldObject.sprite.texture.Height / Tile.Size);

            Vector2 mouseTilePosition = TileMap.GetWorldToTilePosition(InputManager.GetMousePosition() + new Vector2(tileWidth % 2f == 0f ? (Tile.Size / 2f) : 0f, tileHeight % 2f == 0f ? (Tile.Size / 2f) : 0f)).ToVector2() * Tile.Size;

            /*if (EntityManager.GetEntitiesIntersectingPolygon<WorldObjectEntity>(new Box(mouseTilePosition - worldObject.sprite.origin, new Vector2(tileWidth, tileHeight) * Tile.Size)).Count == 0) {
                EntityManager.AddEntity<WorldObjectEntity>(mouseTilePosition, Game1.server.netPlayers[0].player.world, worldObjectEntity => worldObjectEntity.worldObject = worldObject);
                Game1.server.netPlayers[0].player.inventory.RemoveItem(this);
            }*/

            /*foreach (Point tilePosition in Game1.server.netPlayers[0].player.tileSelection) {
                TileMap tileMap = Game1.server.netPlayers[0].player.tileSelectionWalls ? UniverseManager.universeCurrent.worldCurrent.tileMapWalls : UniverseManager.universeCurrent.worldCurrent.tileMap;

                if (tileMap.tiles[tilePosition.X, tilePosition.Y] == null) {
                    if (!tileMap.GetTilePositionCollisionWithEntity<Character>(tilePosition) || Game1.server.netPlayers[0].player.tileSelectionWalls) {
                        tileMap.tiles[tilePosition.X, tilePosition.Y] = new TileMapTile(tile, tileMap, tilePosition.X, tilePosition.Y);
                        Game1.server.netPlayers[0].player.inventory.RemoveItem(this);
                    }
                }
            }*/
        }
    }
}