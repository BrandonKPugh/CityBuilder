using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    class BuildState : State
    {
        private SpriteBatch _spriteBatch;
        private SpriteSheet _spriteSheet;


        public BuildState(Game1 game, GameData data, ContentManager content, GraphicsDevice graphics) : base(game, data, content, graphics)
        {

        }

        public override void Initialize()
        {
            SpriteMapper mapper = new SpriteMapper();
            _spriteSheet = mapper.ReadFile(Config.SHEET_CONFIG_FILE_NAME, Content);

            Grid buildGrid = new Grid(Config.BUILD_GRID);

            this.Data = new GameData();
            Data.Initialize(new Town(), buildGrid);
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteSheet.LoadContent(Content);

            Data.Grid.LoadContent(_spriteSheet.GetSprite("tile"));
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            Data.Grid.Draw(_spriteBatch);

            /*
            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            _spriteBatch.End();
            */
        }
    }
}
