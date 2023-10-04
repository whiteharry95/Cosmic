namespace Cosmic.Projectiles.Bullets {
    using Cosmic.Assets;

    public class CopperBullet : Bullet {
        public override void Load() {
            sprite = new Sprite(TextureManager.Textures_Projectiles_Bullets_CopperBullet, Sprite.OriginPreset.MiddleCentre);

            base.Load();
        }
    }
}