﻿namespace Cosmic.Entities.Characters.NPCs {
    using Cosmic.NPCs;

    public abstract class NPCEntity<T> : Character where T : NPC {
        public T nPC;

        public override void Init() {
            sprite = nPC.sprite;
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size));

            healthMax = nPC.health;
            health = healthMax;
        }
    }
}