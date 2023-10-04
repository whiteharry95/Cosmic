namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Worlds;
    using Cosmic;

    public abstract class Entity {
        public enum DrawLayer {
            Player,
            ItemDrops,
            Projectiles,
            Debug
        }

        public Vector2 position;
        public Vector2 velocity;

        public DrawLayer drawLayer;

        public Sprite sprite;
        public EntityCollider collider;

        public World world;

        public abstract void Init();

        public virtual void Update(GameTime gameTime) {
            position += velocity;
        }

        public virtual void Draw(GameTime gameTime) {
            sprite.Draw(position - Camera.position);
        }

        public virtual void Destroy() {
            EntityManager.entities.Remove(this);
        }

        public bool GetExists() {
            return EntityManager.entities.Contains(this);
        }
    }
}