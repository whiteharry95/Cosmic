namespace Cosmic.Worlds {
    using Cosmic.Entities.Characters.Enemies;
    using System;
    using System.Collections.Generic;

    public class WorldSpawnPool {
        public List<Type> enemyTypes = new List<Type>();

        public void Add<T>(int count = 1) where T : EnemyCharacter {
            for (int i = 0; i < count; i++) {
                enemyTypes.Add(typeof(T));
            }
        }
    }
}