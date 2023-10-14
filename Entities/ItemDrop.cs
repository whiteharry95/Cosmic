namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic;
    using System;
    using Cosmic.Items;
    using Cosmic.Utilities;
    using Microsoft.Xna.Framework.Graphics;
    using Cosmic.Entities.Characters;

    public class ItemDrop : Entity {
        public Item item;
        public int quantity = 1;

        public float moveSpeedChange = 0.25f;

        public bool pickup;
        public float pickupDistance = 80f;
        public float pickupSpeed = 10f;
        public int pickupTime;
        public int pickupTimeMax = 60;
        public Player pickupPlayer;

        public int shootTime;

        public SpriteEffects flip;

        public override void Init() {
            drawLayer = DrawLayer.ItemDrops;

            sprite = new Sprite(item.sprite.texture, Sprite.OriginPreset.MiddleCentre);

            collider = new EntityCollider(this);
        }

        public override void Update(GameTime gameTime) {
            bool pickupPrevious = pickup;

            if (pickupTime > 0) {
                pickupTime--;
            }

            if (pickupTime == 0) {
                pickup = false;

                for (int i = 0; i < EntityManager.players.Length; i++) {
                    if (EntityManager.players[i] == null) {
                        continue;
                    }

                    if (EntityManager.players[i].GetExists() && EntityManager.players[i].world == world) {
                        if (Vector2.Distance(position, EntityManager.players[i].position) <= pickupDistance) {
                            pickup = true;
                            pickupPlayer = EntityManager.players[i];
                            break;
                        }
                    }
                }
            }

            if (pickup != pickupPrevious) {
                velocity = Vector2.Zero;
            }

            if (pickup) {
                velocity = MathUtilities.NormaliseVector2(pickupPlayer.position - position) * pickupSpeed;
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

            base.Update(gameTime);

            if (position.X < 0f || position.Y < 0f || position.X >= world.Width || position.Y >= world.Height) {
                Destroy();
            }
        }

        public override void Draw(GameTime gameTime) {
            sprite.Draw(position - Camera.position, scale: new Vector2(0.5f), flip: flip, origin: item.sprite.Size / 2f);
        }
    }
}