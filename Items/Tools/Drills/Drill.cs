namespace Cosmic.Items.Tools.Drills {
    using Cosmic.Entities;
    using System;
    using Cosmic.Worlds;

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
            for (int y = 0; y < EntityManager.Player.TileSelection.GetLength(1); y++) {
                for (int x = 0; x < EntityManager.Player.TileSelection.GetLength(0); x++) {
                    if (EntityManager.Player.TileSelection[x, y]) {
                        WorldTileMapTile mouseTile = (wall ? EntityManager.Player.world.TileMapWalls : EntityManager.Player.world.TileMap).GetTile(EntityManager.Player.TileSelectionPosition.X + x, EntityManager.Player.TileSelectionPosition.Y + y);

                        if (mouseTile != null) {
                            mouseTile.Mine(1);
                        }
                    }
                }
            }
        }
    }
}