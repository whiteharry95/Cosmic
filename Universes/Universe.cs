namespace Cosmic.Universes {
    using Cosmic.Saving;
    using System.Collections.Generic;

    public class Universe {
        public List<World> worlds;
        public World worldCurrent;

        public void Generate() {
            foreach (World world in worlds) {
                world.Generate();
            }

            worldCurrent = worlds[0];
        }

        public SaveUniverse GetAsSaveUniverse() {
            SaveUniverse saveUniverse = new SaveUniverse();
            //saveUniverse.worlds = worlds;

            return saveUniverse;
        }
    }
}