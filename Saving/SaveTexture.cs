namespace Cosmic.Saving {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SaveTexture {
        public Color[] pixels;

        public int width;
        public int height;

        public Texture2D LoadTexture() {
            Texture2D texture = new Texture2D(Game1.graphicsDeviceStatic, width, height);
            Color[] textureData = new Color[width * height];

            for (int i = 0; i < textureData.Length; i++) {
                textureData[i] = new Color(pixels[i].R, pixels[i].G, pixels[i].B);
            }

            return texture;
        }
    }
}