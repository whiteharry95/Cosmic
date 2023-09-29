namespace Cosmic.Items {
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Items.Blocks;
    using Cosmic.Items.Tools.Miners;
    using Cosmic.Items.Weapons.Guns;
    using System;

    public static class ItemManager {
        private static Item[] items;

        public static DirtBlock dirtBlock;
        public static StoneBlock stoneBlock;
        public static WoodenPlatform woodenPlatform;
        public static Gun gun;
        public static Miner miner;
        public static Sword sword;

        public static void Init() {
            items = new Item[6];

            dirtBlock = AddItem<DirtBlock>(0);
            stoneBlock = AddItem<StoneBlock>(1);
            woodenPlatform = AddItem<WoodenPlatform>(2);
            gun = AddItem<Gun>(3);
            miner = AddItem<Miner>(4);
            sword = AddItem<Sword>(5);
        }

        public static void Load(ContentManager contentManager) {
            for (int i = 0; i < items.Length; i++) {
                items[i].Load(contentManager);
            }
        }

        public static T AddItem<T>(int id) where T : Item {
            items[id] = Activator.CreateInstance<T>();
            items[id].id = id;

            return (T)items[id];
        }
    }
}