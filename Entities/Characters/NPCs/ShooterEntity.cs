namespace Cosmic.Entities.Characters.NPCs {
    using Cosmic.Entities.Projectiles;
    using Cosmic.NPCs.Shooters;
    using Cosmic.Projectiles;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework;
    using System;

    public class ShooterEntity : NPCEntity<Shooter> {
        public float moveSpeedChange = 0.25f;

        public int shootTime;

        public override void Update(GameTime gameTime) {
            velocity.X += Math.Min(moveSpeedChange, Math.Abs(velocity.X)) * -Math.Sign(velocity.X);
            velocity.Y += Math.Min(world.fallSpeed, Math.Abs(world.fallSpeedMax - velocity.Y)) * Math.Sign(world.fallSpeedMax - velocity.Y);

            if (velocity.X != 0f) {
                if (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f))) {
                    collider.MakeContactWithTiles(Math.Abs(velocity.X), velocity.X >= 0f ? MathUtilities.Direction.Right : MathUtilities.Direction.Left);
                    velocity.X = 0f;
                }
            }

            if (collider.GetCollisionWithTiles(new Vector2(0f, velocity.Y))) {
                collider.MakeContactWithTiles(Math.Abs(velocity.Y), velocity.Y >= 0f ? MathUtilities.Direction.Down : MathUtilities.Direction.Up);
                velocity.Y = 0f;
            }

            if (collider.GetCollisionWithTiles(velocity)) {
                velocity.X = 0f;
            }

            base.Update(gameTime);

            if (shootTime < nPC.shootTime) {
                shootTime++;
            } else {
                Vector2 bulletDirection = MathUtilities.NormaliseVector2(Game1.server.netPlayers[0].player.position - position);

                if (bulletDirection != Vector2.Zero) {
                    BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(position, world, projectileEntity => projectileEntity.projectile = ProjectileManager.copperBullet);
                    bullet.velocity = bulletDirection * 16f;
                    bullet.damage = 3;
                    bullet.strength = 5f;
                    bullet.enemy = true;
                }

                shootTime = 0;
            }
        }
    }
}