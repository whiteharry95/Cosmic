namespace Cosmic.Items {
    using Cosmic.Items.Blocks;
    using Cosmic.Items.Tools.Drills;
    using Cosmic.Items.Weapons.Guns;
    using Cosmic.Items.Weapons.Swords;
    using Cosmic.Items.WorldObjects;
    using System;
    using System.Collections.Generic;

    public static class ItemManager {
        public static DirtBlock DirtBlock { get; private set; }
        public static StoneBlock StoneBlock { get; private set; }
        public static MachineGun Gun { get; private set; }
        public static CopperDrill CopperDrill { get; private set; }
        public static WoodenSword WoodenSword { get; private set; }
        public static Rock Rock { get; private set; }
        public static Chandelier Chandelier { get; private set; }

        private static List<Item> items;

        public static void Init() {
            items = new List<Item>();

            DirtBlock = AddItem<DirtBlock>();
            StoneBlock = AddItem<StoneBlock>();
            Gun = AddItem<MachineGun>();
            CopperDrill = AddItem<CopperDrill>();
            WoodenSword = AddItem<WoodenSword>();
            Rock = AddItem<Rock>();
            Chandelier = AddItem<Chandelier>();
        }

        public static void Load() {
            foreach (Item item in items) {
                item.Load();
            }
        }

        public static T AddItem<T>(bool load = false) where T : Item {
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