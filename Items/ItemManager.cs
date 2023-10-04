namespace Cosmic.Items
{
    using Cosmic.Items.Blocks;
    using Cosmic.Items.Drills;
    using Cosmic.Items.Guns;
    using Cosmic.Items.Swords;
    using System;
    using System.Collections.Generic;

    public static class ItemManager {
        private static List<Item> items;

        public static DirtBlock dirtBlock;
        public static StoneBlock stoneBlock;
        public static MachineGun gun;
        public static CopperDrill miner;
        public static WoodenSword sword;

        public static void Init() {
            items = new List<Item>();

            dirtBlock = AddItem<DirtBlock>(false);
            stoneBlock = AddItem<StoneBlock>(false);
            gun = AddItem<MachineGun>(false);
            miner = AddItem<CopperDrill>(false);
            sword = AddItem<WoodenSword>(false);
        }

        public static void Load() {
            foreach (Item item in items) {
                item.Load();
            }
        }

        public static T AddItem<T>(bool load = true) where T : Item {
            T item = Activator.CreateInstance<T>();
            item.id = items.Count;

            if (load) {
                item.Load();
            }

            items.Add(item);

            return item;
        }
    }
}