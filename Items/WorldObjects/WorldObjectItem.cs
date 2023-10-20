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
            if (EntityManager.player.tileSelection.Count > 0) {
                int tileWidth = (int)Math.Ceiling((float)worldObject.sprite.texture.Width / Tile.Size);
                int tileHeight = (int)Math.Ceiling((float)worldObject.sprite.texture.Height / Tile.Size);

                Vector2 mouseTilePosition = TileMap.GetWorldToTilePosition(InputManager.GetMouseWorldPosition() + new Vector2(tileWidth % 2f == 0f ? (Tile.Size / 2f) : 0f, tileHeight % 2f == 0f ? (Tile.Size / 2f) : 0f)).ToVector2() * Tile.Size;

                Polygon worldObjectPolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, tileHeight) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size));

                if (EntityManager.GetEntitiesIntersectingPolygon<WorldObjectEntity>(worldObjectPolygon).Count == 0) {
                    if (EntityManager.player.world.tileMap.GetTilesIntersectingPolygon(worldObjectPolygon).Count == 0) {
                        bool place = false;

                        Polygon worldObjectPlaceTypePolygon;

                        switch (worldObject.placeType) {
                            case WorldObject.PlaceType.Floor:
                                worldObjectPlaceTypePolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, 1f) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size) + new Vector2(0f, tileHeight * Tile.Size));
                                place = EntityManager.player.world.tileMap.GetTilesIntersectingPolygon(worldObjectPlaceTypePolygon).Count == tileWidth;
                                break;

                            case WorldObject.PlaceType.Ceiling:
                                worldObjectPlaceTypePolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(tileWidth, 1f) * Tile.Size), () => mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size) - new Vector2(0f, Tile.Size));
                                place = EntityManager.player.world.tileMap.GetTilesIntersectingPolygon(worldObjectPlaceTypePolygon).Count == tileWidth;
                                break;
                        }

                        if (place) {
                            EntityManager.AddEntity<WorldObjectEntity>(mouseTilePosition - (new Vector2(MathF.Floor(tileWidth / 2f), MathF.Floor(tileHeight / 2f)) * Tile.Size), EntityManager.player.world, worldObjectEntity => worldObjectEntity.worldObject = worldObject);
                            EntityManager.player.inventory.RemoveItem(this);
                        }
                    }
                }
            }
        }
    }
}