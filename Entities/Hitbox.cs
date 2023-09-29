namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using Cosmic.Entities.Characters.Enemies;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class Hitbox : Entity {
        public int damage;
        public Vector2 force;

        public bool enemy;

        public override void Init() {
            drawPriority = 999;

            sprite = new Sprite(AssetManager.hitbox, new Vector2(AssetManager.hitbox.Width, AssetManager.hitbox.Height) / 2f);
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size.ToVector2()));
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (collider.GetCollisionWithTiles(out List<TilemapTile> tilemapTiles)) {
                foreach (TilemapTile tilemapTile in tilemapTiles) {
                    tilemapTile.Hurt(1);
                }
            }

            if (collider.GetCollisionWithEntities(out List<CharacterEntity> characters)) {
                foreach (CharacterEntity character in characters) {
                    if (character is EnemyCharacter ^ enemy) {
                        character.Hurt(damage, force, new Vector2(Math.Clamp(character.position.X, position.X + collider.box.Left, position.X + collider.box.Right), Math.Clamp(character.position.Y, position.Y + collider.box.Top, position.Y + collider.box.Bottom)));
                    }
                }
            }

            Destroy();
        }
    }
}