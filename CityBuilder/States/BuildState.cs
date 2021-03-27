using CityBuilder.Interface;
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
        private SpriteSheet _spriteSheet;

        public BuildState(Game1 game, GameData data, ContentManager content, GraphicsDevice graphics) : base(game, data, content, graphics)
        {

        }

        public override void Initialize()
        {

            Grid buildGrid = new Grid(Config.BUILD_GRID);

            Town town = new Town();

            List<Structure.StructureData> structureDataList = new List<Structure.StructureData>();
            
            structureDataList.Add(new Structure.StructureData(3, 2, 1, 2));
            structureDataList.Add(new Structure.StructureData(1, 1, 5, 2));
            structureDataList.Add(new Structure.StructureData(1, 1, 5, 3));
            structureDataList.Add(new Structure.StructureData(1, 2, 1, 5));
            structureDataList.Add(new Structure.StructureData(1, 1, 3, 5));
            structureDataList.Add(new Structure.StructureData(1, 1, 4, 5));
            structureDataList.Add(new Structure.StructureData(1, 1, 3, 6));
            structureDataList.Add(new Structure.StructureData(1, 1, 4, 6));
            structureDataList.Add(new Structure.StructureData(2, 1, 3, 7));
            structureDataList.Add(new Structure.StructureData(3, 4, 8, 6));

            foreach (Structure.StructureData data in structureDataList)
            {
                Structure testStructure = new Structure(Game, buildGrid, data);
                town.AddStructure(testStructure);
            }

            Structure.StructureData structureData = new Structure.StructureData(2, 4, 2, 2);

            /*
            Random rand = new Random();
            for(int i = 0; i < 5; i++)
            {
                Structure.StructureData structureData = new Structure.StructureData(rand.Next(1, 4), rand.Next(1, 4), rand.Next(0, Config.BUILD_GRID.TilesWide), rand.Next(0, Config.BUILD_GRID.TilesHigh));
                Structure testStructure = new Structure(Game, buildGrid, structureData);
                town.AddStructure(testStructure);
            }
            */

            /*
            Texture2D buttonTexture = Content.Load<Texture2D>("Button");
            SpriteFont buttonFont = Content.Load<SpriteFont>("DebugFont");
            TestButton1 = new Button(buttonTexture, buttonFont);
            TestButton1.Position = new Vector2(820, 590);
            TestButton1.Size = new Vector2(760, 190);
            TestButton1.Click += TestButton1_Click;
            TestButton1.Text = "Test Text Here";
            TestButton1.PenColour = Color.Black;
            */

            this.Data = new GameData();
            Data.Initialize(town, buildGrid);
        }

        private void TestButton1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            SpriteMapper mapper = new SpriteMapper();
            _spriteSheet = mapper.ReadFile(Config.SHEET_CONFIG_FILE_NAME, Content);

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            

            Data.Town.LoadContent(_spriteSheet);

            Data.Grid.LoadContent(_spriteSheet.GetSprite("tile"));
        }

        public override void Update(GameTime gameTime)
        {
            //TestButton1.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            Data.Grid.Draw(SpriteBatch);

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            Data.Town.Draw(SpriteBatch);
            //TestButton1.Draw(gameTime, SpriteBatch);
            SpriteBatch.End();
        }
    }
}
