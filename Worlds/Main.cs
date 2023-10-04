namespace Cosmic.Worlds {
    using Cosmic.TileMap;
    using Cosmic.Tiles;

    public class Main : World {
        public Main(int tileMapWidth, int tileMapHeight) : base(tileMapWidth, tileMapHeight) {
            generationActions.Add(() => {
                for (int y = (int)(tileMap.Height * (2f / 3f)); y < tileMapHeight; y++) {
                    for (int x = 0; x < tileMapWidth; x++) {
                        tileMap.tiles[x, y] = new TileMapTile(TileManager.dirt, tileMap, x, y);
                        tileMapWalls.tiles[x, y] = new TileMapTile(TileManager.dirt, tileMapWalls, x, y);
                    }
                }
            });
        }
    }
}