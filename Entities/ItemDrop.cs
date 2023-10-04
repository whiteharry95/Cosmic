namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using System;
    using Cosmic.Items;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework.Graphics;

    public class ItemDrop : Entity {
        public Item item;
        public int quantity = 1;

        public float moveSpeedChange = 0.25f;

        public bool pickup;
        public float pickupDistance = 80f;
        public float pickupSpeed = 10f;
        public int pickupTime;
        public int pickupTimeMax = 60;

        public int shootTime;

        public SpriteEffects flip;

        public override void Init() {
            drawLayer = DrawLayer.ItemDrops;

            sprite = new Sprite(item.sprite.texture, Sprite.OriginPreset.MiddleCentre);
            collider = new EntityCollider(this, new Box(-sprite.origin / 2f, sprite.Size / 2f));
        }

        public override void Update(GameTime gameTime) {
            bool pickupPrevious = pickup;

            if (pickupTime > 0) {
                pickupTime--;
            }

            if (pickupTime <= 0) {
                if (EntityManager.player.GetExists() && EntityManager.player.world == world) {
                    if (Vector2.Distance(position, EntityManager.player.position) <= pickupDistance) {
                        pickup = true;
                    }
                } else {
                    pickup = false;
                }
            }

            if (pickup != pickupPrevious) {
                velocity = Vector2.Zero;
            }

            if (pickup) {
                velocity = MathUtilities.NormaliseVector2(EntityManager.player.position - position) * pickupSpeed;
            } else {
                velocity.X += Math.Min(moveSpeedChange, Math.Abs(velocity.X)) * -Math.Sign(velocity.X);
                velocity.Y += Math.Min(world.fallSpeed, Math.Abs(world.fallSpeedMax - velocity.Y)) * Math.Sign(world.fallSpeedMax - velocity.Y);

                if (velocity.X != 0f) {
                    if (collider.GetCollisionWithTiles(new Vector2(velocity.X, 0f))) {
                        collider.MakeContactWithTiles(Math.Abs(velocity.X), velocity.X >= 0f ? 0 : 2);
                        velocity.X = 0f;
                    }
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

            if (position.X < 0f || position.Y < 0f || position.X >= world.Width || position.Y >= world.Height) {
                Destroy();
            }
        }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(position - Camera.position, scale: 0.5f, flip: flip, origin: item.sprite.Size / 2f);
        }
    }
}