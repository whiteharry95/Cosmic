namespace Cosmic.Assets {
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class FontManager {
        public static SpriteFont ArialSmall;
        public static SpriteFont ArialMedium;

        public static void Load(ContentManager contentManager) {
            ArialSmall = contentManager.Load<SpriteFont>("Fonts/ArialSmall");
            ArialMedium = contentManager.Load<SpriteFont>("Fonts/ArialMedium");
        }
    }
}