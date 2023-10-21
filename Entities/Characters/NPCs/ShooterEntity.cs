namespace Cosmic.Entities.Characters.NPCs {
    using Cosmic.Entities.Projectiles;
    using Cosmic.NPCs.Shooters;
    using Cosmic.Projectiles;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework;
    using System;

    public class ShooterEntity : NPCEntity<Shooter> {
        public float moveSpeedChange = 0.2f;

        public int shootTime;

        public override void Update() {
            velocity.X += Math.Min(moveSpeedChange, Math.Abs(velocity.X)) * -Math.Sign(velocity.X);
            velocity.Y += Math.Min(world.FallSpeed, Math.Abs(world.FallSpeedMax - velocity.Y)) * Math.Sign(world.FallSpeedMax - velocity.Y);

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

            base.Update();

            if (shootTime < nPC.shootTime) {
                shootTime++;
            } else {
                Vector2 bulletDirection = MathUtilities.GetNormalisedVector2(EntityManager.Player.position - position);

                if (bulletDirection != Vector2.Zero) {
                    BulletEntity bullet = EntityManager.AddEntity<BulletEntity>(position, world, projectileEntity => projectileEntity.projectile = ProjectileManager.copperBullet);
                    bullet.rotation = MathF.Atan2(EntityManager.Player.position.Y - position.Y, EntityManager.Player.position.X - position.X);
                    bullet.velocity = bulletDirection * 10f;
                    bullet.damage = 3;
                    bullet.strength = 2.5f;
                    bullet.enemy = true;
                }

                shootTime = 0;
            }
        }
    }
}