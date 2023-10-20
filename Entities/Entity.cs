namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Worlds;
    using Cosmic;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Entity {
        public enum DrawLayer {
            WorldObjects,
            NPCs,
            Player,
            ItemDrops,
            Projectiles,
            Debug
        }

        public Vector2 position;
        public float rotation;
        public Vector2 scale = Vector2.One;

        public SpriteEffects flip;

        public Vector2 velocity;

        public DrawLayer drawLayer;

        public Sprite sprite;
        public EntityCollider collider;

        public World world;

        public abstract void Init();

        public virtual void Update() {
            position += velocity;
        }

        public virtual void Draw() {
            sprite.Draw(position, rotation, scale, flip: flip);
        }

        public virtual void Destroy() {
            EntityManager.entities.Remove(this);
        }

        public bool GetExists() {
            return EntityManager.entities.Contains(this);
        }
    }
}