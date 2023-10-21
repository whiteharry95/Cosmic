namespace Cosmic {
    using Cosmic.NPCs;
    using Cosmic.Entities;
    using Cosmic.Items;
    using Cosmic.Projectiles;
    using Cosmic.Tiles;
    using Cosmic.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using Cosmic.Assets;
    using Cosmic.WorldObjects;
    using Cosmic.Worlds;

    public class Game1 : Game {
        public static new GraphicsDevice GraphicsDevice { get; private set; }
        public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static Random Random { get; private set; } = new Random();

        public Game1() {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            GraphicsDeviceManager.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            GraphicsDevice = base.GraphicsDevice;

            TileManager.Init();
            WorldObjectManager.Init();
            ItemManager.Init();
            ProjectileManager.Init();
            NPCManager.Init();
            EntityManager.Init();

            base.Initialize();
        }

        protected override void LoadContent() {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.Load(Content);
            FontManager.Load(Content);

            TileManager.Load();
            WorldObjectManager.Load();
            ItemManager.Load();
            ProjectileManager.Load();
            NPCManager.Load();

            WorldManager.Load();
            WorldManager.Generate();

            EntityManager.Load();
            
            UIManager.Load();
        }

        protected override void UnloadContent() {
            TextureManager.Unload();
        }

        protected override void Update(GameTime gameTime) {
            InputManager.Update();

            if (InputManager.GetKeyPressed(Keys.R)) {
                Restart();
            }

            UIManager.EarlyUpdate();

            WorldManager.Update();
            EntityManager.Update();
            Camera.Update();
            UIManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.CreateTranslation(new Vector3(-Camera.Position, 0f)) * Matrix.CreateScale(new Vector3(new Vector2(Camera.Scale), 0f)));

            WorldManager.Draw();
            EntityManager.Draw();

            SpriteBatch.End();

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            UIManager.Draw();

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void Restart() {
            TileManager.Init();
            WorldObjectManager.Init();
            ItemManager.Init();
            ProjectileManager.Init();
            NPCManager.Init();
            EntityManager.Init();

            TileManager.Load();
            WorldObjectManager.Load();
            ItemManager.Load();
            ProjectileManager.Load();
            NPCManager.Load();

            WorldManager.Load();
            WorldManager.Generate();

            UIManager.Load();

            Camera.Load();
        }
    }
}