namespace Cosmic.Worlds {
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using System;
    using System.Collections.Generic;

    public class WorldManager {
        public List<World> Worlds { get; private set; }
        public World WorldCurrent { get; private set; }

        public static Forest forest;
        public static Desert desert;

        public void Load() {
            Worlds = new List<World>();

            forest = AddWorld<Forest>(120, 80);
            desert = AddWorld<Desert>(120, 80);
        }

        public void Update() {
            foreach (World world in Worlds) {
                world.Update();
            }
        }

        public void Draw() {
            WorldCurrent.Draw();
        }

        public void Generate() {
            foreach (World world in Worlds) {
                world.Generate();
            }

            if (Worlds.Count > 0) {
                WorldCurrent = Worlds[0];

                EntityManager.player = EntityManager.AddEntity<Player>(WorldCurrent.Size / 2f);
                //EntityManager.AddEntity<ShooterEntity>(worldCurrent.Size / 3f, preInitAction: shooterEntity => shooterEntity.nPC = NPCManager.greenShooter);
            }
        }

        public static T AddWorld<T>(int tileMapWidth, int tileMapHeight, bool generate = false) where T : World {
            T world = Activator.CreateInstance<T>();
            world.Load(tileMapWidth, tileMapHeight);

            if (generate) {
                world.Generate();
            }

            Worlds.Add(world);

            return world;
        }
    }
}