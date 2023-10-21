namespace Cosmic.UI {
    using Cosmic.UI.UIElements;
    using System.Collections.Generic;

    public static class UIManager {
        public static int Width => Game1.GraphicsDeviceManager.PreferredBackBufferWidth;
        public static int Height => Game1.GraphicsDeviceManager.PreferredBackBufferHeight;

        public static List<UIElement> uIElements;

        public static TileSelection tileSelections;
        public static CharacterHealthBars characterHealthBars;
        public static DamageText damageTexts;
        public static PlayerInventory playerInventory;
        public static PlayerBars playerBars;
        public static Cursor cursor;

        public static void Load() {
            uIElements = new List<UIElement> {
                (tileSelections = new TileSelection()),
                (characterHealthBars = new CharacterHealthBars()),
                (damageTexts = new DamageText()),
                (playerBars = new PlayerBars()),
                (playerInventory = new PlayerInventory()),
                (cursor = new Cursor())
            };
        }

        public static void EarlyUpdate() {
            foreach (UIElement uIElement in uIElements) {
                uIElement.EarlyUpdate();
            }
        }

        public static void Update() {
            foreach (UIElement uIElement in uIElements) {
                uIElement.Update();
            }
        }

        public static void Draw() {
            foreach (UIElement uIElement in uIElements) {
                uIElement.Draw();
            }
        }
    }
}