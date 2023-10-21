namespace Cosmic.Entities.Projectiles
{
    using Cosmic.Entities.Characters;
    using Cosmic.Utilities;
    using System.Collections.Generic;
    using System;
    using Cosmic.Projectiles.Bullets;
    using Cosmic.Worlds;

    public class BulletEntity : ProjectileEntity<Bullet> {
        public override void Update() {
            float distance = velocity.Length();

            for (float distanceMoved = 0f; distanceMoved < distance;) {
                float distanceToMove = Math.Min(1f, distance - distanceMoved);

                bool destroy = false;

                if (collider.GetCollisionWithTiles(out List<WorldTileMapTile> tileMapTiles)) {
                    foreach (WorldTileMapTile tileMapTile in tileMapTiles) {
                        tileMapTile.Mine(1);
                    }

                    destroy = true;
                }

                if (collider.GetCollisionWithEntities(out List<Character> characters)) {
                    foreach (Character character in characters) {
                        if (character is Player == enemy) {
                            character.Hurt(damage, MathUtilities.GetNormalisedVector2(velocity) * strength);
                            destroy = true;
                        }
                    }
                }

                if (destroy) {
                    Destroy();
                    break;
                }

                position += MathUtilities.GetNormalisedVector2(velocity) * distanceToMove;

                distanceMoved += distanceToMove;
            }
        }
    }
}