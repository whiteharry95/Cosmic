namespace Cosmic.Entities.Projectiles {
    using Cosmic.Entities.Characters;
    using Cosmic.TileMap;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using System;
    using Cosmic.Projectiles.Bullets;

    public class BulletEntity : ProjectileEntity<Bullet> {
        public override void Update(GameTime gameTime) {
            float distance = velocity.Length();

            for (float distanceMoved = 0f; distanceMoved < distance;) {
                float distanceToMove = Math.Min(0.5f, distance - distanceMoved);

                bool destroy = false;

                if (collider.GetCollisionWithTiles(out List<TileMapTile> tileMapTiles)) {
                    foreach (TileMapTile tileMapTile in tileMapTiles) {
                        tileMapTile.Hurt(1);
                    }

                    destroy = true;
                }

                if (collider.GetCollisionWithEntities(out List<Character> characters)) {
                    foreach (Character character in characters) {
                        if (character is Player == enemy) {
                            //character.Hurt(damage, MathUtilities.NormaliseVector2(velocity) * strength, new Vector2(Math.Clamp(character.position.X, position.X + collider.polygon.Left, position.X + collider.polygon.Right), Math.Clamp(character.position.Y, position.Y + collider.polygon.Top, position.Y + collider.polygon.Bottom)));
                            destroy = true;
                        }
                    }
                }

                if (destroy) {
                    Destroy();
                    break;
                }

                position += MathUtilities.NormaliseVector2(velocity) * distanceToMove;

                distanceMoved += distanceToMove;
            }
        }
    }
}