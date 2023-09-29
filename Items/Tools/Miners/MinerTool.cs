namespace Cosmic.Items.Tools.Miners {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Cosmic.Entities;
    using Cosmic.Tiles;
    using Cosmic.Worlds;

    public class MinerTool : ToolItem {
        public override void Load(ContentManager contentManager) {
            showTileSelection = true;
        }

        public override void OnUse() {
            foreach (Point tileSelection in EntityManager.player.tileSelection) {
                TilemapTile mouseTile = (EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tilemapWalls : WorldManager.worldCurrent.tilemap).GetTile(tileSelection);

                if (mouseTile != null) {
                    mouseTile.Hurt(1);
                }
            }
        }
    }
}