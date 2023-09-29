﻿namespace Cosmic.UI.UIElements {
    using Microsoft.Xna.Framework;

    public abstract class UIElement {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}