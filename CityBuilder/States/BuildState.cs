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

            Grid buildGrid = new Grid(Config.BUILD_GRID);

            Town town = new Town();

            Rectangle rect = buildGrid.TileToPixelRect(2, 4);
            RectangleBody testCollision = new RectangleBody(rect);
            //RectangleBody testCollision = new RectangleBody(new Vector2(2, 2), new Vector2(20, 20));
            Structure.StructureData structureData = new Structure.StructureData(1, 3);
            Structure testStructure = new Structure(Game, testCollision, structureData);
            town.AddStructure(testStructure);

            this.Data = new GameData();
            Data.Initialize(town, buildGrid);
        }

        public override void LoadContent()
        {
            SpriteMapper mapper = new SpriteMapper();
            _spriteSheet = mapper.ReadFile(Config.SHEET_CONFIG_FILE_NAME, Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);



            Data.Town.LoadContent(_spriteSheet);

            Data.Grid.LoadContent(_spriteSheet.GetSprite("tile"));
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            //Data.Grid.Draw(_spriteBatch);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            Data.Town.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
