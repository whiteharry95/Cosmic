namespace Cosmic {
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class AssetManager {
        public static Texture2D pixel;
        public static Texture2D player;
        public static Texture2D shooter;
        public static Texture2D tile;
        public static Texture2D tileLife0;
        public static Texture2D tileLife1;
        public static Texture2D tileLife2;
        public static Texture2D tileLife3;
        public static Texture2D projectile;
        public static Texture2D cursor;
        public static Texture2D tileSelection;
        public static Texture2D inventorySlot;
        public static Texture2D itemDrop;
        public static Texture2D gun;
        public static Texture2D miner;
        public static Texture2D hammer;
        public static Texture2D sword;
        public static Texture2D hitbox;
        public static Texture2D shield;

        public static SpriteFont arial;
        public static SpriteFont arialSmall;

        public static void Load(ContentManager contentManager) {
            pixel = contentManager.Load<Texture2D>("Textures/Pixel");
            player = contentManager.Load<Texture2D>("Textures/Player");
            shooter = contentManager.Load<Texture2D>("Textures/Shooter");
            tile = contentManager.Load<Texture2D>("Textures/Tile");
            tileLife0 = contentManager.Load<Texture2D>("Textures/TileLife0");
            tileLife1 = contentManager.Load<Texture2D>("Textures/TileLife1");
            tileLife2 = contentManager.Load<Texture2D>("Textures/TileLife2");
            tileLife3 = contentManager.Load<Texture2D>("Textures/TileLife3");
            projectile = contentManager.Load<Texture2D>("Textures/Projectile");
            cursor = contentManager.Load<Texture2D>("Textures/Cursor");
            tileSelection = contentManager.Load<Texture2D>("Textures/TileSelection");
            inventorySlot = contentManager.Load<Texture2D>("Textures/InventorySlot");
            itemDrop = contentManager.Load<Texture2D>("Textures/ItemDrop");
            gun = contentManager.Load<Texture2D>("Textures/Gun");
            miner = contentManager.Load<Texture2D>("Textures/Miner");
            hammer = contentManager.Load<Texture2D>("Textures/Hammer");
            sword = contentManager.Load<Texture2D>("Textures/Sword");
            hitbox = contentManager.Load<Texture2D>("Textures/Hitbox");
            shield = contentManager.Load<Texture2D>("Textures/Shield");

            arial = contentManager.Load<SpriteFont>("Fonts/Arial");
            arialSmall = contentManager.Load<SpriteFont>("Fonts/ArialSmall");
        }
    }
}