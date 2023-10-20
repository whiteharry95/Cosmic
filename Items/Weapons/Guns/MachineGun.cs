namespace Cosmic.Items.Weapons.Guns {
    using Cosmic.Assets;

    public class MachineGun : Gun {
        public override void Load() {
            name = "Machine Gun";

            sprite = new Sprite(TextureManager.Items_Guns_MachineGun, Sprite.OriginPreset.MiddleLeft);

            useTime = 6;

            projectileOffset = 16f;
            projectileSpeed = 20f;
            projectileDamage = 2;
            projectileStrength = 1f;

            base.Load();
        }
    }
}