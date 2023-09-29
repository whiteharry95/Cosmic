namespace Cosmic.Items {
    using Microsoft.Xna.Framework.Content;

    public abstract class Item {
        public int id;

        public string name;

        public Sprite sprite;

        public int useTime;

        public int stack = 1;

        public bool showTileSelection;

        public float holdLengthOffset;
        public float holdRotationOffset;

        public virtual void Load(ContentManager contentManager) {
        }

        public virtual void OnUse() {
        }
    }
}