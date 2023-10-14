namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using Cosmic.Entities.Projectiles;
    using Cosmic.Projectiles;
    using Cosmic.Items.Weapons;

    public class Gun : Weapon {
        public float projectileOffset;
        public float projectileSpeed;
        public int projectileDamage;
        public float projectileStrength;

        public override void Load() {
            displayRotation = -MathHelper.Pi / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 bulletDirection = MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - Game1.server.netPlayers[0].player.position);

            if (bulletDirection != Vector2.Zero) {
                BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(Game1.server.netPlayers[0].player.position + bulletDirection * projectileOffset, Game1.server.netPlayers[0].player.world, bulletEntity => bulletEntity.projectile = ProjectileManager.copperBullet);
                bullet.velocity = bulletDirection * projectileSpeed;
                bullet.damage = projectileDamage;
                bullet.strength = projectileStrength;
            }
        }
    }
}