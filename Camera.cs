namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.UI;
    using System;

    public static class Camera {
        public static int Width => Game1.graphicsDeviceManager.PreferredBackBufferWidth;
        public static int Height => Game1.graphicsDeviceManager.PreferredBackBufferHeight;

        public static Vector2 Size => new Vector2(Width, Height);

        public static Vector2 position;

        public static Vector2 lookPosition;
        public static float lookDistance = 64f;
        public static float lookSpeed = 0.25f;

        public static void Update(GameTime gameTime) {
            if (EntityManager.player?.GetExists() ?? false) {
                lookPosition = EntityManager.player.position - (new Vector2(Game1.graphicsDeviceManager.PreferredBackBufferWidth, Game1.graphicsDeviceManager.PreferredBackBufferHeight) / 2f);

                if (!UIManager.playerInventory.open) {
                    lookPosition += Vector2.Normalize(InputManager.GetMousePosition() - EntityManager.player.position) * lookDistance * Math.Min(Vector2.Distance(InputManager.GetMousePosition(), EntityManager.player.position) / (lookDistance * 4f), 1f);
                }
            }

            position += (lookPosition - position) * lookSpeed;
        }

        public static Box GetBox() {
            return new Box(position, Size);
        }
    }
}