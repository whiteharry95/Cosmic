namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Worlds;

    public abstract class Entity {
        public Vector2 position;
        public Vector2 velocity;

        public int drawPriority;

        public Sprite sprite;
        public EntityCollider collider;

        public World world;

        public abstract void Init();

        public virtual void Update(GameTime gameTime) {
            position += velocity;
        }

        public virtual void Draw(GameTime gameTime) {
            sprite.Draw(position - Camera.position, 0f, 1f, 1f);
        }

        public virtual void Destroy() {
            EntityManager.entities.Remove(this);
        }

        public bool GetExists() {
            return EntityManager.entities.Contains(this);
        }
    }
}