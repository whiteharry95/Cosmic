namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Cosmic.UI;
    using System;
    using Cosmic.Utilities;
    using Cosmic.Entities;

    public static class Camera {
        public const int Scale = 2;

        public static int Width => Game1.graphicsDeviceManager.PreferredBackBufferWidth / Scale;
        public static int Height => Game1.graphicsDeviceManager.PreferredBackBufferHeight / Scale;

        public static Vector2 Size => new Vector2(Width, Height);

        public static Vector2 position;

        public static bool lookSet;
        public static Vector2 lookPosition;
        public static float lookDistance = 64f;
        public static float lookSpeed = 0.25f;

        public static void Update() {
            if (EntityManager.player?.GetExists() ?? false) {
                lookPosition = EntityManager.player.position - (Size / 2f);

                if (!UIManager.playerInventory.open) {
                    lookPosition += MathUtilities.GetNormalisedVector2(InputManager.GetMouseWorldPosition() - EntityManager.player.position) * lookDistance * Math.Min(Vector2.Distance(InputManager.GetMouseWorldPosition(), EntityManager.player.position) / (lookDistance * 4f), 1f);
                }
            }

            if (!lookSet) {
                position = lookPosition;
                lookSet = true;
            }

            position += (lookPosition - position) * lookSpeed;
        }
    }
}