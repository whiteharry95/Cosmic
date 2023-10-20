namespace Cosmic.Projectiles.Bullets {
    using Cosmic.Assets;

    public class CopperBullet : Bullet {
        public override void Load() {
            sprite = new Sprite(TextureManager.Projectiles_Bullets_CopperBullet, Sprite.OriginPreset.MiddleLeft);

            base.Load();
        }
    }
}