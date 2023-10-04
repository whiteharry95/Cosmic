﻿namespace Cosmic.UI.UIElements {
    using Cosmic.Assets;
    using Cosmic.Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class PlayerBars : UIElement {
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            Vector2 healthBarPosition = new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 12f;
            Vector2 healthBarSize = new Vector2(384f, 32f);

            Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.Black, 0f, Vector2.Zero, new Vector2(healthBarSize.X, healthBarSize.Y), SpriteEffects.None, 0f);
            Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.White, 0f, Vector2.Zero, new Vector2(healthBarSize.X * ((float)EntityManager.player.health / EntityManager.player.healthMax), healthBarSize.Y), SpriteEffects.None, 0f);
        }
    }
}