namespace Cosmic.Worlds {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;

    public class World {
        public int Width => tileMap.Width * Tile.Size;
        public int Height => tileMap.Height * Tile.Size;

        public TileMap tileMap;
        public TileMap tileMapWalls;

        public List<Action> generationActions = new List<Action>();

        public float fallSpeed = 0.5f;
        public float fallSpeedMax = 32f;

        public World(int width, int height) {
            tileMap = new TileMap(width, height, this);

            tileMapWalls = new TileMap(width, height, this);
            tileMapWalls.tileBrightness = 0.4f;
        }

        public virtual void Draw(GameTime gameTime) {
            tileMapWalls.Draw(gameTime);
            tileMap.Draw(gameTime);
        }

        public void Generate() {
            foreach (Action generationAction in generationActions) {
                generationAction();
            }
        }

        public Box GetBox() {
            return new Box(Vector2.Zero, new Vector2(Width, Height));
        }
    }
}