namespace Cosmic.NPCs
{
    public abstract class NPC {
        public int id;

        public string name;

        public Sprite sprite;

        public int health;

        public int invincibilityTime;

        public abstract void Load();
    }
}