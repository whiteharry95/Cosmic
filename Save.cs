namespace Cosmic {
    using Cosmic.Items;
    using System;

    [Serializable]
    public class Save {
        public string name;

        public Inventory playerInventory;
        // custom items
        // custom tiles

        public Save(string name) {
            this.name = name;
        }
    }
}