namespace Cosmic.Universes {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;
    using Cosmic.Saving;

    [Serializable]
    public class World {
        public float Width => tileMap.Width * Tile.Size;
        public float Height => tileMap.Height * Tile.Size;

        [NonSerialized] public TileMap tileMap;
        [NonSerialized] public TileMap tileMapWalls;

        [NonSerialized] public List<Action> generationActions = new List<Action>();

        public float fallSpeed = 0.5f;
        public float fallSpeedMax = 32f;

        public World(int width, int height) {
            tileMap = new TileMap(width, height, this);

            tileMapWalls = new TileMap(width, height, this);
            tileMapWalls.tileBrightness = 0.4f;
        }

        public void Draw(GameTime gameTime) {
            tileMapWalls.Draw(gameTime);
            tileMap.Draw(gameTime);
        }

        public void Generate() {
            foreach (Action generationAction in generationActions) {
                generationAction();
            }
        }

        public SaveWorld GetAsSaveWorld() {
            SaveWorld saveWorld = new SaveWorld();

            saveWorld.tileMap = tileMap.GetAsSaveTileMap();
            saveWorld.tileMapWalls = tileMapWalls.GetAsSaveTileMap();

            saveWorld.fallSpeed = fallSpeed;
            saveWorld.fallSpeedMax = fallSpeedMax;

            return saveWorld;
        }
    }
}