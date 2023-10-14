namespace Cosmic.Tiles {
    using System;
    using System.Collections.Generic;

    public static class TileManager {
        private static List<Tile> tiles;

        public static Dirt dirt;
        public static Stone stone;

        public static void Init() {
            tiles = new List<Tile>();

            dirt = AddTile<Dirt>();
            stone = AddTile<Stone>();
        }

        public static void Load() {
            foreach (Tile tile in tiles) {
                tile.Load();
            }
        }

        public static T AddTile<T>(bool load = false) where T : Tile {
            T tile = Activator.CreateInstance<T>();
            tile.id = (ushort)tiles.Count;

            if (load) {
                tile.Load();
            }

            tiles.Add(tile);

            return tile;
        }
    }
}