namespace Cosmic.WorldObjects {
    using System.Collections.Generic;
    using System;

    public static class WorldObjectManager {
        public static Rock Rock { get; private set; }
        public static Chandelier Chandelier { get; private set; }

        private static List<WorldObject> worldObjects;
        
        public static void Init() {
            worldObjects = new List<WorldObject>();

            Rock = AddWorldObject<Rock>();
            Chandelier = AddWorldObject<Chandelier>();
        }

        public static void Load() {
            foreach (WorldObject worldObject in worldObjects) {
                worldObject.Load();
            }
        }

        public static T AddWorldObject<T>(bool load = false) where T : WorldObject {
            T worldObject = Activator.CreateInstance<T>();
            worldObject.id = (ushort)worldObjects.Count;

            if (load) {
                worldObject.Load();
            }

            worldObjects.Add(worldObject);

            return worldObject;
        }
    }
}