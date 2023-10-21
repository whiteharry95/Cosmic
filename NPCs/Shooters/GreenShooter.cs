namespace Cosmic.NPCs.Shooters {
    using Cosmic.Assets;

    public class GreenShooter : Shooter {
        public override void Load() {
            name = "Green Shooter";

            sprite = new Sprite(TextureManager.Characters_NPCs_Shooters_GreenShooter, Sprite.OriginPreset.MiddleCentre);

            health = 35;

            shootTime = 30;

            base.Load();
        }
    }
}