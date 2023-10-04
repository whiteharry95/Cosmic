namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Entities.Characters;
    using Cosmic.Worlds;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using Cosmic.Entities.Characters.NPCs;
    using Cosmic.NPCs;

    public static class EntityManager {
        public static List<Entity> entities;

        public static Player player;

        public static void Load(ContentManager contentManager) {
            entities = new List<Entity>();

            player = AddEntity<Player>(new Vector2(WorldManager.worldCurrent.Width, WorldManager.worldCurrent.Height) / 2f);

            AddEntity<ShooterEntity>(player.position + new Vector2(128f, 0f), preInitAction: shooterEntity => shooterEntity.nPC = NPCManager.greenShooter);
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
                if (entity.world == WorldManager.worldCurrent) {
                    entity.Draw(gameTime);
                }
            }
        }

        public static T AddEntity<T>(Vector2 position, World world = null, Action<T> preInitAction = null) where T : Entity {
            T entity = Activator.CreateInstance<T>();
            entity.position = position;
            entity.world = world ?? WorldManager.worldCurrent;

            entities.Add(entity);

            preInitAction?.Invoke(entity);
            entity.Init();

            return entity;
        }

        public static List<Entity> GetEntitiesInWorld(World world = null) {
            return entities.FindAll(entity => entity.world == (world ?? WorldManager.worldCurrent));
        }
    }
}