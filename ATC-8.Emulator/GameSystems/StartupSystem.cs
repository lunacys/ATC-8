using System;
using System.IO;
using System.Linq;
using lunge.Library.GameSystems;
using lunge.Library.GameTimers;
using lunge.Library.Input;
using lunge.Library.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.ViewportAdapters;

namespace ATC8.Emulator.GameSystems
{
    public class StartupSystem : DrawableGameSystem
    {
        private string _inputText = "";
        private string _outputText = "";
        private SpriteFont _terminalFont;

        private int _cursorPosition = 0;
        private int _outputLines = 0;

        private InputHandler _input;
        private InputListenerComponent _inputListener;

        private bool _isInsert = false;

        private float _outputY = 0;
        private readonly float _maxOutputY = 21;

        private ViewportAdapter _viewport;

        public StartupSystem(Game game, Screen hostScreen) 
            : base(game, hostScreen)
        {
            var kbListener = new KeyboardListener(new KeyboardListenerSettings
            {
                InitialDelayMilliseconds = 500,
                RepeatDelayMilliseconds = 50,
                RepeatPress = true
            });
            _inputListener = new InputListenerComponent(GameRoot, kbListener);
            _input = new InputHandler(GameRoot);
            GameRoot.Components.Add(_inputListener);
            GameRoot.Components.Add(_input);
            kbListener.KeyPressed += (sender, args) =>
            {
                if (args.Key == Keys.Left)
                    _cursorPosition--;
                if (args.Key == Keys.Right)
                    _cursorPosition++;
                if (args.Key == Keys.Insert)
                    _isInsert = !_isInsert;

                if (args.Key == Keys.Delete && _inputText.Length > 0 && _cursorPosition < _inputText.Length)
                {
                    _inputText = _inputText.Remove(_cursorPosition, 1);
                }

                _cursorPosition = MathHelper.Clamp(_cursorPosition, 0, _inputText.Length);
            };
            kbListener.KeyTyped += (sender, args) =>
            {
                if (args.Character != null && args.Key != Keys.Back && args.Key != Keys.Tab)
                {
                    if (_isInsert && _cursorPosition < _inputText.Length)
                        _inputText = _inputText.Remove(_cursorPosition, 1);

                    if (_inputText.Length < 33)
                    {
                        _inputText = _inputText.Insert(_cursorPosition, args.Character.ToString());
                        _cursorPosition++;
                    }
                }

                if (args.Key == Keys.Back && _inputText.Length > 0)
                {
                    _inputText = _inputText.Remove(_inputText.Length - 1, 1);
                    _cursorPosition--;
                }

                if (args.Key == Keys.Enter)
                {
                    ProcessInputString();
                    _cursorPosition = 0;
                    _inputText = "";
                }
            };

            _viewport = new BoxingViewportAdapter(GameRoot.Window, GameRoot.GraphicsDevice, 256, 256);
        }

        private void ProcessInputString()
        {
            _inputText = _inputText.TrimStart('\r', '\n', '\n').TrimEnd('\r', '\n', ' ');

            string result = "";

            if (_inputText.ToLower() == "help")
                result += $" > {_inputText}: THIS\nis\nyOuR\nHeLp!1!";
            else if (_inputText.ToLower() == "hello")
                result += $" > {_inputText}: HIIIIIIIII!!!";
            else
                result += $" > {_inputText}: Unknown command";

            result += '\n';
            _outputText += result;

            var lines = result.Count(s => s == '\n');

            _outputLines += lines;

            if (_outputLines > _maxOutputY)
                _outputY -= lines;
        }

        public override void LoadContent()
        {
            _terminalFont = GameRoot.Content.Load<SpriteFont>(Path.Combine("Fonts", "Terminal"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, transformMatrix: _viewport.GetScaleMatrix());

            SpriteBatch.DrawString(_terminalFont, _outputText, new Vector2(4, _outputY * 11 + 4), Color.White);

            SpriteBatch.FillRectangle(0, 256 - 16, 256, 16, Color.Black);
            SpriteBatch.DrawRectangle(0, 256 - 16, 256, 16, Color.White);
            
            SpriteBatch.DrawString(_terminalFont, "root:~$ " + _inputText.Insert(_cursorPosition, _isInsert ? "_" : "|"), new Vector2(4, 256-14), Color.White);

            SpriteBatch.DrawRectangle(0, 0, 256, 256, Color.White);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}