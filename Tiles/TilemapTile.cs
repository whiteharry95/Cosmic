namespace Cosmic.Tiles {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;

    public class TilemapTile {
        public Tile Tile { get; private set; }

        public Tilemap Tilemap { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public byte Life { get; private set; }

        public byte Time { get; private set; }

        public TilemapTile(Tile tile, Tilemap tilemap, int x, int y) {
            Tile = tile;

            Tilemap = tilemap;

            X = x;
            Y = y;

            Life = tile.life;
        }

        public void Update(GameTime gameTime) {
            Tile.Update(gameTime);

            Time++;
            Time %= Tile.timeMax;
        }

        public void Hurt(byte damage) {
            Life -= damage;

            if (Life <= 0) {
                if (Tile.item != null) {
                    ItemDrop itemDrop = EntityManager.AddEntity<ItemDrop>((new Point(X, Y).ToVector2() + new Vector2(0.5f)) * Game1.tileSize, Tilemap.world);
                    itemDrop.item = Tile.item;
                }

                Tilemap.tiles[X, Y] = null;
                Life = 0;
            }
        }

        public Box GetBox() {
            return new Box(X * Game1.tileSize, Y * Game1.tileSize, Game1.tileSize, Game1.tileSize);
        }
    }
}