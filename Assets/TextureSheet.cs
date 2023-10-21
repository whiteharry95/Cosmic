namespace Cosmic.Assets {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TextureSheet {
        public Texture2D TextureSheetTexture { get; private set; }

        public Texture2D[] Textures { get; private set; }

        public int TextureWidth { get; private set; }
        public int TextureHeight { get; private set; }

        public TextureSheet(Texture2D textureSheetTexture, int textureWidth, int textureHeight) {
            TextureSheetTexture = textureSheetTexture;

            TextureWidth = textureWidth;
            TextureHeight = textureHeight;

            Color[] textureSheetData = new Color[TextureSheetTexture.Width * TextureSheetTexture.Height];
            TextureSheetTexture.GetData(textureSheetData);

            Textures = new Texture2D[(TextureSheetTexture.Width / textureWidth) * (TextureSheetTexture.Height / textureHeight)];

            for (int i = 0; i < Textures.Length; i++) {
                Textures[i] = new Texture2D(Game1.GraphicsDevice, textureWidth, textureHeight);
                Color[] textureData = new Color[Textures[i].Width * Textures[i].Height];

                for (int y = 0; y < textureHeight; y++) {
                    for (int x = 0; x < textureWidth; x++) {
                        textureData[(y * textureWidth) + x] = textureSheetData[(y * TextureSheetTexture.Width) + x + (textureWidth * i)];
                    }
                }

                Textures[i].SetData(textureData);

                TextureManager.TexturesToDispose.Add(Textures[i]);
            }
        }
    }
}