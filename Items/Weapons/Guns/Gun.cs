namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;

    public class Gun : GunWeapon {
        public override void Generate() {
            name = "Gun";

            sprite = new Sprite(AssetManager.gun, new Vector2(0f, AssetManager.gun.Height) / 2f);

            useTime = 6;

            projectileSpeed = 24f;
            projectileDamage = 2;
            projectileOffset = 32f;
        }
    }
}