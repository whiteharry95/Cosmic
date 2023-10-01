namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using System;

    public class Sword : SwordWeapon {
        public override void Generate() {
            name = "Sword";

            sprite = new Sprite(AssetManager.sword, new Vector2(0f, AssetManager.sword.Height) / 2f);

            useTime = 15;

            holdRotationOffset = (float)(Math.PI * (7f / 12f));

            hitboxDamage = 8;
            hitboxStrength = 4f;
            hitboxOffset = 48f;
        }
    }
}