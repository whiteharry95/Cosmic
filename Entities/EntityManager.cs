namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Entities.Characters;
    using Cosmic.Universes;
    using System.Collections.Generic;
    using System;
    using System.Linq;

    public static class EntityManager {
        public static List<Entity> entities;

        public static Player[] players = new Player[16];
        public static int playerCurrentIndex;

        public static void Load(ContentManager contentManager) {
            entities = new List<Entity>();
        }

        public static void Update(GameTime gameTime) {
            Entity[] entitiesPrevious = entities.ToArray();

            foreach (Entity entity in entitiesPrevious) {
                entity.Update(gameTime);
            }
        }

        public static void Draw(GameTime gameTime) {
            Entity[] entitiesPreviousDrawOrdered = entities.OrderBy((Entity entity) => entity.drawLayer).ToArray();

            foreach (Entity entity in entitiesPreviousDrawOrdered) {
                if (entity.world == UniverseManager.universeCurrent.worldCurrent) {
                    entity.Draw(gameTime);
                }
            }
        }

        public static T AddEntity<T>(Vector2 position, World world = null, Action<T> preInitAction = null) where T : Entity {
            T entity = Activator.CreateInstance<T>();
            entity.position = position;
            entity.world = world ?? UniverseManager.universeCurrent.worldCurrent;

            entities.Add(entity);

            preInitAction?.Invoke(entity);
            entity.Init();

            return entity;
        }

        public static List<Entity> GetEntitiesInWorld(World world = null) {
            return entities.FindAll(entity => entity.world == (world ?? UniverseManager.universeCurrent.worldCurrent));
        }

        /*public static List<T> GetEntitiesContainingPosition<T>(Vector2 position, World world = null) where T : Entity {
            List<T> entitiesContainingPosition = new List<T>();
            List<T> entitiesInWorld = GetEntitiesInWorld(world).OfType<T>().ToList();

            foreach(T entity in entitiesInWorld) {
                if (entity.collider.GetBoxReal().GetContains(position)) {
                    entitiesContainingPosition.Add(entity);
                }
            }

            return entitiesContainingPosition;
        }*/

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