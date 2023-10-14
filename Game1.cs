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
    using System.Collections.Generic;
    using Cosmic.Assets;
    using Cosmic.WorldObjects;
    using System.Net;

    public class Game1 : Game {
        public static GraphicsDevice graphicsDeviceStatic;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static SpriteBatch spriteBatch;

        public static Random random = new Random();

        public static List<Texture2D> texturesToUnload = new List<Texture2D>();

        public static Server.Server server = new Server.Server(IPAddress.Parse("127.0.0.1"), 13000);

        public Game1() {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = 1920;
            graphicsDeviceManager.PreferredBackBufferHeight = 1080;
            graphicsDeviceManager.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            graphicsDeviceStatic = GraphicsDevice;

            TileManager.Init();
            WorldObjectManager.Init();
            ItemManager.Init();
            ProjectileManager.Init();
            NPCManager.Init();

            server.Start();

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.Load(Content);
            FontManager.Load(Content);

            TileManager.Load();
            WorldObjectManager.Load();
            ItemManager.Load();
            ProjectileManager.Load();
            NPCManager.Load();

            EntityManager.Load(Content);
            UIManager.Load(Content);
        }

        protected override void UnloadContent() {
            foreach (Texture2D textureToUnload in texturesToUnload) {
                textureToUnload.Dispose();
            }
        }

        protected override void Update(GameTime gameTime) {
            InputManager.Update(gameTime);

            if (InputManager.GetKeyPressed(Keys.R)) {
                Restart();
            }

            UIManager.EarlyUpdate(gameTime);

            EntityManager.Update(gameTime);
            Camera.Update(gameTime);
            UIManager.Update(gameTime);

            server.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            EntityManager.Draw(gameTime);
            UIManager.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void Restart() {
            TileManager.Init();
            WorldObjectManager.Init();
            ItemManager.Init();
            ProjectileManager.Init();
            NPCManager.Init();

            TileManager.Load();
            WorldObjectManager.Load();
            ItemManager.Load();
            ProjectileManager.Load();
            NPCManager.Load();

            EntityManager.Load(Content);
            UIManager.Load(Content);

            Camera.lookSet = false;
        }
    }
}