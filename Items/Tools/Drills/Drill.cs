namespace Cosmic.Items.Tools.Drills {
    using Microsoft.Xna.Framework;
    using Cosmic.TileMap;
    using Cosmic.Entities;
    using System;

    public class Drill : Tool {
        public override void Load() {
            showTileSelection = true;
            displayRotation = -MathF.PI / 4f;
        }

        public override void OnPrimaryUse() {
            Mine(false);
        }

        public override void OnSecondaryUse() {
            Mine(true);
        }

        public void Mine(bool wall) {
            foreach (Point tilePosition in EntityManager.player.tileSelection) {
                TileMapTile mouseTile = (wall ? EntityManager.player.world.tileMapWalls : EntityManager.player.world.tileMap).GetTile(tilePosition.X, tilePosition.Y);

                if (mouseTile != null) {
                    mouseTile.Mine(1);
                }
            }
        }
    }
}