namespace Cosmic.Items.Weapons.Guns {
    using Cosmic.Assets;

    public class MachineGun : Gun {
        public override void Load() {
            name = "Machine Gun";

            sprite = new Sprite(TextureManager.Items_Guns_MachineGun, Sprite.OriginPreset.MiddleLeft);

            useTime = 6;

            projectileOffset = 32f;
            projectileSpeed = 24f;
            projectileDamage = 2;
            projectileStrength = 2f;

            base.Load();
        }
    }
}