namespace Cosmic.Items.Swords {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;

    public class Sword : Item {
        public int hitboxDamage;
        public float hitboxStrength;
        public float hitboxOffset;

        public override void Load() {
            displayRotation = -MathHelper.Pi / 4f;
        }

        public override void OnUse() {
            Vector2 hitboxDirection = MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - EntityManager.player.position);

            if (hitboxDirection != Vector2.Zero) {
                Hitbox hitbox = EntityManager.AddEntity<Hitbox>(EntityManager.player.position + hitboxDirection * hitboxOffset, EntityManager.player.world);
                hitbox.damage = hitboxDamage;
                hitbox.force = hitboxDirection * hitboxStrength;

                EntityManager.player.itemRotationOffsetAxis *= -1f;
            }
        }
    }
}