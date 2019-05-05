using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marqi.Tester
{
    public class MarqiGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private GameDisplay _display;
        private SpriteBatch _sprite;

        public MarqiGame(GameDisplay display)
        {
            _graphics = new GraphicsDeviceManager(this);
            _display = display;
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            _display.Load(GraphicsDevice);
            _graphics.PreferredBackBufferWidth = _display.Width;
            _graphics.PreferredBackBufferHeight = _display.Height;
            _graphics.ApplyChanges();
            _sprite = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Console.KeyAvailable)
            {
                this.Exit();
            }

            _display.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Gray);
            _sprite.Begin();
            _sprite.Draw(_display.Texture, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            _sprite.End();
            base.Draw(gameTime);
        }
    }
}
