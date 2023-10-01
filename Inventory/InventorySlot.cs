using Cosmic.Items;

namespace Cosmic.Inventory {
    public class InventorySlot {
        public Item item;
        public int quantity;

        public InventorySlot(Item item, int quantity) {
            this.item = item;
            this.quantity = quantity;
        }
    }
}