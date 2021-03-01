using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CityBuilder
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        State currentState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Config.VIEWPORT_WIDTH;
            _graphics.PreferredBackBufferHeight = Config.VIEWPORT_HEIGHT;
            _graphics.ApplyChanges();

            GameData data = new GameData();
            currentState = new BuildState(this, data, Content, GraphicsDevice);

            currentState.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            currentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
