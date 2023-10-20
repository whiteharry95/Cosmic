namespace Cosmic.Entities {
    using Cosmic.WorldObjects;
    using Microsoft.Xna.Framework;

    public class WorldObjectEntity : Entity {
        public WorldObject worldObject;

        public override void Init() {
            drawLayer = DrawLayer.WorldObjects;

            sprite = new Sprite(worldObject.sprite.texture, Vector2.Zero);
            collider = new EntityCollider(this);
        }
    }
}