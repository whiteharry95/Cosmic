namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;

    public class Cursor : UIElement {
        private Sprite cursorSprite = new Sprite(AssetManager.cursor, new Vector2(AssetManager.cursor.Width, AssetManager.cursor.Height) / 2f);

        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            cursorSprite.Draw(InputManager.mouseState.Position.ToVector2(), 0f, 1f, 1f);
        }
    }
}