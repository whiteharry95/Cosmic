using Cosmic.Tiles;

namespace Cosmic.TileMap {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using Cosmic.Entities;
    using System.Collections.Generic;
    using Cosmic.WorldObjects;

    public class TileMapTile {
        public Tile tile;

        public TileMap tileMap;

        public ushort x;
        public ushort y;

        public ushort life;

        public byte textureIndex;

        public TileMapTile(Tile tile, TileMap tileMap, ushort x, ushort y) {
            this.tile = tile;

            this.tileMap = tileMap;

            this.x = x;
            this.y = y;

            life = (ushort)tile.life;
        }

        public void Mine(byte damage) {
            life -= damage;

            if (life <= 0) {
                if (tile.item != null) {
                    EntityManager.AddEntity<ItemDrop>((new Point(x, y).ToVector2() + new Vector2(0.5f)) * Tile.Size, tileMap.world, itemDrop => itemDrop.item = tile.item);
                }

                Polygon worldObjectPolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size, 3f * Tile.Size)), () => new Vector2(x, y - 1) * Tile.Size);

                List<WorldObjectEntity> worldObjectEntities = EntityManager.GetEntitiesIntersectingPolygon<WorldObjectEntity>(worldObjectPolygon);

                foreach (WorldObjectEntity worldObjectEntity in worldObjectEntities) {
                    Polygon worldObjectPlaceTypePolygon = null;

                    switch (worldObjectEntity.worldObject.placeType) {
                        case WorldObject.PlaceType.Floor:
                            worldObjectPlaceTypePolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y - 1) * Tile.Size);
                            break;

                        case WorldObject.PlaceType.Ceiling:
                            worldObjectPlaceTypePolygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y + 1) * Tile.Size);
                            break;
                    }

                    if (worldObjectPlaceTypePolygon?.GetCollisionWithPolygon(worldObjectEntity.collider.polygon) ?? false) {
                        EntityManager.AddEntity<ItemDrop>(worldObjectEntity.position + worldObjectEntity.sprite.mask.Location.ToVector2() + (worldObjectEntity.sprite.mask.Size.ToVector2() / 2f), worldObjectEntity.world, itemDrop => itemDrop.item = worldObjectEntity.worldObject.item);
                        worldObjectEntity.Destroy();
                    }
                }

                tileMap.DestroyTile(x, y);
                life = 0;
            }
        }

        public Polygon GetPolygon() {
            switch (textureIndex) {
                case 1:
                    return new Polygon(() => new Vector2[3] {
                        Vector2.Zero,
                        new Vector2(Tile.Size, 0f),
                        new Vector2(0f, Tile.Size)
                    }, () => new Vector2(x, y) * Tile.Size);

                case 2:
                    return new Polygon(() => new Vector2[3] {
                        Vector2.Zero,
                        new Vector2(Tile.Size, 0f),
                        new Vector2(Tile.Size)
                    }, () => new Vector2(x, y) * Tile.Size);

                case 3:
                    return new Polygon(() => new Vector2[3] {
                        Vector2.Zero,
                        new Vector2(0f, Tile.Size),
                        new Vector2(Tile.Size)
                    }, () => new Vector2(x, y) * Tile.Size);

                case 4:
                    return new Polygon(() => new Vector2[3] {
                        new Vector2(Tile.Size, 0f),
                        new Vector2(Tile.Size),
                        new Vector2(0f, Tile.Size)
                    }, () => new Vector2(x, y) * Tile.Size);

                default:
                    return new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y) * Tile.Size);
            }
        }
    }
}