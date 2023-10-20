namespace Cosmic.Items.Weapons.Swords {
    using Cosmic.Assets;
    using System;

    public class WoodenSword : Sword {
        public override void Load() {
            name = "Wooden Sword";

            sprite = new Sprite(TextureManager.Items_Swords_WoodenSword, Sprite.OriginPreset.MiddleLeft);

            useTime = 15;

            holdRotationOffset = MathF.PI * (7f / 12f);

            hitboxDamage = 7;
            hitboxStrength = 2.5f;
            hitboxOffset = 24f;

            base.Load();
        }
    }
}