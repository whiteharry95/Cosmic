namespace Cosmic.Items.WorldObjects
{
    using Cosmic.Entities;
    using Cosmic.Tiles;
    using Cosmic.WorldObjects;
    using Cosmic.Worlds;
    using Microsoft.Xna.Framework;
    using System;

    public class WorldObjectItem : Item {
        public WorldObject WorldObject { get; protected set; }

        public override void Load() {
            stack = 999;
            showTileSelection = true;
        }

        public override void OnPrimaryUse() {
            bool tileSelectionHasActiveElement = false;

            for (int y = 0; y < EntityManager.Player.TileSelection.GetLength(1); y++) {
                for (int x = 0; x < EntityManager.Player.TileSelection.GetLength(0); x++) {
                    if (EntityManager.Player.TileSelection[x, y]) {
                        tileSelectionHasActiveElement = true;
                        break;
                    }
                }
            }

            if (tileSelectionHasActiveElement) {
                int tileWidth = (int)Math.Ceiling((float)WorldObject.sprite.texture.Width / Tile.Size);
                int tileHeight = (int)Math.Ceiling((float)WorldObject.sprite.texture.Height / Tile.Size);

                Vector2 mouseTilePosition = WorldTileMap.GetWorldToTilePosition(InputManager.GetMouseWorldPosition() + new Vector2(tileWidth % 2f == 0f ? (Tile.Size / 2f) : 0f, tileHeight % 2f == 0f ? (Tile.Size / 2f) : 0f)).ToVector2() * Tile.Size;

                Polygon worldObjectPolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, tileHeight) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size));

                if (EntityManager.GetEntitiesIntersectingPolygon<WorldObjectEntity>(worldObjectPolygon).Count == 0) {
                    if (EntityManager.Player.world.TileMap.GetTilesIntersectingPolygon(worldObjectPolygon).Count == 0) {
                        bool place = false;

                        switch (WorldObject.placeType) {
                            case WorldObject.PlaceType.Floor:
                                place = EntityManager.Player.world.TileMap.GetTilesIntersectingPolygon(new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, 1f) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size) + new Vector2(0f, tileHeight * Tile.Size))).Count == tileWidth;
                                break;

                            case WorldObject.PlaceType.Ceiling:
                                place = EntityManager.Player.world.TileMap.GetTilesIntersectingPolygon(new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, 1f) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size) - new Vector2(0f, Tile.Size))).Count == tileWidth;
                                break;
                        }

                        if (place) {
                            EntityManager.AddEntity<WorldObjectEntity>(mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size), EntityManager.Player.world, worldObjectEntity => worldObjectEntity.worldObject = WorldObject);
                            EntityManager.Player.Inventory.RemoveItem(this);
                        }
                    }
                }
            }
        }
    }
}