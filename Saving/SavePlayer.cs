namespace Cosmic.Saving {
    using Cosmic.Inventory;
    using System;

    [Serializable]
    public class SavePlayer {
        public string name;
        public Inventory inventory;
    }
}