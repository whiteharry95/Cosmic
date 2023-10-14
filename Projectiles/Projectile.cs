namespace Cosmic.Projectiles
{
    public abstract class Projectile {
        public ushort id;

        public Sprite sprite;

        public abstract void Load();
    }
}