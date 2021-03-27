using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public abstract class State
    {
        protected Game1 Game;
        protected GameData Data;
        protected ContentManager Content;
        protected GraphicsDevice GraphicsDevice;
        protected SpriteBatch SpriteBatch;
        public State(Game1 game, GameData data, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.Game = game;
            this.Data = data;
            this.Content = content;
            this.GraphicsDevice = graphicsDevice;
        }

        public abstract void Initialize();
        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
