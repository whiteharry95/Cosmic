namespace Cosmic.Items.Tools.Miners {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Tiles;
    using Cosmic.Worlds;

    public class MinerTool : ToolItem {
        public override void Generate() {
            showTileSelection = true;
        }

        public override void OnUse() {
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                TilemapTile mouseTile = (EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tilemapWalls : WorldManager.worldCurrent.tilemap).GetTile(tilePosition);

                if (mouseTile != null) {
                    mouseTile.Hurt(1);
                }
            }
        }
    }
}