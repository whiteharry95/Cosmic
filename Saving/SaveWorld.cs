namespace Cosmic.Saving {
    using Cosmic.Universes;
    using System;

    [Serializable]
    public class SaveWorld {
        public SaveTileMap tileMap;
        public SaveTileMap tileMapWalls;

        public float fallSpeed;
        public float fallSpeedMax;

        /*public World GetAsWorld() {
            World world = new World(tileMap.tiles.GetLength(0), tileMap.tiles.GetLength(1));
            world.tileMap = tileMap;
            world.tileMapWalls = tileMapWalls;
        }*/
    }
}