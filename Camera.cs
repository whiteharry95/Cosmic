namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Cosmic.UI;
    using System;
    using Cosmic.Utilities;

    public static class Camera {
        public static int Width => Game1.graphicsDeviceManager.PreferredBackBufferWidth;
        public static int Height => Game1.graphicsDeviceManager.PreferredBackBufferHeight;

        public static Vector2 Size => new Vector2(Width, Height);

        public static Vector2 position;

        public static bool lookSet;
        public static Vector2 lookPosition;
        public static float lookDistance = 64f;
        public static float lookSpeed = 0.25f;

        public static void Update(GameTime gameTime) {
            if (Game1.server.netPlayers[0]?.player?.GetExists() ?? false) {
                lookPosition = Game1.server.netPlayers[0].player.position - (new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 2f);

                if (!UIManager.playerInventory.open) {
                    lookPosition += MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - Game1.server.netPlayers[0].player.position) * lookDistance * Math.Min(Vector2.Distance(InputManager.GetMousePosition(), Game1.server.netPlayers[0].player.position) / (lookDistance * 4f), 1f);
                }
            }

            if (!lookSet) {
                position = lookPosition;
                lookSet = true;
            }

            position += (lookPosition - position) * lookSpeed;
        }

        public static Rectangle GetRectangle() {
            return new Rectangle(position.ToPoint(), Size.ToPoint());
        }
    }
}