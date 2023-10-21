namespace Cosmic.Entities {
    using System;

    public class DamageText : Entity {
        public int damage;

        public float moveSpeedChange = 0.25f;

        public float alpha = 1f;
        public float alphaChange = 0.2f;

        public override void Init() {
            velocity.Y = -(Game1.Random.Next(40, 61) / 10f);
        }

        public override void Update() {
            if (velocity.Y == 0f) {
                if (alpha > 0f) {
                    alpha -= Math.Min(alphaChange, alpha);
                }

                if (alpha == 0f) {
                    Destroy();
                }
            }

            velocity.Y += Math.Min(moveSpeedChange, Math.Abs(velocity.Y)) * Math.Sign(-velocity.Y);

            base.Update();
        }

        public override void Draw() {
        }
    }
}