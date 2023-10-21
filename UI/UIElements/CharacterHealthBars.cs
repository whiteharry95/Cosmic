namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using System.Collections.Generic;
    using System.Linq;
    using Cosmic.Assets;

    public class CharacterHealthBars : UIElement {
        public override void Update() {
        }

        public override void Draw() {
            List<Character> characters = EntityManager.GetEntitiesInWorld().OfType<Character>().ToList();

            foreach (Character character in characters) {
                float healthBarOffset = 6f;

                float healthBarWidth = 16f;
                float healthBarHeight = 2f;

                Vector2 healthBarPosition = (character.position - Camera.Position + new Vector2(-healthBarWidth / 2f, -character.sprite.origin.Y + character.sprite.mask.Bottom + healthBarOffset)) * Camera.Scale;

                Game1.SpriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.Black, 0f, Vector2.Zero, new Vector2(healthBarWidth, healthBarHeight) * Camera.Scale, SpriteEffects.None, 0f);
                Game1.SpriteBatch.Draw(TextureManager.Pixel, healthBarPosition, null, Color.White, 0f, Vector2.Zero, new Vector2(healthBarWidth * ((float)character.health / character.healthMax), healthBarHeight) * Camera.Scale, SpriteEffects.None, 0f);
            }
        }
    }
}