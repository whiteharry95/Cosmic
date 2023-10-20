namespace Cosmic.WorldObjects {
    using Cosmic.Items;

    public class WorldObject {
        public enum PlaceType {
            Floor,
            Ceiling
        }

        public ushort id;

        public Sprite sprite;

        public byte life;

        public Item item;

        public PlaceType placeType;

        public virtual void Load() {
        }
    }
}