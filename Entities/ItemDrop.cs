namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using System;
    using Cosmic.Items;
    using Cosmic.Utilities;

    public class ItemDrop : Entity {
        public Item item;
        public int quantity = 1;

        public float moveSpeedChange = 0.2f;

        public bool pickup;
        public float pickupDistance = 40f;
        public float pickupSpeed = 5f;
        public int pickupTime;
        public int pickupTimeMax = 60;

        public int shootTime;

        public override void Init() {
            scale = new Vector2(0.5f);

            drawLayer = DrawLayer.ItemDrops;

            sprite = new Sprite(item.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            collider = new EntityCollider(this);
        }

        public override void Update() {
            bool pickupPrevious = pickup;

            if (pickupTime > 0) {
                pickupTime--;
            }

            if (pickupTime == 0) {
                pickup = false;

                if ((EntityManager.player?.GetExists() ?? false) && EntityManager.player?.world == world) {
                    if (Vector2.Distance(position, EntityManager.player.position) <= pickupDistance) {
                        pickup = true;
                    }
                }
            }

            if (pickup != pickupPrevious) {
                velocity = Vector2.Zero;
            }

            if (pickup) {
                velocity = MathUtilities.GetNormalisedVector2(EntityManager.player.position - position) * pickupSpeed;
            } else {
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
            }

            base.Update();
        }
    }
}