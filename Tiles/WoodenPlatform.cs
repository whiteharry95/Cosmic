namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Items;

    public class WoodenPlatform : Tile {
        public override void Load(ContentManager contentManager) {
            texture = TileManager.CreateTileTexture(Color.Purple);
            
            life = 20;

            item = ItemManager.woodenPlatform;

            platform = true;
        }
    }
}