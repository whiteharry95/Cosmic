namespace Cosmic.Items.Weapons.Swords {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using System;

    public class Sword : Weapon {
        public int hitboxDamage;
        public float hitboxStrength;
        public float hitboxOffset;

        public override void Load() {
            displayRotation = -MathF.PI / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 hitboxDirection = MathUtilities.GetNormalisedVector2(InputManager.GetMouseWorldPosition() - EntityManager.player.position);

            if (hitboxDirection != Vector2.Zero) {
                Hitbox hitbox = EntityManager.AddEntity<Hitbox>(EntityManager.player.position + hitboxDirection * hitboxOffset, EntityManager.player.world);
                hitbox.damage = hitboxDamage;
                hitbox.force = hitboxDirection * hitboxStrength;

                EntityManager.player.itemRotationOffsetAxis *= -1f;
            }
        }
    }
}