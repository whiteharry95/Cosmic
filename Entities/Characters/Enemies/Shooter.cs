namespace Cosmic.Entities.Characters.Enemies {
    using Cosmic.Items;
    using Microsoft.Xna.Framework;
    using System;

    public class Shooter : EnemyCharacter {
        public float moveSpeedChange = 0.25f;

        public int shootTime;
        public int shootTimeMax = 60;

        public override void Init() {
            healthMax = 35;
            health = healthMax;

            sprite = new Sprite(AssetManager.shooter, new Vector2(AssetManager.shooter.Width, AssetManager.shooter.Height) / 2f);
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size.ToVector2()));
        }

        public override void Update(GameTime gameTime) {
            velocity.X += Math.Min(moveSpeedChange, Math.Abs(velocity.X)) * -Math.Sign(velocity.X);
            velocity.Y += Math.Min(world.fallSpeed, Math.Abs(world.fallSpeedMax - velocity.Y)) * Math.Sign(world.fallSpeedMax - velocity.Y);

            if (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f))) {
                collider.MakeContactWithTiles(Math.Abs(velocity.X), velocity.X >= 0f ? 0 : 2);
                velocity.X = 0f;
            }

            if (collider.GetCollisionWithTiles(new Vector2(0f, velocity.Y))) {
                collider.MakeContactWithTiles(Math.Abs(velocity.Y), velocity.Y >= 0f ? 3 : 1);
                velocity.Y = 0f;
            }

            if (collider.GetCollisionWithTiles(velocity)) {
                velocity.X = 0f;
            }

            base.Update(gameTime);

            if (shootTime < shootTimeMax) {
                shootTime++;
            } else {
                Projectile projectile = EntityManager.AddEntity<Projectile>(position, world);
                projectile.velocity = Vector2.Normalize(EntityManager.player.position - position) * 16f;
                projectile.damage = 3;
                projectile.enemy = true;

                shootTime = 0;
            }
        }

        public override void Destroy() {
            ItemDrop itemDrop = EntityManager.AddEntity<ItemDrop>(position, world);
            itemDrop.item = ItemManager.beef;

            base.Destroy();
        }
    }
}