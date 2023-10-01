namespace Cosmic.Worlds {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class World {
        public int Width => tilemap.Width * Game1.tileSize;
        public int Height => tilemap.Height * Game1.tileSize;

        public Tilemap tilemap;
        public Tilemap tilemapWalls;

        public List<Action> generationActions = new List<Action>();

        public float fallSpeed = 0.5f;
        public float fallSpeedMax = 32f;

        public World(int tilemapWidth, int tilemapHeight) {
            tilemap = new Tilemap(tilemapWidth, tilemapHeight, this);

            tilemapWalls = new Tilemap(tilemapWidth, tilemapHeight, this);
            tilemapWalls.tilesBrightness = 0.4f;
        }

        public virtual void Update(GameTime gameTime) {
            tilemapWalls.Update(gameTime);
            tilemap.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime) {
            tilemapWalls.Draw(gameTime);
            tilemap.Draw(gameTime);
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