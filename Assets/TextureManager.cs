namespace Cosmic.Assets {
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class TextureManager {
        public static Texture2D Pixel;

        public static Texture2D Characters_Player;
        public static Texture2D Characters_NPCs_Shooters_GreenShooter;

        public static Texture2D Tiles_Dirt;
        public static Texture2D Tiles_Stone;

        public static Texture2D[] Tiles_TileLife;

        public static Texture2D Textures_Projectiles_Bullets_CopperBullet;

        public static Texture2D UI_Cursor;
        public static Texture2D UI_TileSelection;
        public static Texture2D UI_InventorySlot;

        public static Texture2D Entities_ItemDrop;
        public static Texture2D Entities_Hitbox;

        public static Texture2D Items_Guns_MachineGun;
        public static Texture2D Items_Drills_CopperDrill;
        public static Texture2D Items_Swords_WoodenSword;

        public static void Load(ContentManager contentManager) {
            Pixel = contentManager.Load<Texture2D>("Textures/Pixel");

            Characters_Player = contentManager.Load<Texture2D>("Textures/Characters/Player");
            Characters_NPCs_Shooters_GreenShooter = contentManager.Load<Texture2D>("Textures/Characters/NPCs/Shooters/GreenShooter");

            Tiles_Dirt = contentManager.Load<Texture2D>("Textures/Tiles/Dirt");
            Tiles_Stone = contentManager.Load<Texture2D>("Textures/Tiles/Stone");

            Tiles_TileLife = new Texture2D[4] {
                contentManager.Load<Texture2D>("Textures/Tiles/TileLife0"),
                contentManager.Load<Texture2D>("Textures/Tiles/TileLife1"),
                contentManager.Load<Texture2D>("Textures/Tiles/TileLife2"),
                contentManager.Load<Texture2D>("Textures/Tiles/TileLife3")
            };

            Textures_Projectiles_Bullets_CopperBullet = contentManager.Load<Texture2D>("Textures/Projectiles/Bullets/CopperBullet");

            UI_Cursor = contentManager.Load<Texture2D>("Textures/UI/Cursor");
            UI_InventorySlot = contentManager.Load<Texture2D>("Textures/UI/InventorySlot");
            UI_TileSelection = contentManager.Load<Texture2D>("Textures/UI/TileSelection");

            Entities_ItemDrop = contentManager.Load<Texture2D>("Textures/Entities/ItemDrop");
            Entities_Hitbox = contentManager.Load<Texture2D>("Textures/Entities/Hitbox");

            Items_Guns_MachineGun = contentManager.Load<Texture2D>("Textures/Items/Guns/MachineGun");
            Items_Drills_CopperDrill = contentManager.Load<Texture2D>("Textures/Items/Drills/CopperDrill");
            Items_Swords_WoodenSword = contentManager.Load<Texture2D>("Textures/Items/Swords/WoodenSword");
        }
    }
}