namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using Cosmic.Entities.Characters;
    using Cosmic.Entities.Characters.Enemies;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class Projectile : Entity {
        public int damage;
        public float strength;

        public bool enemy;

        public override void Init() {
            drawPriority = 3;

            sprite = new Sprite(AssetManager.projectile, new Vector2(AssetManager.projectile.Width, AssetManager.projectile.Height) / 2f);
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size.ToVector2()));
        }

        public override void Update(GameTime gameTime) {
            float distance = velocity.Length();

            for (float distanceMoved = 0; distanceMoved < distance;) {
                float distanceToMove = Math.Min(1f, distance - distanceMoved);

                bool destroy = false;

                if (collider.GetCollisionWithTiles(out List<TilemapTile> tilemapTiles)) {
                    foreach (TilemapTile tilemapTile in tilemapTiles) {
                        tilemapTile.Hurt(1);
                    }

                    destroy = true;
                }

                if (collider.GetCollisionWithEntities(out List<CharacterEntity> characters)) {
                    foreach (CharacterEntity character in characters) {
                        if (character is EnemyCharacter ^ enemy) {
                            character.Hurt(damage, Vector2.Normalize(velocity) * strength, new Vector2(Math.Clamp(character.position.X, position.X + collider.box.Left, position.X + collider.box.Right), Math.Clamp(character.position.Y, position.Y + collider.box.Top, position.Y + collider.box.Bottom)));
                            destroy = true;
                        }
                    }
                }

                if (destroy) {
                    Destroy();
                    break;
                }

                position += Vector2.Normalize(velocity) * distanceToMove;

                distanceMoved += distanceToMove;
            }
        }
    }
}