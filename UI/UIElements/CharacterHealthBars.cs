namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using System.Collections.Generic;
    using System.Linq;
    using Cosmic.Assets;

    public class CharacterHealthBars : UIElement {
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            List<Character> characters = EntityManager.GetEntitiesInWorld().OfType<Character>().ToList();

            foreach (Character character in characters) {
                float healthBarOffset = 12f;

                float healthBarWidth = 32f;
                float healthBarHeight = 4f;

                Vector2 healthBarPosition = character.position - Camera.position + new Vector2(-healthBarWidth / 2f, -character.sprite.origin.Y + character.sprite.texture.Height + healthBarOffset);

                Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.Black, 0f, Vector2.Zero, new Vector2(healthBarWidth, healthBarHeight), SpriteEffects.None, 0f);
                Game1.spriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.White, 0f, Vector2.Zero, new Vector2(healthBarWidth * ((float)character.health / character.healthMax), healthBarHeight), SpriteEffects.None, 0f);
            }
        }
    }
}