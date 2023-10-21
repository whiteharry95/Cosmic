namespace Cosmic.Items.Weapons.Swords {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using System;

    public class Sword : Weapon {
        protected int hitboxDamage;
        protected float hitboxStrength;
        protected float hitboxOffset;

        public override void Load() {
            displayRotation = -MathF.PI / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 hitboxDirection = MathUtilities.GetNormalisedVector2(InputManager.GetMouseWorldPosition() - EntityManager.Player.position);

            if (hitboxDirection != Vector2.Zero) {
                Hitbox hitbox = EntityManager.AddEntity<Hitbox>(EntityManager.Player.position + hitboxDirection * hitboxOffset, EntityManager.Player.world);
                hitbox.damage = hitboxDamage;
                hitbox.force = hitboxDirection * hitboxStrength;

                EntityManager.Player.itemRotationOffsetAxis *= -1f;
            }
        }
    }
}