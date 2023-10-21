namespace Cosmic.Worlds {
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class World {
        public float Width => TileMap.Width * Tile.Size;
        public float Height => TileMap.Height * Tile.Size;

        public WorldTileMap TileMap { get; private set; }
        public WorldTileMap TileMapWalls { get; private set; }

        public float FallSpeed { get; protected set; } = 0.25f;
        public float FallSpeedMax { get; protected set; } = 16f;

        public List<Action> GenerationActions { get; private set; } = new List<Action>();

        public virtual void Load(int tileMapWidth, int tileMapHeight) {
            TileMap = new WorldTileMap(tileMapWidth, tileMapHeight, this);
            TileMapWalls = new WorldTileMap(tileMapWidth, tileMapHeight, this, 0.4f);

            GenerationActions.Add(() => {
                int dirtLevel = (int)(TileMap.Height * (2f / 3f));
                int stoneLevel = (int)(TileMap.Height * (3f / 4f));

                for (int y = dirtLevel; y < TileMap.Height; y++) {
                    for (int x = 0; x < TileMap.Width; x++) {
                        Tile tile = y >= stoneLevel ? TileManager.stone : TileManager.dirt;

                        TileMap.PlaceTile(x, y, tile, false);
                        TileMapWalls.PlaceTile(x, y, tile, false);
                    }
                }
            });

            GenerationActions.Add(() => {
                for (int y = 0; y < TileMap.Height; y++) {
                    for (int x = 0; x < TileMap.Width; x++) {
                        TileMap.RefreshTileTextureIndex(x, y);
                        TileMapWalls.RefreshTileTextureIndex(x, y);
                    }
                }
            });
        }

        public virtual void Update() {
        }

        public virtual void Draw() {
            TileMapWalls.Draw();
            TileMap.Draw();
        }

        public virtual void Generate() {
            foreach (Action generationAction in GenerationActions) {
                generationAction();
            }
        }
    }
}