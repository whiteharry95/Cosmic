using Cosmic.Items;

namespace Cosmic.Inventory {
    public class Inventory {
        public InventorySlot[] slots;

        public Inventory(int size) {
            slots = new InventorySlot[size];
        }

        public int AddItem(Item item, int quantity = 1, int slotIndex = 0, bool slotIndexOnly = false) {
            int i;

            for (i = 0; i < quantity; i++) {
                bool found = false;

                for (int s = slotIndex; s < slots.Length; s++) {
                    if (slots[s] == null) {
                        slots[s] = new InventorySlot(item, 1);
                        found = true;
                        break;
                    }

                    if (slots[s].item == item) {
                        if (slots[s].quantity < item.stack) {
                            slots[s].quantity++;
                            found = true;
                            break;
                        }
                    }

                    if (slotIndexOnly) {
                        break;
                    }
                }

                if (!found) {
                    break;
                }
            }

            return quantity - i;
        }

        public void RemoveItem(Item item, int quantity = 1, int slotIndex = 0, bool slotIndexOnly = false) {
            for (int i = 0; i < quantity; i++) {
                for (int s = slotIndex; s < slots.Length; s++) {
                    if (slots[s]?.item == item) {
                        slots[s].quantity--;

                        if (slots[s].quantity <= 0) {
                            slots[s] = null;
                            break;
                        }
                    }

                    if (slotIndexOnly) {
                        break;
                    }
                }
            }
        }

        public int GetItemQuantity(Item item) {
            int quantity = 0;

            for (int s = 0; s < slots.Length; s++) {
                if (slots[s]?.item == item) {
                    quantity += slots[s].quantity;
                }
            }

            return quantity;
        }
    }
}