namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;

    public class SwordWeapon : WeaponItem {
        public int hitboxDamage;
        public float hitboxStrength;
        public float hitboxOffset;

        public override void OnUse() {
            Vector2 hitboxDirection = Vector2.Normalize(InputManager.GetMousePosition() - EntityManager.player.position);

            Hitbox hitbox = EntityManager.AddEntity<Hitbox>(EntityManager.player.position + (hitboxDirection * hitboxOffset), EntityManager.player.world);
            hitbox.damage = hitboxDamage;
            hitbox.force = hitboxDirection * hitboxStrength;

            EntityManager.player.itemRotationOffsetAxis *= -1f;
        }
    }
}