namespace Cosmic.Entities.Characters {
    using Microsoft.Xna.Framework;
    using System.Diagnostics;
    using System.Diagnostics.SymbolStore;

    public abstract class CharacterEntity : Entity {
        public int health;
        public int healthMax;

        public int invincibilityTime;
        public int invincibilityTimeMax;

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (invincibilityTime > 0) {
                invincibilityTime--;
            }
        }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(position - Camera.position, 0f, 1f, invincibilityTime > 0 ? (invincibilityTime % 2 == 0 ? 0.5f : 0.75f) : 1f);
        }

        public virtual bool Hurt(int damage, Vector2? force = null, Vector2? position = null) {
            if (damage <= 0 || invincibilityTime > 0) {
                return false;
            }

            health -= damage;

            if (health <= 0) {
                Destroy();
            }

            if (force != null) {
                velocity = (Vector2)force;
            }

            invincibilityTime = invincibilityTimeMax;

            DamageText damageText = EntityManager.AddEntity<DamageText>(position ?? this.position, world);
            damageText.damage = damage;

            return true;
        }

        public virtual void Test() {
            return;
        }
    }
}