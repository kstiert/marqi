using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Marqi.Tester
{
    public class MarqiGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly GameDisplay _display;
        private SpriteBatch _sprite;

        public MarqiGame(GameDisplay display)
        {
            _graphics = new GraphicsDeviceManager(this);
            _display = display;
            IsMouseVisible = true;
            IsFixedTimeStep = false;
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

            // _display.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30,30,30));
            _sprite.Begin();
            _sprite.Draw(_display.Texture, Vector2.Zero, Microsoft.Xna.Framework.Color.White);
            _sprite.End();
            base.Draw(gameTime);
        }
    }
}
