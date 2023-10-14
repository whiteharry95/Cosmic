using Cosmic.Tiles;

namespace Cosmic.TileMap {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic;

    public class TileMapTile {
        public Tile tile;

        public TileMap tileMap;

        public int x;
        public int y;

        public ushort life;

        public TileMapTile(Tile tile, TileMap tileMap, int x, int y) {
            this.tile = tile;

            this.tileMap = tileMap;

            this.x = x;
            this.y = y;

            life = tile.life;
        }

        public void Hurt(byte damage) {
            life -= damage;

            if (life <= 0) {
                if (tile.item != null) {
                    EntityManager.AddEntity<ItemDrop>((new Point(x, y).ToVector2() + new Vector2(0.5f)) * Tile.Size, tileMap.world, itemDrop => itemDrop.item = tile.item);
                }

                tileMap.tiles[x, y] = null;
                life = 0;
            }
        }

        public Polygon GetPolygon() {
            return new Polygon(() => Polygon.GetRectangleVertices(new Vector2(Tile.Size)), () => new Vector2(x, y) * Tile.Size);
        }
    }
}