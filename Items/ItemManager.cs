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
        public static Chandelier chandelier;
        public static WorldTeleporter worldTeleporter;
        public static WorldBuilder worldBuilder;

        public static void Init() {
            items = new List<Item>();

            dirtBlock = AddItem<DirtBlock>();
            stoneBlock = AddItem<StoneBlock>();
            gun = AddItem<MachineGun>();
            copperDrill = AddItem<CopperDrill>();
            sword = AddItem<WoodenSword>();
            rock = AddItem<Rock>();
            chandelier = AddItem<Chandelier>();
            worldTeleporter = AddItem<WorldTeleporter>();
            worldBuilder = AddItem<WorldBuilder>();
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