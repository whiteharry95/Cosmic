namespace Cosmic.Entities.Projectiles {
    using Cosmic.Projectiles;

    public abstract class ProjectileEntity<T> : Entity where T : Projectile {
        public T projectile;

        public int damage;
        public float strength;

        public bool enemy;

        public override void Init() {
            drawLayer = DrawLayer.Projectiles;

            sprite = projectile.sprite;
            collider = new EntityCollider(this);
        }
    }
}