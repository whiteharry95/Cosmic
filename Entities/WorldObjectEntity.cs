namespace Cosmic.Entities {
    using Cosmic;
    using Cosmic.WorldObjects;

    public class WorldObjectEntity : Entity {
        public WorldObject worldObject;

        public override void Init() {
            drawLayer = DrawLayer.WorldObjects;

            sprite = worldObject.sprite;
            collider = new EntityCollider(this);
        }
    }
}