namespace Cosmic.Projectiles {
    using Cosmic.Projectiles.Bullets;
    using System;
    using System.Collections.Generic;

    public class ProjectileManager {
        private static List<Projectile> projectiles;

        public static CopperBullet copperBullet;

        public static void Init() {
            projectiles = new List<Projectile>();

            copperBullet = AddProjectile<CopperBullet>();
        }

        public static void Load() {
            foreach (Projectile projectile in projectiles) {
                projectile.Load();
            }
        }

        public static T AddProjectile<T>(bool load = false) where T : Projectile {
            T projectile = Activator.CreateInstance<T>();
            projectile.id = (ushort)projectiles.Count;

            if (load) {
                projectile.Load();
            }

            projectiles.Add(projectile);

            return projectile;
        }
    }
}