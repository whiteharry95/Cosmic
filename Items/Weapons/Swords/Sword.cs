namespace Cosmic.Items.Weapons.Guns {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using System;

    public class Sword : SwordWeapon {
        public override void Load(ContentManager contentManager) {
            name = "Sword";

            sprite = new Sprite(AssetManager.sword, new Vector2(0f, AssetManager.sword.Height) / 2f);

            useTime = 15;

            holdRotationOffset = (float)(Math.PI * (7f / 12f));

            hitboxDamage = 4;
            hitboxStrength = 4f;
            hitboxOffset = 48f;
        }
    }
}