namespace Cosmic.Crafting {
    using Cosmic.Items;
    using Cosmic.Inventory;
    using System.Collections.Generic;

    public class Recipe {
        public Item itemProduct;
        public List<ItemQuantity> itemQuantities;

        public bool GetCanCraft(Inventory inventory) {
            foreach(ItemQuantity itemQuantity in itemQuantities) {
                if (inventory.GetItemQuantity(itemQuantity.item) < itemQuantity.quantity) {
                    return false;
                }
            }

            return true;
        }
    }
}