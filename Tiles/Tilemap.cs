namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Worlds;
    using System;
    using System.Collections.Generic;

    public class Tilemap {
        public int Width => tiles.GetLength(0);
        public int Height => tiles.GetLength(1);

        public TilemapTile[,] tiles;
        public float tilesBrightness = 1f;

        public World world;

        private Sprite tileLifeSprite = new Sprite(AssetManager.tileLife0, AssetManager.tileLife1, AssetManager.tileLife2, AssetManager.tileLife3);

        public Tilemap(int width, int height, World world) {
            tiles = new TilemapTile[width, height];
            this.world = world;
        }

        public void Update(GameTime gameTime) {
            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    tiles[x, y]?.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime) {
            Box cameraBox = Camera.GetBox();
            Rectangle cameraTileRectangle = new Rectangle((int)Math.Floor(cameraBox.x / Game1.tileSize), (int)Math.Floor(cameraBox.y / Game1.tileSize), (int)Math.Ceiling(cameraBox.width / Game1.tileSize) + 1, (int)Math.Ceiling(cameraBox.height / Game1.tileSize) + 1);

            for (int y = Math.Max(cameraTileRectangle.Top, 0); y < Math.Min(cameraTileRectangle.Bottom, Height); y++) {
                for (int x = Math.Max(cameraTileRectangle.Left, 0); x < Math.Min(cameraTileRectangle.Right, Width); x++) {
                    if (tiles[x, y] != null) {
                        Game1.spriteBatch.Draw(tiles[x, y].Tile.texture, new Vector2(x, y) * Game1.tileSize - Camera.position, new Color(tilesBrightness, tilesBrightness, tilesBrightness));

                        if (tiles[x, y].Life < tiles[x, y].Tile.life) {
                            tileLifeSprite.DrawIndex((int)((1f - (float)tiles[x, y].Life / tiles[x, y].Tile.life) * tileLifeSprite.textures.Length), new Vector2(x, y) * Game1.tileSize - Camera.position, 0f, 1f, 1f);
                        }
                    }
                }
            }
        }

        public TilemapTile GetTile(int x, int y) {
            return x >= 0 && y >= 0 && x < Width && y < Height ? tiles[x, y] : null;
        }

        public TilemapTile GetTile(Point position) {
            return GetTile(position.X, position.Y);
        }

        public bool GetTileCollisionWithEntity<T>(int x, int y) where T : Entity {
            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (GetTile(x, y).GetBox().GetIntersects(entity.collider.GetBoxReal())) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetTileCollisionWithEntity<T>(Point position) where T : Entity {
            return GetTileCollisionWithEntity<T>(position.X, position.Y);
        }

        public bool GetTilePositionCollisionWithEntity<T>(int x, int y) where T : Entity {
            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (new Box(x * Game1.tileSize, y * Game1.tileSize, Game1.tileSize, Game1.tileSize).GetIntersects(entity.collider.GetBoxReal())) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetTilePositionCollisionWithEntity<T>(Point position) where T : Entity {
            return GetTilePositionCollisionWithEntity<T>(position.X, position.Y);
        }

        public List<TilemapTile> GetTilesWithinRange(int cx, int cy, int range, Predicate<TilemapTile> predicate = null) {
            List<TilemapTile> _tiles = new List<TilemapTile>();

            for (int y = cy - range; y <= cy + range; y++) {
                for (int x = cx - range; x <= cx + range; x++) {
                    TilemapTile tile = GetTile(x, y);

                    if (tile != null) {
                        if (predicate?.Invoke(tile) ?? true) {
                            _tiles.Add(tile);
                        }
                    }
                }
            }

            return _tiles;
        }

        public List<TilemapTile> GetTilesWithinRange(Point centrePosition, int range, Predicate<TilemapTile> predicate = null) {
            return GetTilesWithinRange(centrePosition.X, centrePosition.Y, range, predicate);
        }

        public static Point GetWorldToTilePosition(Vector2 position) {
            return new Point((int)Math.Floor(position.X / Game1.tileSize), (int)Math.Floor(position.Y / Game1.tileSize));
        }

        public static Vector2 GetTileToWorldPosition(Point position) {
            return new Vector2(position.X * Game1.tileSize, position.Y * Game1.tileSize);
        }
    }
}