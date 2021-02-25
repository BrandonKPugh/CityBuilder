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
        private SpriteBatch _spriteBatch;
        private SpriteSheet _spriteSheet;

        
        Object testObj;
        Grid testGrid;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SpriteMapper mapper = new SpriteMapper();
            _spriteSheet = mapper.ReadFile(Config.SHEET_CONFIG_FILE_NAME, Content);

            _graphics.PreferredBackBufferWidth = Config.VIEWPORT_WIDTH;
            _graphics.PreferredBackBufferHeight = Config.VIEWPORT_HEIGHT;
            _graphics.ApplyChanges();

            RectangleBody collision = new RectangleBody(new Vector2(100, 100), new Vector2(200, 200));
            testObj = new Object(this, collision);

            //Config.GRID_INFO gridInfo = new Config.GRID_INFO(50, 25, 0f, 0f, 1.0f, 1.0f);
            //testGrid = new Grid(gridInfo);

            testGrid = new Grid(Config.BUILD_GRID);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteSheet.LoadContent(Content);

            testObj.LoadContent(_spriteSheet.GetSprite("structure-1x1"));

            testGrid.LoadContent(_spriteSheet.GetSprite("tile"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            testObj.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            testGrid.Draw(_spriteBatch);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);

            testObj.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
