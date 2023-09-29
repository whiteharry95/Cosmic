namespace Cosmic.Worlds {
    using Microsoft.Xna.Framework;
    using Cosmic.Tiles;
    using System;
    using System.Collections.Generic;

    public class World {
        public int Width => tilemap.Width * Game1.tileSize;
        public int Height => tilemap.Height * Game1.tileSize;

        public Tilemap tilemap;
        public Tilemap tilemapWalls;

        public List<Action> generationActions = new List<Action>();

        public float fallSpeed = 0.4f;
        public float fallSpeedMax = 32f;

        public int spawnTime;

        public World(int tilemapWidth, int tilemapHeight) {
            tilemap = new Tilemap(tilemapWidth, tilemapHeight, this);

            tilemapWalls = new Tilemap(tilemapWidth, tilemapHeight, this);
            tilemapWalls.tilesBrightness = 0.4f;
        }

        public virtual void Update(GameTime gameTime) {
            /*biomes = biomes.OrderBy(biome => biome.priority).ToList();

            foreach (WorldBiome biome in biomes) {
                if (biome.prerequisite()) {
                    biomeCurrent = biome;
                }
            }

            int spawnPoints = 0;

            List<EnemyCharacter> enemies = EntityManager.GetEntitiesInWorld(this).OfType<EnemyCharacter>().ToList();

            foreach (EnemyCharacter enemy in enemies) {
                spawnPoints += enemy.spawnPoints;
            }

            if (biomeCurrent.GetSpawnPoolSize() > 0) {
                if (spawnTime < biomeCurrent.spawnTimeMax) {
                    spawnTime++;
                } else {
                    if (spawnPoints < biomeCurrent.spawnPointsMax) {
                        Box worldBox = GetBox();
                        float worldBoxOffset = 32f;

                        Box cameraBox = Camera.GetBox();
                        float cameraBoxOffset = -32f;

                        Box cameraBoxSpawn = Camera.GetBox();
                        float cameraBoxSpawnOffset = -256f;

                        cameraBoxSpawn.Position += new Vector2(cameraBoxSpawnOffset);
                        cameraBoxSpawn.Size -= new Vector2(cameraBoxSpawnOffset * 2f);

                        EnemyCharacter enemy = (EnemyCharacter)EntityManager.AddEntity(biomeCurrent.GetRandomTypeFromSpawnPool(), Vector2.Zero, this);
                        Vector2 enemyPositionBounded;

                        int spawnTrials = 0;

                        do {
                            enemy.position = cameraBoxSpawn.Position + new Vector2(Game1.random.Next((int)cameraBoxSpawn.width), Game1.random.Next((int)cameraBoxSpawn.height));

                            Box enemyBoxReal = enemy.collider.GetBoxReal();

                            enemyPositionBounded = new Vector2(Math.Clamp(cameraBox.Centre.X, enemyBoxReal.Left, enemyBoxReal.Right), Math.Clamp(cameraBox.Centre.Y, enemyBoxReal.Top, enemyBoxReal.Bottom));

                            spawnTrials++;

                            if (spawnTrials >= 1000) {
                                enemy.Destroy();
                                break;
                            }
                        } while (cameraBox.GetContains(enemyPositionBounded, cameraBoxOffset) || !worldBox.GetContains(enemyPositionBounded, worldBoxOffset) || enemy.collider.GetCollisionWithTiles());
                    }

                    spawnTime = 0;
                }
            }*/

            tilemapWalls.Update(gameTime);
            tilemap.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime) {
            tilemapWalls.Draw(gameTime);
            tilemap.Draw(gameTime);
        }

        public void Generate() {
            foreach (Action generationAction in generationActions) {
                generationAction();
            }
        }

        public Box GetBox() {
            return new Box(Vector2.Zero, new Vector2(Width, Height));
        }
    }
}