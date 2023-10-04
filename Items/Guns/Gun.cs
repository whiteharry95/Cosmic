namespace Cosmic.Items.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using Cosmic.Entities.Projectiles;
    using Cosmic.Projectiles;

    public class Gun : Item {
        public float projectileOffset;
        public float projectileSpeed;
        public int projectileDamage;
        public float projectileStrength;

        public override void Load() {
            displayRotation = -MathHelper.Pi / 4f;
        }

        public override void OnUse() {
            Vector2 bulletDirection = MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - EntityManager.player.position);

            if (bulletDirection != Vector2.Zero) {
                BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(EntityManager.player.position + bulletDirection * projectileOffset, EntityManager.player.world, bulletProjectile => bulletProjectile.projectile = ProjectileManager.copperBullet);
                bullet.velocity = bulletDirection * projectileSpeed;
                bullet.damage = projectileDamage;
                bullet.strength = projectileStrength;
            }
        }
    }
}