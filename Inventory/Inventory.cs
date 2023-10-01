using Cosmic.Items;

namespace Cosmic.Inventory {
    public class Inventory {
        public int Width => slots.GetLength(0);
        public int Height => slots.GetLength(1);

        public InventorySlot[,] slots;

        public Inventory(int width, int height) {
            slots = new InventorySlot[width, height];
        }

        public void AddItem(Item item, int quantity = 1) {
            for (int i = 0; i < quantity; i++) {
                bool added = false;

                for (int y = 0; y < Height; y++) {
                    for (int x = 0; x < Width; x++) {
                        if (slots[x, y] == null) {
                            slots[x, y] = new InventorySlot(item, 1);
                            added = true;
                            break;
                        }

                        if (slots[x, y].item == item) {
                            if (slots[x, y].quantity < item.stack) {
                                slots[x, y].quantity++;
                                added = true;
                                break;
                            }
                        }
                    }

                    if (added) {
                        break;
                    }
                }
            }
        }

        public void RemoveItem(Item item, int quantity = 1) {
            for (int i = 0; i < quantity; i++) {
                bool removed = false;

                for (int y = 0; y < Height; y++) {
                    for (int x = 0; x < Width; x++) {
                        if (slots[x, y]?.item == item) {
                            slots[x, y].quantity--;

                            if (slots[x, y].quantity <= 0) {
                                slots[x, y] = null;
                                removed = true;
                                break;
                            }
                        }
                    }

                    if (removed) {
                        break;
                    }
                }
            }
        }

        public int GetItemQuantity(Item item) {
            int quantity = 0;

            for (int y = 0; y < Height; y++) {
                for (int x = 0; x < Width; x++) {
                    if (slots[x, y]?.item == item) {
                        quantity += slots[x, y].quantity;
                    }
                }
            }

            return quantity;
        }
    }
}