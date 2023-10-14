namespace Cosmic.Items.Weapons.Swords {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;

    public class Sword : Weapon {
        public int hitboxDamage;
        public float hitboxStrength;
        public float hitboxOffset;

        public override void Load() {
            displayRotation = -MathHelper.Pi / 4f;
        }

        public override void OnPrimaryUse() {
            Vector2 hitboxDirection = MathUtilities.NormaliseVector2(InputManager.GetMousePosition() - Game1.server.netPlayers[0].player.position);

            if (hitboxDirection != Vector2.Zero) {
                Hitbox hitbox = EntityManager.AddEntity<Hitbox>(Game1.server.netPlayers[0].player.position + hitboxDirection * hitboxOffset, Game1.server.netPlayers[0].player.world);
                hitbox.damage = hitboxDamage;
                hitbox.force = hitboxDirection * hitboxStrength;

                Game1.server.netPlayers[0].player.itemRotationOffsetAxis *= -1f;
            }
        }
    }
}