namespace Cosmic.Items.Tools.Drills {
    using Microsoft.Xna.Framework;
    using Cosmic.Universes;
    using Cosmic.TileMap;

    public class Drill : Tool {
        public override void Load() {
            showTileSelection = true;
            displayRotation = -MathHelper.Pi / 4f;
        }

        /*public override void OnUse() {
            foreach (Point tilePosition in Game1.server.netPlayers[0].player.tileSelection) {
                TileMapTile mouseTile = (Game1.server.netPlayers[0].player.tileSelectionWalls ? UniverseManager.universeCurrent.worldCurrent.tileMapWalls : UniverseManager.universeCurrent.worldCurrent.tileMap).GetTile(tilePosition);

                if (mouseTile != null) {
                    mouseTile.Hurt(1);
                }
            }
        }*/
    }
}