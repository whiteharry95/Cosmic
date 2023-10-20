using Cosmic.Tiles;

namespace Cosmic.TileMap {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using System;
    using System.Collections.Generic;
    using Cosmic.Worlds;
    using Cosmic;
    using Cosmic.Assets;

    public class TileMap {
        public int Width => tiles.GetLength(0);
        public int Height => tiles.GetLength(1);

        private TileMapTile[,] tiles;
        private float tileBrightness;

        public World world;

        public TileMap(int width, int height, World world, float tileBrightness = 1f) {
            tiles = new TileMapTile[width, height];
            this.tileBrightness = tileBrightness;

            this.world = world;
        }

        public void Draw() {
            Rectangle cameraTileRectangle = new Rectangle(new Point((int)Math.Floor(Camera.position.X / Tile.Size), (int)Math.Floor(Camera.position.Y / Tile.Size)), new Point((int)Math.Ceiling((float)Camera.Width / Tile.Size) + 1, (int)Math.Ceiling((float)Camera.Height / Tile.Size) + 1));

            for (int y = cameraTileRectangle.Top; y < cameraTileRectangle.Bottom; y++) {
                for (int x = cameraTileRectangle.Left; x < cameraTileRectangle.Right; x++) {
                    if (GetTile(x, y) != null) {
                        Game1.spriteBatch.Draw(tiles[x, y].tile.textureSheet.textures[tiles[x, y].textureIndex], new Vector2(x, y) * Tile.Size, new Color(tileBrightness, tileBrightness, tileBrightness));

                        if (tiles[x, y].life > 0 && tiles[x, y].life < tiles[x, y].tile.life) {
                            Game1.spriteBatch.Draw(TextureManager.Tiles_TileLife.textures[(int)((1f - (float)tiles[x, y].life / tiles[x, y].tile.life) * TextureManager.Tiles_TileLife.textures.Length)], new Vector2(x, y) * Tile.Size, Color.White);
                        }
                    }
                }
            }
        }

        public void PlaceTile(int x, int y, Tile tile, bool refreshTileTextureIndices = true) {
            if (x < 0 || y < 0 || x >= Width || y >= Height) {
                return;
            }

            if (tiles[x, y] != null) {
                return;
            }

            tiles[x, y] = new TileMapTile(tile, this, (ushort)x, (ushort)y);

            if (refreshTileTextureIndices) {
                for (int ry = y - 1; ry <= y + 1; ry++) {
                    for (int rx = x - 1; rx <= x + 1; rx++) {
                        RefreshTileTextureIndex(rx, ry);
                    }
                }
            }
        }

        public void DestroyTile(int x, int y, bool refreshTileTextureIndices = true) {
            if (x < 0 || y < 0 || x >= Width || y >= Height) {
                return;
            }

            tiles[x, y] = null;

            if (refreshTileTextureIndices) {
                for (int ry = y - 1; ry <= y + 1; ry++) {
                    for (int rx = x - 1; rx <= x + 1; rx++) {
                        RefreshTileTextureIndex(rx, ry);
                    }
                }
            }
        }

        public void RefreshTileTextureIndex(int x, int y) {
            if (x < 0 || y < 0 || x >= Width || y >= Height) {
                return;
            }

            if (tiles[x, y] == null) {
                return;
            }

            bool tileRight = GetTile(x + 1, y) != null;
            bool tileLeft = GetTile(x - 1, y) != null;
            bool tileDown = GetTile(x, y + 1) != null;
            bool tileUp = GetTile(x, y - 1) != null;

            tiles[x, y].textureIndex = 0;

            if (!tileRight && tileLeft && !tileDown && tileUp) {
                tiles[x, y].textureIndex = 1;
            }

            if (tileRight && !tileLeft && !tileDown && tileUp) {
                tiles[x, y].textureIndex = 2;
            }

            if (!tileRight && tileLeft && tileDown && !tileUp) {
                tiles[x, y].textureIndex = 3;
            }

            if (tileRight && !tileLeft && tileDown && !tileUp) {
                tiles[x, y].textureIndex = 4;
            }
        }

        public TileMapTile GetTile(int x, int y) {
            return (x >= 0 && y >= 0 && x < Width && y < Height) ? tiles[x, y] : null;
        }

        public bool GetTileCollisionWithEntity<T>(int x, int y, Predicate<T> predicate = null) where T : Entity {
            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (predicate?.Invoke((T)entity) ?? true) {
                        if (GetTile(x, y).GetPolygon().GetCollisionWithPolygon(entity.collider.polygon)) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool GetTilePositionCollisionWithEntity<T>(int x, int y, Predicate<T> predicate = null) where T : Entity {
            Polygon polygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y) * Tile.Size);

            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (predicate?.Invoke((T)entity) ?? true) {
                        if (polygon.GetCollisionWithPolygon(entity.collider.polygon)) {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public List<TileMapTile> GetTilesWithinRange(int cx, int cy, int range) {
            List<TileMapTile> tiles = new List<TileMapTile>();

            for (int y = cy - (int)Math.Floor(range / 2f); y < cy + Math.Ceiling(range / 2f); y++) {
                for (int x = cx - (int)Math.Floor(range / 2f); x < cx + Math.Ceiling(range / 2f); x++) {
                    TileMapTile tile = GetTile(x, y);

                    if (tile != null) {
                        tiles.Add(tile);
                    }
                }
            }

            return tiles;
        }

        public List<TileMapTile> GetTilesIntersectingPolygon(Polygon polygon) {
            List<TileMapTile> tiles = new List<TileMapTile>();

            Point topLeft = GetWorldToTilePosition(polygon.GetTopLeft());
            Point bottomRight = GetWorldToTilePosition(polygon.GetBottomRight());

            for (int y = topLeft.Y; y < bottomRight.Y; y++) {
                for (int x = topLeft.X; x < bottomRight.X; x++) {
                    TileMapTile tile = GetTile(x, y);

                    if (tile != null) {
                        if (tile.GetPolygon().GetCollisionWithPolygon(polygon)) {
                            tiles.Add(tile);
                        }
                    }
                }
            }

            return tiles;
        }

        public static Point GetWorldToTilePosition(Vector2 position) {
            return new Point((int)Math.Floor(position.X / Tile.Size), (int)Math.Floor(position.Y / Tile.Size));
        }

        public static Vector2 GetTileToWorldPosition(Point position) {
            return new Vector2(position.X * Tile.Size, position.Y * Tile.Size);
        }
    }
}