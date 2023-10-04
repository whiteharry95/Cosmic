namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;

    public class EntityCollider {
        private const int TileRange = 12;

        public Entity entity;
        public Box box;

        public EntityCollider(Entity entity, Box box) {
            this.entity = entity;
            this.box = box;
        }

        public Box GetBoxReal() {
            return new Box(entity.position + box.Position, box.Size);
        }

        public bool GetCollisionWithEntities<T>(Vector2? offset = null) where T : Entity {
            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            List<Entity> entitiesInWorld = EntityManager.GetEntitiesInWorld(entity.world);

            foreach (Entity entity in entitiesInWorld) {
                if (entity == this.entity) {
                    continue;
                }

                if (entity is T) {
                    if (boxOffset.GetIntersects(entity.collider.GetBoxReal())) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetCollisionWithEntities<T>(out List<T> entities, Vector2? offset = null) where T : Entity {
            entities = new List<T>();

            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            List<Entity> entitiesInWorld = EntityManager.GetEntitiesInWorld(entity.world);

            foreach (Entity entity in entitiesInWorld) {
                if (entity == this.entity) {
                    continue;
                }

                if (entity is T) {
                    if (boxOffset.GetIntersects(entity.collider.GetBoxReal())) {
                        entities.Add(entity as T);
                    }
                }
            }

            return entities.Count > 0;
        }

        public bool GetCollisionWithTiles(Vector2? offset = null, Predicate<TileMapTile> predicate = null) {
            List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(TileMap.GetWorldToTilePosition(entity.position + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f)), TileRange);

            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                if (predicate?.Invoke(tileMapTile) ?? true) {
                    if (boxOffset.GetIntersects(tileMapTile.GetBox())) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetCollisionWithTiles(out List<TileMapTile> tileMapTiles, Vector2? offset = null, Predicate<TileMapTile> predicate = null) {
            tileMapTiles = new List<TileMapTile>();

            List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(TileMap.GetWorldToTilePosition(entity.position + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f)), TileRange);

            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                if (predicate?.Invoke(tileMapTile) ?? true) {
                    if (boxOffset.GetIntersects(tileMapTile.GetBox())) {
                        tileMapTiles.Add(tileMapTile);
                    }
                }
            }

            return tileMapTiles.Count > 0;
        }

        public void MakeContactWithTiles(float distance, int direction) {
            for (float distanceMoved = 0f; distanceMoved < distance;) {
                float distanceToMove = Math.Min(0.5f, distance - distanceMoved);

                Vector2 positionTo = new Vector2(entity.position.X, entity.position.Y);

                switch (direction) {
                    case 0:
                        if (entity.position.X != Math.Ceiling(entity.position.X)) {
                            float offset = (float)(Math.Ceiling(entity.position.X) - entity.position.X);

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionTo.X += distanceToMove;

                        break;

                    case 1:
                        if (entity.position.Y != Math.Floor(entity.position.Y)) {
                            float offset = (float)(entity.position.Y - Math.Floor(entity.position.Y));

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionTo.Y -= distanceToMove;

                        break;

                    case 2:
                        if (entity.position.X != Math.Floor(entity.position.X)) {
                            float offset = (float)(entity.position.X - Math.Floor(entity.position.X));

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionTo.X -= distanceToMove;

                        break;

                    case 3:
                        if (entity.position.Y != Math.Ceiling(entity.position.Y)) {
                            float offset = (float)(Math.Ceiling(entity.position.Y) - entity.position.Y);

                            if (offset < distanceToMove) {
                                distanceToMove = offset;
                            }
                        }

                        positionTo.Y += distanceToMove;

                        break;
                }

                bool collision = false;

                List<TileMapTile> tileMapTilesWithinRange = entity.world.tileMap.GetTilesWithinRange(TileMap.GetWorldToTilePosition(positionTo + new Vector2(TileRange % 2f == 0f ? (Tile.Size / 2f) : 0f)), TileRange);

                foreach (TileMapTile tileMapTile in tileMapTilesWithinRange) {
                    if (new Box(positionTo + box.Position, box.Size).GetIntersects(tileMapTile.GetBox())) {
                        collision = true;
                        break;
                    }
                }

                if (!collision) {
                    entity.position = positionTo;
                    distanceMoved += distanceToMove;
                } else {
                    break;
                }
            }
        }
    }
}