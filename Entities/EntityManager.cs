namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using Cosmic.Worlds;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using Cosmic.Entities.Characters.NPCs;
    using Cosmic.NPCs;

    public static class EntityManager {
        public static List<Entity> Entities { get; private set; }

        public static Player Player;

        public static void Init() {
            Entities = new List<Entity>();
        }

        public static void Load() {
            Player = AddEntity<Player>(new Vector2(128f), WorldManager.WorldCurrent);

            AddEntity<ShooterEntity>(Player.position + new Vector2(128f, 0f), WorldManager.WorldCurrent, shooterEntity => shooterEntity.nPC = NPCManager.GreenShooter);
        }

        public static void Update() {
            Entity[] entitiesAsArray = Entities.ToArray();

            foreach (Entity entity in entitiesAsArray) {
                entity.Update();
            }
        }

        public static void Draw() {
            List<Entity> entitiesSortedByDrawLayer = Entities.OrderBy(entity => entity.drawLayer).ToList();

            foreach (Entity entity in entitiesSortedByDrawLayer) {
                if (entity.world == WorldManager.WorldCurrent) {
                    entity.Draw();
                }
            }
        }

        public static T AddEntity<T>(Vector2 position, World world, Action<T> preInitAction = null) where T : Entity {
            T entity = Activator.CreateInstance<T>();
            entity.position = position;
            entity.world = world;

            Entities.Add(entity);

            preInitAction?.Invoke(entity);
            entity.Init();

            return entity;
        }

        public static List<Entity> GetEntitiesInWorld(World world = null) {
            return Entities.FindAll(entity => entity.world == (world ?? WorldManager.WorldCurrent));
        }

        public static List<T> GetEntitiesIntersectingPolygon<T>(Polygon polygon, World world = null) where T : Entity {
            List<T> entitiesIntersectingBox = new List<T>();
            List<T> entitiesInWorld = GetEntitiesInWorld(world).OfType<T>().ToList();

            foreach (T entity in entitiesInWorld) {
                if (entity.collider.polygon.GetCollisionWithPolygon(polygon)) {
                    entitiesIntersectingBox.Add(entity);
                }
            }

            return entitiesIntersectingBox;
        }
    }
}