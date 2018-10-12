using System;
using System.IO;
using ATC8.Emulator.GameSystems;
using lunge.Library.GameSystems;
using lunge.Library.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ATC8.Emulator.Screens
{
    public class StartupScreen : Screen
    {
        private Texture2D _testTexture;

        public StartupScreen(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            Console.WriteLine("Initializing StartupScreen");
            GameSystemComponent.Register(new StartupSystem(GameRoot));

            base.Initialize();
        }

        public override void LoadContent()
        {
            _testTexture = GameRoot.Content.Load<Texture2D>(Path.Combine("Images", "Test"));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(_testTexture, new Vector2(64, 64), null, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}