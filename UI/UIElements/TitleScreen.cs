namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Cosmic.Utilities;
    using System.Collections.Generic;

    public class TitleScreen : UIElement {
        public List<Save> saves;

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            foreach (Save save in saves) {
                DrawUtilities.DrawString(AssetManager.arial, new DrawUtilities.Text(save.name), new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight), Color.White, DrawUtilities.AlignmentHor.Middle, DrawUtilities.AlignmentVer.Middle);
            }
        }
    }
}