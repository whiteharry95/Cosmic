namespace Cosmic.Items.Food {
    using Microsoft.Xna.Framework;

    public class Beef : FoodItem {
        public override void Generate() {
            name = "Beef";

            sprite = new Sprite(AssetManager.beef, new Vector2(AssetManager.beef.Width, AssetManager.beef.Height) / 2f);

            base.Generate();
        }
    }
}