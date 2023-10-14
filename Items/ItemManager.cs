namespace Cosmic.Items {
    using Cosmic.Items.Blocks;
    using Cosmic.Items.Tools.Drills;
    using Cosmic.Items.Weapons.Guns;
    using Cosmic.Items.Weapons.Swords;
    using Cosmic.Items.WorldObjects;
    using System;
    using System.Collections.Generic;

    public static class ItemManager {
        private static List<Item> items;

        public static DirtBlock dirtBlock;
        public static StoneBlock stoneBlock;
        public static MachineGun gun;
        public static CopperDrill copperDrill;
        public static WoodenSword sword;
        public static Rock rock;

        public static void Init() {
            items = new List<Item>();

            dirtBlock = AddItem<DirtBlock>(false);
            stoneBlock = AddItem<StoneBlock>(false);
            gun = AddItem<MachineGun>(false);
            copperDrill = AddItem<CopperDrill>(false);
            sword = AddItem<WoodenSword>(false);
            rock = AddItem<Rock>(false);
        }

        public static void Load() {
            foreach (Item item in items) {
                item.Load();
            }
        }

        public static T AddItem<T>(bool load = true) where T : Item {
            T item = Activator.CreateInstance<T>();
            item.id = (ushort)items.Count;

            if (load) {
                item.Load();
            }

            items.Add(item);

            return item;
        }
    }
}