namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;

    public class Gun : GunWeapon {
        public override void Load(ContentManager contentManager) {
            name = "Gun";

            sprite = new Sprite(AssetManager.gun, new Vector2(0f, AssetManager.gun.Height) / 2f);

            useTime = 6;

            projectileSpeed = 24f;
            projectileDamage = 2;
            projectileStrength = 2f;
            projectileOffset = 32f;
        }
    }
}