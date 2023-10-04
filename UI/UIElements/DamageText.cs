﻿namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;
    using Cosmic.Entities;
    using Cosmic.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using Cosmic.Assets;

    public class DamageText : UIElement {
        public override void Update(GameTime gameTime) {
        }

        public override void Draw(GameTime gameTime) {
            List<Entities.DamageText> damageTexts = EntityManager.GetEntitiesInWorld().OfType<Entities.DamageText>().ToList();

            foreach (Entities.DamageText damageText in damageTexts) {
                DrawUtilities.DrawText(FontManager.ArialSmall, (-damageText.damage).ToString(), damageText.position - Camera.position, Color.White * damageText.alpha, DrawUtilities.HorizontalAlignment.Centre, DrawUtilities.VerticalAlignment.Middle);
            }
        }
    }
}