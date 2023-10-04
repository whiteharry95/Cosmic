namespace Cosmic.Worlds {
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    public static class WorldManager {
        public static List<World> worlds;
        public static World worldCurrent;

        public static Main main;

        public static void Generate() {
            worlds = new List<World> {
                (main = new Main(800, 800))
            };

            main.Generate();

            worldCurrent = main;
        }

        public static void Draw(GameTime gameTime) {
            worldCurrent.Draw(gameTime);
        }
    }
}