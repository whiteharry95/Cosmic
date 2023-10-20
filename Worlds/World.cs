namespace Cosmic.Worlds {
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;
    using Microsoft.Xna.Framework;

    public class World {
        public float Width => tileMap.Width * Tile.Size;
        public float Height => tileMap.Height * Tile.Size;

        public Vector2 Size => new Vector2(Width, Height);

        public TileMap tileMap;
        public TileMap tileMapWalls;

        public List<Action> generationActions = new List<Action>();

        public float fallSpeed = 0.25f;
        public float fallSpeedMax = 16f;

        public virtual void Load(int tileMapWidth, int tileMapHeight) {
            tileMap = new TileMap(tileMapWidth, tileMapHeight, this);
            tileMapWalls = new TileMap(tileMapWidth, tileMapHeight, this, 0.4f);

            generationActions.Add(() => {
                int dirtLevel = (int)(tileMap.Height * (2f / 3f));
                int stoneLevel = (int)(tileMap.Height * (3f / 4f));

                for (int y = dirtLevel; y < tileMap.Height; y++) {
                    for (int x = 0; x < tileMap.Width; x++) {
                        Tile tile = y >= stoneLevel ? TileManager.stone : TileManager.dirt;

                        tileMap.PlaceTile(x, y, tile, false);
                        tileMapWalls.PlaceTile(x, y, tile, false);
                    }
                }
            });

            generationActions.Add(() => {
                for (int y = 0; y < tileMap.Height; y++) {
                    for (int x = 0; x < tileMap.Width; x++) {
                        tileMap.RefreshTileTextureIndex(x, y);
                        tileMapWalls.RefreshTileTextureIndex(x, y);
                    }
                }
            });
        }

        public virtual void Update() {
        }

        public virtual void Draw() {
            tileMapWalls.Draw();
            tileMap.Draw();
        }

        public virtual void Generate() {
            foreach (Action generationAction in generationActions) {
                generationAction();
            }
        }
    }
}