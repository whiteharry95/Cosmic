namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using Cosmic.Entities.Projectiles;
    using Cosmic.Projectiles;
    using Cosmic.Items.Weapons;
    using System;

    public class Gun : Weapon {
        public float projectileOffset;
        public float projectileSpeed;
        public int projectileDamage;
        public float projectileStrength;

        public override void Load() {
            displayRotation = -MathF.PI / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 bulletDirection = MathUtilities.GetNormalisedVector2(InputManager.GetMouseWorldPosition() - EntityManager.player.position);

            if (bulletDirection != Vector2.Zero) {
                BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(EntityManager.player.position + bulletDirection * projectileOffset, EntityManager.player.world, bulletEntity => bulletEntity.projectile = ProjectileManager.copperBullet);
                bullet.rotation = MathUtilities.GetDirectionBetweenPositions(EntityManager.player.position, InputManager.GetMouseWorldPosition());
                bullet.velocity = bulletDirection * projectileSpeed;
                bullet.damage = projectileDamage;
                bullet.strength = projectileStrength;
            }
        }
    }
}