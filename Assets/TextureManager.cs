namespace Cosmic.Assets {
    using Cosmic.Tiles;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public static class TextureManager {
        public static List<Texture2D> TexturesToDispose { get; private set; } = new List<Texture2D>();

        public static Texture2D Pixel { get; private set; }

        public static Texture2D Characters_Player { get; private set; }
        public static Texture2D Characters_NPCs_Shooters_GreenShooter { get; private set; }

        public static TextureSheet Tiles_Dirt { get; private set; }
        public static TextureSheet Tiles_Stone { get; private set; }
        public static TextureSheet Tiles_TileLife { get; private set; }

        public static Texture2D WorldObjects_Rock { get; private set; }
        public static Texture2D WorldObjects_Chandelier { get; private set; }

        public static Texture2D Projectiles_Bullets_CopperBullet { get; private set; }

        public static Texture2D UI_Cursor { get; private set; }
        public static Texture2D UI_InventorySlot { get; private set; }

        public static Texture2D Entities_Hitbox { get; private set; }

        public static Texture2D Items_Guns_MachineGun { get; private set; }
        public static Texture2D Items_Drills_CopperDrill { get; private set; }
        public static Texture2D Items_Swords_WoodenSword { get; private set; }
        public static Texture2D Items_WorldTeleporter { get; private set; }
        public static Texture2D Items_WorldBuilder { get; private set; }

        public static void Load(ContentManager contentManager) {
            Pixel = contentManager.Load<Texture2D>("Textures/Pixel");

            Characters_Player = contentManager.Load<Texture2D>("Textures/Characters/Player");
            Characters_NPCs_Shooters_GreenShooter = contentManager.Load<Texture2D>("Textures/Characters/NPCs/Shooters/GreenShooter");

            Tiles_Dirt = new TextureSheet(contentManager.Load<Texture2D>("Textures/Tiles/Dirt"), Tile.Size, Tile.Size);
            Tiles_Stone = new TextureSheet(contentManager.Load<Texture2D>("Textures/Tiles/Stone"), Tile.Size, Tile.Size);
            Tiles_TileLife = new TextureSheet(contentManager.Load<Texture2D>("Textures/Tiles/TileLife"), Tile.Size, Tile.Size);

            WorldObjects_Rock = contentManager.Load<Texture2D>("Textures/WorldObjects/Rock");
            WorldObjects_Chandelier = contentManager.Load<Texture2D>("Textures/WorldObjects/Chandelier");

            Projectiles_Bullets_CopperBullet = contentManager.Load<Texture2D>("Textures/Projectiles/Bullets/CopperBullet");

            UI_Cursor = contentManager.Load<Texture2D>("Textures/UI/Cursor");
            UI_InventorySlot = contentManager.Load<Texture2D>("Textures/UI/InventorySlot");

            Entities_Hitbox = contentManager.Load<Texture2D>("Textures/Entities/Hitbox");

            Items_Guns_MachineGun = contentManager.Load<Texture2D>("Textures/Items/Guns/MachineGun");
            Items_Drills_CopperDrill = contentManager.Load<Texture2D>("Textures/Items/Drills/CopperDrill");
            Items_Swords_WoodenSword = contentManager.Load<Texture2D>("Textures/Items/Swords/WoodenSword");
            Items_WorldTeleporter = contentManager.Load<Texture2D>("Textures/Items/WorldTeleporter");
            Items_WorldBuilder = contentManager.Load<Texture2D>("Textures/Items/WorldBuilder");
        }

        public static void Unload() {
            foreach (Texture2D texture in TexturesToDispose) {
                texture.Dispose();
            }
        }
    }
}