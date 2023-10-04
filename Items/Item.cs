namespace Cosmic.Items
{
    public abstract class Item {
        public int id;

        public string name;

        public Sprite sprite;

        public int useTime;
        public bool useHold = true;

        public int stack = 1;

        public bool showTileSelection;

        public float holdLengthOffset;
        public float holdRotationOffset;

        public float displayRotation;

        public abstract void Load();

        public virtual void OnUse() {
        }
    }
}