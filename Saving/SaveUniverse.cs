namespace Cosmic.Saving {
    using Cosmic.Universes;
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class SaveUniverse {
        public List<SaveWorld> worlds;

        public Universe GetAsUniverse() {
            Universe universe = new Universe();

            return universe;
        }
    }
}