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

            /*
            Structure.StructureData structureData = new Structure.StructureData(2, 4, 2, 2);
            Structure testStructure = new Structure(Game, buildGrid, structureData);
            town.AddStructure(testStructure);

            Structure.StructureData structureData2 = new Structure.StructureData(3, 1, 4, 2);
            Structure testStructure2 = new Structure(Game, buildGrid, structureData2);
            town.AddStructure(testStructure2);

            Structure.StructureData structureData3 = new Structure.StructureData(2, 2, 4, 3);
            Structure testStructure3 = new Structure(Game, buildGrid, structureData3);
            town.AddStructure(testStructure3);
            */
            Random rand = new Random();
            for(int i = 0; i < 5; i++)
            {
                Structure.StructureData structureData = new Structure.StructureData(rand.Next(1, 4), rand.Next(1, 4), rand.Next(0, Config.BUILD_GRID.TilesWide), rand.Next(0, Config.BUILD_GRID.TilesHigh));
                Structure testStructure = new Structure(Game, buildGrid, structureData);
                town.AddStructure(testStructure);
            }

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

            Data.Grid.Draw(_spriteBatch);

            _spriteBatch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
            Data.Town.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
