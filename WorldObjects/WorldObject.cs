namespace Cosmic.WorldObjects {
    using Cosmic.Items;

    public class WorldObject {
        public ushort id;

        public Sprite sprite;

        public byte life;

        public Item item;

        public virtual void Load() {
        }
    }
}