namespace Cosmic.NPCs {
    using Cosmic.NPCs.Shooters;
    using System;
    using System.Collections.Generic;

    public static class NPCManager {
        private static List<NPC> nPCS;

        public static GreenShooter GreenShooter { get; private set; }

        public static void Init() {
            nPCS = new List<NPC>();

            GreenShooter = AddNPC<GreenShooter>();
        }

        public static void Load() {
            foreach (NPC nPC in nPCS) {
                nPC.Load();
            }
        }

        public static T AddNPC<T>(bool load = false) where T : NPC {
            T nPC = Activator.CreateInstance<T>();
            nPC.id = (ushort)nPCS.Count;

            if (load) {
                nPC.Load();
            }

            nPCS.Add(nPC);

            return nPC;
        }
    }
}