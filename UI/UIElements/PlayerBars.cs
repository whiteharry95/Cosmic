namespace Cosmic.UI.UIElements {
    using Cosmic.Assets;
    using Cosmic.Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class PlayerBars : UIElement {
        public override void Update() {
        }

        public override void Draw() {
            if (!(EntityManager.player?.GetExists() ?? false)) {
                return;
            }

            Vector2 healthBarPosition = new Vector2(UIManager.Width, UIManager.Height) / 12f;
            Vector2 healthBarSize = new Vector2(256f, 24f);

            Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.Black, 0f, Vector2.Zero, new Vector2(healthBarSize.X, healthBarSize.Y), SpriteEffects.None, 0f);

            if (EntityManager.player.health > 0f) {
                Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.White, 0f, Vector2.Zero, new Vector2(healthBarSize.X * ((float)EntityManager.player.health / EntityManager.player.healthMax), healthBarSize.Y), SpriteEffects.None, 0f);
            }
        }
    }
}