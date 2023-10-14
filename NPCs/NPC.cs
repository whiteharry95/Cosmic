namespace Cosmic.NPCs {
    public abstract class NPC {
        public ushort id;

        public string name;

        public Sprite sprite;

        public int health;

        public int invincibilityTime;

        public abstract void Load();
    }
}