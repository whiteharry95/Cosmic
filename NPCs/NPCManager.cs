namespace Cosmic.NPCs {
    using Cosmic.NPCs.Shooters;
    using System;
    using System.Collections.Generic;

    public static class NPCManager {
        private static List<NPC> nPCs;

        public static GreenShooter greenShooter;

        public static void Init() {
            nPCs = new List<NPC>();

            greenShooter = AddEnemy<GreenShooter>(false);
        }

        public static void Load() {
            foreach (NPC enemy in nPCs) {
                enemy.Load();
            }
        }

        public static T AddEnemy<T>(bool load = true) where T : NPC {
            T enemy = Activator.CreateInstance<T>();
            enemy.id = (ushort)nPCs.Count;

            if (load) {
                enemy.Load();
            }

            nPCs.Add(enemy);

            return enemy;
        }
    }
}