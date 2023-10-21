namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Worlds;
    using Cosmic;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Entity {
        public enum DrawLayer {
            WorldObjects,
            NPCs,
            Players,
            ItemDrops,
            Projectiles,
            Debug
        }

        public Vector2 position;
        public float rotation;
        public Vector2 scale = Vector2.One;
        
        public SpriteEffects flip;
        
        public DrawLayer drawLayer;

        public Vector2 velocity;
        
        public Sprite sprite;
        public EntityCollider collider;

        public World world;
        
        public virtual void Init() {
        }

        public virtual void Update() {
            position += velocity;
        }

        public virtual void Draw() {
            sprite.Draw(position, rotation, scale, flip: flip);
        }

        public virtual void Destroy() {
            EntityManager.Entities.Remove(this);
        }

        public bool GetExists() {
            return EntityManager.Entities.Contains(this);
        }
    }
}