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
        public StartupScreen(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            Console.WriteLine("Initializing StartupScreen");
            GameSystemComponent.Register(new StartupSystem(GameRoot, this));

            base.Initialize();
        }

        public override void LoadContent()
        {

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}