namespace Cosmic.Items.Weapons.Swords {
    using Cosmic.Assets;
    using Microsoft.Xna.Framework;

    public class WoodenSword : Sword {
        public override void Load() {
            name = "Wooden Sword";

            sprite = new Sprite(TextureManager.Items_Swords_WoodenSword, Sprite.OriginPreset.MiddleLeft);

            useTime = 15;

            holdRotationOffset = MathHelper.Pi * (7f / 12f);

            hitboxDamage = 7;
            hitboxStrength = 5f;
            hitboxOffset = 48f;

            base.Load();
        }
    }
}