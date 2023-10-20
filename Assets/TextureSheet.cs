namespace Cosmic.Assets {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TextureSheet {
        public Texture2D textureSheet;

        public Texture2D[] textures;
        public int textureWidth, textureHeight;

        public TextureSheet(Texture2D textureSheet, int textureWidth, int textureHeight) {
            this.textureSheet = textureSheet;

            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;

            Color[] textureSheetData = new Color[textureSheet.Width * textureSheet.Height];
            textureSheet.GetData(textureSheetData);

            textures = new Texture2D[(textureSheet.Width / textureWidth) * (textureSheet.Height / textureHeight)];

            for (int i = 0; i < textures.Length; i++) {
                textures[i] = new Texture2D(Game1.graphicsDeviceStatic, textureWidth, textureHeight);
                Color[] textureData = new Color[textures[i].Width * textures[i].Height];

                for (int y = 0; y < textureHeight; y++) {
                    for (int x = 0; x < textureWidth; x++) {
                        textureData[(y * textureWidth) + x] = textureSheetData[(y * textureSheet.Width) + x + (textureWidth * i)];
                    }
                }

                textures[i].SetData(textureData);

                Game1.texturesToUnload.Add(textures[i]);
            }
        }
    }
}