namespace Cosmic.Worlds {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Entities.Characters.Enemies;
    using System.Collections.Generic;

    public static class WorldManager {
        public static List<World> worlds;
        public static World worldCurrent;

        public static Main main;

        public static void Load(ContentManager contentManager) {
            worlds = new List<World>();

            main = new Main(800, 256);

            worlds.Add(main);



            main.Generate();

            worldCurrent = main;
        }

        public static void Update(GameTime gameTime) {
            foreach (World world in worlds) {
                world.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime) {
            worldCurrent.Draw(gameTime);
        }
    }
}