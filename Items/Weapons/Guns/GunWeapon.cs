namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;

    public class GunWeapon : WeaponItem {
        public float projectileSpeed;
        public int projectileDamage;
        public float projectileStrength;
        public float projectileOffset;

        public override void OnUse() {
            Vector2 projectileDirection = Vector2.Normalize(InputManager.GetMousePosition() - EntityManager.player.position);

            Projectile projectile = EntityManager.AddEntity<Projectile>(EntityManager.player.position + (projectileDirection * projectileOffset), EntityManager.player.world);
            projectile.velocity = projectileDirection * projectileSpeed;
            projectile.damage = projectileDamage;
            projectile.strength = projectileStrength;
        }
    }
}