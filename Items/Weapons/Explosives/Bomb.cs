namespace Cosmic.Items.Weapons.Explosives {
    using Microsoft.Xna.Framework;

    public class Bomb : ExplosiveWeapon {
        public override void Generate() {
            name = "Bomb";

            sprite = new Sprite(AssetManager.bomb, new Vector2(AssetManager.bomb.Width, AssetManager.bomb.Height) / 2f);

            useTime = 6;

            projectileSpeed = 24f;
            projectileDamage = 2;
            projectileOffset = 32f;
        }
    }
}