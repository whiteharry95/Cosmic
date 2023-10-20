namespace Cosmic.UI.UIElements {
    public abstract class UIElement {
        public virtual void EarlyUpdate() {
        }

        public abstract void Update();
        public abstract void Draw();
    }
}