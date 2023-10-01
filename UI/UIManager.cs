namespace Cosmic.UI {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.UI.UIElements;
    using System.Collections.Generic;

    public static class UIManager {
        public static List<UIElement> uIElements;

        public static TileSelection tileSelections;
        public static CharacterHealthBars characterHealthBars;
        public static DamageText damageTexts;
        public static PlayerInventory playerInventory;
        public static Cursor cursor;

        public static void Load(ContentManager contentManager) {
            uIElements = new List<UIElement> {
                (tileSelections = new TileSelection()),
                (characterHealthBars = new CharacterHealthBars()),
                (damageTexts = new DamageText()),
                (playerInventory = new PlayerInventory()),
                (cursor = new Cursor())
            };
        }

        public static void Update(GameTime gameTime) {
            foreach (UIElement uIElement in uIElements) {
                uIElement.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime) {
            foreach (UIElement uIElement in uIElements) {
                uIElement.Draw(gameTime);
            }
        }
    }
}