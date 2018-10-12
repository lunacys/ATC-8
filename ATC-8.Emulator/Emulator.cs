using System.Collections.Generic;
using ATC8.Emulator.Screens;
using lunge.Library.GameTimers;
using lunge.Library.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ATC8.Emulator
{
    public class Emulator : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private ScreenGameComponent _screenComponent;
        private StartupScreen _startupScreen;

        public Emulator()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _startupScreen = new StartupScreen(this);
            _screenComponent = new ScreenGameComponent(this);
            _screenComponent.Register(_startupScreen);

            Components.Add(_screenComponent);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            GameTimerManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}