namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using System.Collections.Generic;
    using Cosmic.Assets;
    using Cosmic.Worlds;

    public class Hitbox : Entity {
        public int damage;
        public Vector2 force;

        public bool enemy;

        public override void Init() {
            drawLayer = DrawLayer.Debug;

            sprite = new Sprite(TextureManager.Entities_Hitbox, Sprite.OriginPreset.MiddleCentre);
            collider = new EntityCollider(this);
        }

        public override void Update() {
            base.Update();

            if (collider.GetCollisionWithTiles(out List<WorldTileMapTile> tileMapTiles)) {
                foreach (WorldTileMapTile tileMapTile in tileMapTiles) {
                    tileMapTile.Mine(1);
                }
            }

            if (collider.GetCollisionWithEntities(out List<Character> characters)) {
                foreach (Character character in characters) {
                    if (character is Player == enemy) {
                        character.Hurt(damage, force);
                    }
                }
            }

            Destroy();
        }
    }
}