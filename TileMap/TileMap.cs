using Cosmic.Tiles;

namespace Cosmic.TileMap {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using System;
    using System.Collections.Generic;
    using Cosmic.Universes;
    using Cosmic;
    using Cosmic.Assets;
    using Cosmic.Saving;

    public class TileMap {
        public int Width => tiles.GetLength(0);
        public int Height => tiles.GetLength(1);

        public TileMapTile[,] tiles;
        public float tileBrightness = 1f;

        public World world;

        public TileMap(int width, int height, World world) {
            tiles = new TileMapTile[width, height];
            this.world = world;
        }

        public void Draw(GameTime gameTime) {
            //Rectangle cameraRectangle = Camera.GetRectangle();
            //Rectangle cameraTileRectangle = new Rectangle((int)Math.Floor(cameraRectangle.X / Tile.Size), (int)Math.Floor(cameraRectangle.y / Tile.Size), (int)Math.Ceiling(cameraRectangle.width / Tile.Size) + 1, (int)Math.Ceiling(cameraRectangle.height / Tile.Size) + 1);

            /*for (int y = Math.Max(cameraTileRectangle.Top, 0); y < Math.Min(cameraTileRectangle.Bottom, Height); y++) {
                for (int x = Math.Max(cameraTileRectangle.Left, 0); x < Math.Min(cameraTileRectangle.Right, Width); x++) {
                    if (tiles[x, y] != null) {
                        Game1.spriteBatch.Draw(tiles[x, y].tile.texture, (new Vector2(x, y) * Tile.Size) - Camera.position, new Microsoft.Xna.Framework.Color(tileBrightness, tileBrightness, tileBrightness));

                        if (tiles[x, y].life < tiles[x, y].tile.life) {
                            Game1.spriteBatch.Draw(TextureManager.Tiles_TileLife[(int)((1f - (float)tiles[x, y].life / tiles[x, y].tile.life) * TextureManager.Tiles_TileLife.Length)], (new Vector2(x, y) * Tile.Size) - Camera.position, Microsoft.Xna.Framework.Color.White);
                        }
                    }
                }
            }*/
        }

        public SaveTileMap GetAsSaveTileMap() {
            SaveTileMap saveTileMap = new SaveTileMap();
            saveTileMap.tiles = new ushort[Width, Height];

            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    saveTileMap.tiles[x, y] = tiles[x, y].tile.id;
                }
            }

            return saveTileMap;
        }

        public TileMapTile GetTile(int x, int y) {
            return (x >= 0 && y >= 0 && x < Width && y < Height) ? tiles[x, y] : null;
        }

        public TileMapTile GetTile(Point position) {
            return GetTile(position.X, position.Y);
        }

        public bool GetTileCollisionWithEntity<T>(int wx, int wy) where T : Entity {
            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (GetTile(wx, wy).GetPolygon().GetCollisionWithPolygon(entity.collider.polygon)) {
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
            Polygon polygon = new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y));

            foreach (Entity entity in EntityManager.entities) {
                if (entity is T) {
                    if (polygon.GetCollisionWithPolygon(entity.collider.polygon)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GetTilePositionCollisionWithEntity<T>(Point position) where T : Entity {
            return GetTilePositionCollisionWithEntity<T>(position.X, position.Y);
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

        public List<TileMapTile> GetTilesWithinRange(Microsoft.Xna.Framework.Point centrePosition, int range) {
            return GetTilesWithinRange(centrePosition.X, centrePosition.Y, range);
        }

        public static Point GetWorldToTilePosition(Vector2 position) {
            return new Point((int)Math.Floor(position.X / Tile.Size), (int)Math.Floor(position.Y / Tile.Size));
        }

        public static Vector2 GetTileToWorldPosition(Point position) {
            return new Vector2(position.X * Tile.Size, position.Y * Tile.Size);
        }
    }
}