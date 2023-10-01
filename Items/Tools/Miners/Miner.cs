namespace Cosmic.Items.Tools.Miners {
    using Microsoft.Xna.Framework;

    public class Miner : MinerTool {
        public override void Generate() {
            name = "Miner";

            sprite = new Sprite(AssetManager.miner, new Vector2(0f, AssetManager.miner.Height / 2f));

            base.Generate();
        }
    }
}