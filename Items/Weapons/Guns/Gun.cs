namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using Cosmic.Entities.Projectiles;
    using Cosmic.Projectiles;
    using Cosmic.Items.Weapons;
    using System;

    public class Gun : Weapon {
        protected float projectileOffset;
        protected float projectileSpeed;
        protected int projectileDamage;
        protected float projectileStrength;

        public override void Load() {
            displayRotation = -MathF.PI / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 mouseWorldPosition = InputManager.GetMouseWorldPosition();
            
            Vector2 bulletDirection = MathUtilities.GetNormalisedVector2(mouseWorldPosition - EntityManager.Player.position);

            if (bulletDirection != Vector2.Zero) {
                BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(EntityManager.Player.position + (bulletDirection * projectileOffset), EntityManager.Player.world, bulletEntity => bulletEntity.projectile = ProjectileManager.copperBullet);
                bullet.rotation = MathF.Atan2(mouseWorldPosition.Y - EntityManager.Player.position.Y, mouseWorldPosition.X - EntityManager.Player.position.X);
                bullet.velocity = projectileSpeed * bulletDirection;
                bullet.damage = projectileDamage;
                bullet.strength = projectileStrength;
            }
        }
    }
}