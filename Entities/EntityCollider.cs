namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;
    using Cosmic.Utilities;
    using Cosmic;

    public class EntityCollider {
        private const int TileRange = 12;

        public Entity entity;
        public Polygon polygon;

        public EntityCollider(Entity entity) {
            this.entity = entity;
            polygon = new Polygon(() => Polygon.GetRectangleVertices(entity.sprite.mask.Size.ToVector2()), () => entity.position, () => entity.rotation, () => entity.scale, () => entity.sprite.origin - entity.sprite.mask.Location.ToVector2());
        }

        public bool GetCollisionWithEntities<T>(Vector2? offset = null) where T : Entity {
            List<Entity> entitiesInWorld = EntityManager.GetEntitiesInWorld(entity.world);

            foreach (Entity entity in entitiesInWorld) {
                if (entity == this.entity) {
                    continue;
                }

                if (entity is T) {
                    if (polygon.GetCollisionWithPolygon(entity.collider.polygon, offset)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetCollisionWithEntities<T>(out List<T> entities, Vector2? offset = null) where T : Entity {
            entities = new List<T>();

            List<Entity> entitiesInWorld = EntityManager.GetEntitiesInWorld(entity.world);

            foreach (Entity entity in entitiesInWorld) {
                if (entity == this.entity) {
                    continue;
                }

                if (entity is T) {
                    if (polygon.GetCollisionWithPolygon(entity.collider.polygon, offset)) {
                        entities.Add(entity as T);
                    }
                }
            }

            return entities.Count > 0;
        }

        public bool GetCollisionWithTiles(Vector2? offset = null, Predicate<TileMapTile> predicate = null) {
            Point entityTilePosition = TileMap.GetWorldToTilePosition(entity.position + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f));
            List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(entityTilePosition.X, entityTilePosition.Y, TileRange);

            foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                if (predicate?.Invoke(tileMapTile) ?? true) {
                    if (polygon.GetCollisionWithPolygon(tileMapTile.GetPolygon(), offset)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetCollisionWithTiles(out List<TileMapTile> tileMapTiles, Vector2? offset = null, Predicate<TileMapTile> predicate = null) {
            tileMapTiles = new List<TileMapTile>();

            Point entityTilePosition = TileMap.GetWorldToTilePosition(entity.position + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f));
            List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(entityTilePosition.X, entityTilePosition.Y, TileRange);

            foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                if (predicate?.Invoke(tileMapTile) ?? true) {
                    if (polygon.GetCollisionWithPolygon(tileMapTile.GetPolygon(), offset)) {
                        tileMapTiles.Add(tileMapTile);
                    }
                }
            }

            return tileMapTiles.Count > 0;
        }

        public void MakeContactWithTiles(float distance, MathUtilities.Direction direction) {
            for (float distanceMoved = 0f; distanceMoved < distance;) {
                float distanceToMove = Math.Min(1f, distance - distanceMoved);

                Vector2 positionChange = Vector2.Zero;

                switch (direction) {
                    case MathUtilities.Direction.Right:
                        if (entity.position.X != Math.Ceiling(entity.position.X)) {
                            float offset = (float)(Math.Ceiling(entity.position.X) - entity.position.X);

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionChange.X += distanceToMove;

                        break;

                    case MathUtilities.Direction.Up:
                        if (entity.position.Y != Math.Floor(entity.position.Y)) {
                            float offset = (float)(entity.position.Y - Math.Floor(entity.position.Y));

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionChange.Y -= distanceToMove;

                        break;

                    case MathUtilities.Direction.Left:
                        if (entity.position.X != Math.Floor(entity.position.X)) {
                            float offset = (float)(entity.position.X - Math.Floor(entity.position.X));

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionChange.X -= distanceToMove;

                        break;

                    case MathUtilities.Direction.Down:
                        if (entity.position.Y != Math.Ceiling(entity.position.Y)) {
                            float offset = (float)(Math.Ceiling(entity.position.Y) - entity.position.Y);

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionChange.Y += distanceToMove;

                        break;
                }

                bool collision = false;

                Point tilePosition = TileMap.GetWorldToTilePosition(entity.position + positionChange + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f));
                
                List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(tilePosition.X, tilePosition.Y, TileRange);

                foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                    if (polygon.GetCollisionWithPolygon(tileMapTile.GetPolygon(), positionChange)) {
                        collision = true;
                        break;
                    }
                }

                if (!collision) {
                    entity.position += positionChange;
                    distanceMoved += distanceToMove;
                } else {
                    break;
                }
            }
        }
    }
}