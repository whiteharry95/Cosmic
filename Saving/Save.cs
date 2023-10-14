namespace Cosmic.Saving {
    using Cosmic.Universes;
    using System;

    [Serializable]
    public class Save {
        public SaveUniverse saveUniverse;
        public SavePlayer savePlayer;

        public void Load() {
            UniverseManager.universeCurrent = saveUniverse.GetAsUniverse();
        }
    }
}