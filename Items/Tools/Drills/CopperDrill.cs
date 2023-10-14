namespace Cosmic.Items.Tools.Drills {
    using Cosmic.Assets;

    public class CopperDrill : Drill {
        public override void Load() {
            name = "Copper Drill";

            sprite = new Sprite(TextureManager.Items_Drills_CopperDrill, Sprite.OriginPreset.MiddleLeft);

            base.Load();
        }
    }
}