namespace Cosmic.Entities.Characters.NPCs {
    using Cosmic.NPCs;

    public abstract class NPCEntity<T> : Character where T : NPC {
        public T nPC;

        public override void Init() {
            drawLayer = DrawLayer.NPCs;

            sprite = nPC.sprite;
            collider = new EntityCollider(this);

            healthMax = nPC.health;
            health = healthMax;
        }
    }
}