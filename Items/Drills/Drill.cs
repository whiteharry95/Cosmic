namespace Cosmic.Items.Drills {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Worlds;
    using Cosmic.TileMap;

    public class Drill : Item {
        public override void Load() {
            showTileSelection = true;
            displayRotation = -MathHelper.Pi / 4f;
        }

        public override void OnUse() {
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                TileMapTile mouseTile = (EntityManager.player.tileSelectionWalls ? WorldManager.worldCurrent.tileMapWalls : WorldManager.worldCurrent.tileMap).GetTile(tilePosition);

                if (mouseTile != null) {
                    mouseTile.Hurt(1);
                }
            }
        }
    }
}