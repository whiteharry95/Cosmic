namespace Cosmic.Items.Tools.Miners {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class Miner : MinerTool {
        public override void Load(ContentManager contentManager) {
            name = "Miner";

            sprite = new Sprite(AssetManager.miner, new Vector2(0f, AssetManager.miner.Height / 2f));

            base.Load(contentManager);
        }
    }
}