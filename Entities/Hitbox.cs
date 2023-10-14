namespace Cosmic.Entities {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities.Characters;
    using System;
    using System.Collections.Generic;
    using Cosmic.TileMap;
    using Cosmic.Assets;

    public class Hitbox : Entity {
        public int damage;
        public Vector2 force;

        public bool enemy;

        public override void Init() {
            drawLayer = DrawLayer.Debug;

            sprite = new Sprite(TextureManager.Entities_Hitbox, Sprite.OriginPreset.MiddleCentre);
            collider = new EntityCollider(this);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if (collider.GetCollisionWithTiles(out List<TileMapTile> tileMapTiles)) {
                foreach (TileMapTile tileMapTile in tileMapTiles) {
                    tileMapTile.Hurt(1);
                }
            }

            if (collider.GetCollisionWithEntities(out List<Character> characters)) {
                foreach (Character character in characters) {
                    if (character is Player == enemy) {
                        //character.Hurt(damage, force, new Vector2(Math.Clamp(character.position.X, position.X + collider.polygon.Left, position.X + collider.polygon.Right), Math.Clamp(character.position.Y, position.Y + collider.polygon.Top, position.Y + collider.polygon.Bottom)));
                    }
                }
            }

            Destroy();
        }
    }
}