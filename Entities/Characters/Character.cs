namespace Cosmic.Entities.Characters {
    using Microsoft.Xna.Framework;

    public abstract class Character : Entity {
        public int health;
        public int healthMax;

        public virtual bool Hurt(int damage, Vector2? force = null) {
            if (damage <= 0) {
                return false;
            }

            health -= damage;

            if (health <= 0) {
                health = 0;
                Destroy();
            }

            if (force != null) {
                velocity = (Vector2)force;
            }

            DamageText damageText = EntityManager.AddEntity<DamageText>(position, world);
            damageText.damage = damage;

            return true;
        }
    }
}