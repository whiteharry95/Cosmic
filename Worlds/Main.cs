namespace Cosmic.Worlds {
    using Cosmic.Tiles;
    using Microsoft.Xna.Framework;

    public class Main : World {
        public Main(int tilemapWidth, int tilemapHeight) : base(tilemapWidth, tilemapHeight) {
            generationActions.Add(() => {
                for (int y = 0; y < tilemap.Height; y++) {
                    for (int x = 0; x < tilemap.Width; x++) {
                        tilemapWalls.tiles[x, y] = new TilemapTile(TileManager.dirt, tilemapWalls, x, y);
                        tilemap.tiles[x, y] = new TilemapTile(TileManager.dirt, tilemap, x, y);
                    }
                }
            });

            generationActions.Add(() => {
                Point holePosition = new Point(tilemap.Width / 2, tilemap.Height / 2);
                int holeRadius = 32;

                for (int y = holePosition.Y - holeRadius; y <= holePosition.Y + holeRadius; y++) {
                    if (y < 0 || y >= tilemap.Height) {
                        continue;
                    }

                    for (int x = holePosition.X - holeRadius; x <= holePosition.X + holeRadius; x++) {
                        if (x < 0 || x >= tilemap.Width) {
                            continue;
                        }

                        if (Vector2.Distance(holePosition.ToVector2() - new Vector2(0.5f), new Vector2(x, y)) <= holeRadius) {
                            tilemap.tiles[x, y] = null;
                        }
                    }
                }
            });
        }
    }
}