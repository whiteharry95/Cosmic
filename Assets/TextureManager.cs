namespace Cosmic.Assets {
    using Cosmic.Tiles;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class TextureManager {
        public static Texture2D Pixel;

        public static Texture2D Characters_Player;
        public static Texture2D Characters_NPCs_Shooters_GreenShooter;

        public static TextureSheet Tiles_Dirt;
        public static TextureSheet Tiles_Stone;

        public static TextureSheet Tiles_TileLife;

        public static Texture2D WorldObjects_Rock;
        public static Texture2D WorldObjects_Chandelier;

        public static Texture2D Projectiles_Bullets_CopperBullet;

        public static Texture2D UI_Cursor;
        public static Texture2D UI_InventorySlot;

        public static Texture2D Entities_Hitbox;

        public static Texture2D Items_Guns_MachineGun;
        public static Texture2D Items_Drills_CopperDrill;
        public static Texture2D Items_Swords_WoodenSword;
        public static Texture2D Items_WorldTeleporter;
        public static Texture2D Items_WorldBuilder;

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
    }
}