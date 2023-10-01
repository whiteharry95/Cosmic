namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public static class TileManager {
        private static Tile[] tiles;

        public static Dirt dirt;
        public static Stone stone;
        public static WoodenPlatform woodenPlatform;

        public static void Init() {
            tiles = new Tile[3];

            dirt = AddTile<Dirt>(0);
            stone = AddTile<Stone>(1);
            woodenPlatform = AddTile<WoodenPlatform>(2);
        }

        public static void Generate() {
            for (int i = 0; i < tiles.Length; i++) {
                tiles[i].Generate();
            }
        }

        public static T AddTile<T>(int id) where T : Tile {
            tiles[id] = Activator.CreateInstance<T>();
            tiles[id].id = id;

            return (T)tiles[id];
        }

        public static Texture2D CreateTileTexture(Color colour) {
            Texture2D texture = new Texture2D(Game1.graphicsDeviceStatic, Game1.tileSize, Game1.tileSize);
            Color[] textureData = new Color[Game1.tileSize * Game1.tileSize];

            AssetManager.tile.GetData(textureData);

            for (int i = 0; i < textureData.Length; i++) {
                if (textureData[i].R != 0 && textureData[i].G != 0 && textureData[i].B != 0) {
                    textureData[i] = colour;
                }
            }

            texture.SetData(textureData);

            Game1.texturesToUnload.Add(texture);

            return texture;
        }
    }
}