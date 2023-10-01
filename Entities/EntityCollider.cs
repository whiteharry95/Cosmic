namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class EntityCollider {
        private const int tileRange = 12;

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

        public bool GetCollisionWithTiles(Vector2? offset = null, Predicate<TilemapTile> predicate = null) {
            List<TilemapTile> tilemapTilesWithinRange = entity.world.tilemap.GetTilesWithinRange(Tilemap.GetWorldToTilePosition(entity.position + new Vector2(tileRange % 2f == 0f ? (Game1.tileSize / 2f) : 0f)), tileRange);

            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            foreach (TilemapTile tilemapTile in tilemapTilesWithinRange) {
                if (tilemapTile.Tile.platform) {
                    if (entity.position.Y + box.Position.Y + box.height > tilemapTile.Y * Game1.tileSize) {
                        continue;
                    }
                }

                if (predicate?.Invoke(tilemapTile) ?? true) {
                    if (boxOffset.GetIntersects(tilemapTile.GetBox())) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetCollisionWithTiles(out List<TilemapTile> tilemapTiles, Vector2? offset = null, Predicate<TilemapTile> predicate = null) {
            tilemapTiles = new List<TilemapTile>();

            List<TilemapTile> tilemapTilesWithinRange = entity.world.tilemap.GetTilesWithinRange(Tilemap.GetWorldToTilePosition(entity.position + new Vector2(tileRange % 2f == 0f ? (Game1.tileSize / 2f) : 0f)), tileRange);

            Box boxOffset = new Box(entity.position + box.Position + (offset ?? Vector2.Zero), box.Size);

            foreach (TilemapTile tilemapTile in tilemapTilesWithinRange) {
                if (tilemapTile.Tile.platform) {
                    if (entity.position.Y + box.Position.Y + box.height > tilemapTile.Y * Game1.tileSize) {
                        continue;
                    }
                }

                if (predicate?.Invoke(tilemapTile) ?? true) {
                    if (boxOffset.GetIntersects(tilemapTile.GetBox())) {
                        tilemapTiles.Add(tilemapTile);
                    }
                }
            }

            return tilemapTiles.Count > 0;
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

                List<TilemapTile> tilemapTilesWithinRange = entity.world.tilemap.GetTilesWithinRange(Tilemap.GetWorldToTilePosition(positionTo + new Vector2(tileRange % 2f == 0f ? (Game1.tileSize / 2f) : 0f)), tileRange);

                foreach (TilemapTile tilemapTile in tilemapTilesWithinRange) {
                    if (new Box(positionTo + box.Position, box.Size).GetIntersects(tilemapTile.GetBox())) {
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