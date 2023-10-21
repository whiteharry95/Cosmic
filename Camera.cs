namespace Cosmic {
    using Microsoft.Xna.Framework;
    using Cosmic.UI;
    using System;
    using Cosmic.Utilities;
    using Cosmic.Entities;

    public static class Camera {
        public static Vector2 Position { get; private set; }

        public static float Width => (float)Game1.GraphicsDeviceManager.PreferredBackBufferWidth / Scale;
        public static float Height => (float)Game1.GraphicsDeviceManager.PreferredBackBufferHeight / Scale;

        public static int Scale => 2;

        private static bool lookSet;
        private static Vector2 lookPosition;
        private static float lookSpeed = 0.25f;
        private static float lookDistance = 64f;

        public static void Load() {
            lookSet = false;
        }

        public static void Update() {
            if (EntityManager.Player?.GetExists() ?? false) {
                lookPosition = EntityManager.Player.position - (new Vector2(Width, Height) / 2f);

                if (!UIManager.playerInventory.open) {
                    lookPosition += MathUtilities.GetNormalisedVector2(InputManager.GetMouseWorldPosition() - EntityManager.Player.position) * lookDistance * Math.Min(Vector2.Distance(InputManager.GetMouseWorldPosition(), EntityManager.Player.position) / (lookDistance * 4f), 1f);
                }
            }

            if (!lookSet) {
                Position = lookPosition;
                lookSet = true;
            }

            Position += (lookPosition - Position) * lookSpeed;
        }
    }
}