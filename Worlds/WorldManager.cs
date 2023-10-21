namespace Cosmic.Worlds {
    using System;
    using System.Collections.Generic;

    public static class WorldManager {
        public static List<World> Worlds { get; private set; }
        public static World WorldCurrent { get; private set; }

        public static Forest Forest { get; private set; }
        public static Desert Desert { get; private set; }

        public static void Load() {
            Worlds = new List<World>();

            Forest = AddWorld<Forest>(120, 80);
            Desert = AddWorld<Desert>(120, 80);
        }

        public static void Update() {
            foreach (World world in Worlds) {
                world.Update();
            }
        }

        public static void Draw() {
            WorldCurrent?.Draw();
        }

        public static void Generate() {
            foreach (World world in Worlds) {
                world.Generate();
            }

            WorldCurrent = Worlds[0];
        }

        public static T AddWorld<T>(int tileMapWidth, int tileMapHeight, bool generate = false) where T : World {
            T world = Activator.CreateInstance<T>();
            Worlds.Add(world);

            world.Load(tileMapWidth, tileMapHeight);

            if (generate) {
                world.Generate();
            }

            return world;
        }

        public static void ChangeWorld(World world) {
            WorldCurrent = world;
        }
    }
}