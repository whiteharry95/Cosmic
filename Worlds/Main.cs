namespace Cosmic.Worlds {
    using Cosmic.Tiles;

    public class Main : World {
        public Main(int tilemapWidth, int tilemapHeight) : base(tilemapWidth, tilemapHeight) {
            generationActions.Add(() => {
                int surfaceLevel = (int)((tilemap.Height / 3f) * 2f);

                for (int y = surfaceLevel; y < tilemap.Height; y++) {
                    for (int x = 0; x < tilemap.Width; x++) {
                        tilemapWalls.tiles[x, y] = new TilemapTile(TileManager.dirt, tilemapWalls, x, y);
                        tilemap.tiles[x, y] = new TilemapTile(TileManager.dirt, tilemap, x, y);
                    }
                }
            });
        }
    }
}