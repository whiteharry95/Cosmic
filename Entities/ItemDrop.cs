namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using System;
    using Cosmic.Items;

    public class ItemDrop : Entity {
        public Item item;
        public int quantity = 1;

        public float fallSpeedMax = 40f;

        public bool pickup;
        public float pickupDistance = 80f;
        public float pickupSpeed = 10f;

        public override void Init() {
            drawPriority = 2;

            sprite = new Sprite(AssetManager.itemDrop, new Vector2(AssetManager.itemDrop.Width, AssetManager.itemDrop.Height) / 2f);
            collider = new EntityCollider(this, new Box(-sprite.origin, sprite.Size.ToVector2()));
        }

        public override void Update(GameTime gameTime) {
            bool pickupPrevious = pickup;

            if (EntityManager.player.GetExists() && EntityManager.player.world == world) {
                if (Vector2.Distance(position, EntityManager.player.position) <= pickupDistance) {
                    pickup = true;
                }
            } else {
                pickup = false;
            }

            if (pickup != pickupPrevious) {
                velocity = Vector2.Zero;
            }

            if (pickup) {
                velocity = Vector2.Normalize(EntityManager.player.position - position) * pickupSpeed;
            } else {
                velocity.Y += Math.Min(world.fallSpeed, Math.Abs(fallSpeedMax - velocity.Y)) * Math.Sign(fallSpeedMax - velocity.Y);

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
            }

            base.Update(gameTime);
        }
    }
}