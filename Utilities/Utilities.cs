namespace Cosmic.Utilities {
    using Cosmic.Entities;
    using Cosmic.Entities.Characters;
    using Cosmic.Tiles;
    using Cosmic.Worlds;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Utilities {
        public static void AddExplosion(World world, Vector2 position, float radius, int damage, float strength) {
            List<CharacterEntity> charactersWithinRadius = EntityManager.GetEntitiesInWorld(world).FindAll(entity => Vector2.Distance(position, entity.position) <= radius).OfType<CharacterEntity>().ToList();

            foreach (CharacterEntity character in charactersWithinRadius) {
                Box boxReal = character.collider.GetBoxReal();

                if (boxReal.GetIntersectsCircle(position, radius)) {
                    Vector2 characterPositionClamped = new Vector2(Math.Clamp(position.X, boxReal.Left, boxReal.Right), Math.Clamp(position.Y, boxReal.Top, boxReal.Bottom));
                    character.Hurt(damage, Vector2.Normalize(characterPositionClamped - position) * strength, characterPositionClamped);
                }
            }

            int tileRadius = (int)(radius / Game1.tileSize);
            List<TilemapTile> tiles = world.tilemap.GetTilesWithinRange(Tilemap.GetWorldToTilePosition(position), tileRadius);

            foreach (TilemapTile tile in tiles) {
                world.tilemap.tiles[tile.X, tile.Y].Hurt(tile.Life);
            }
        }
    }
}