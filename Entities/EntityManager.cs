namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using Cosmic.Worlds;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public static class EntityManager {
        public static List<Entity> entities;

        public static Player player;

        public static void Init() {
            entities = new List<Entity>();
        }

        public static void Update() {
            Entity[] entitiesPrevious = entities.ToArray();

            foreach (Entity entity in entitiesPrevious) {
                entity.Update();
            }
        }

        public static void Draw() {
            Entity[] entitiesPreviousDrawOrdered = entities.OrderBy((Entity entity) => entity.drawLayer).ToArray();

            foreach (Entity entity in entitiesPreviousDrawOrdered) {
                if (entity.world == WorldManager.WorldCurrent) {
                    entity.Draw();
                }
            }
        }

        public static T AddEntity<T>(Vector2 position, World world = null, Action<T> preInitAction = null) where T : Entity {
            T entity = Activator.CreateInstance<T>();
            entity.position = position;
            entity.world = world ?? WorldManager.WorldCurrent;

            entities.Add(entity);

            preInitAction?.Invoke(entity);
            entity.Init();

            return entity;
        }

        public static List<Entity> GetEntitiesInWorld(World world = null) {
            return entities.FindAll(entity => entity.world == (world ?? WorldManager.WorldCurrent));
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