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
        private BuildStateUI _ui;

        public BuildState(Game1 game, GameData data, ContentManager content, GraphicsDevice graphics) : base(game, data, content, graphics)
        {

        }

        public override void Initialize()
        {
            _ui = InitializeUI();
            this.Data = InitializeGameData();
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
            _ui.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOliveGreen);

            Data.Grid.Draw(SpriteBatch);

            SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            Data.Town.Draw(SpriteBatch);
            _ui.Draw(gameTime, SpriteBatch);

            SpriteBatch.End();
        }

        private BuildStateUI InitializeUI()
        {
            SpriteFont buttonFont = Content.Load<SpriteFont>("DebugFont");
            BuildStateUI _toReturn = new BuildStateUI();


            #region Resource Labels
            TextBox woodLabel = new TextBox(buttonFont);
            TextBox woodCount = new TextBox(buttonFont);
            woodLabel.TextBoxInfo = ControlConstants.RESOURCE_LABEL_WOOD;
            woodCount.TextBoxInfo = ControlConstants.RESOURCE_COUNTER_WOOD;
            woodLabel.TextAlignment = TextBox.TextAlign.Left;
            woodCount.TextAlignment = TextBox.TextAlign.Right;
            _toReturn.Add(woodLabel);
            _toReturn.Register(woodCount, GameCode.Resource.ResourceType.Wood);

            TextBox stoneLabel = new TextBox(buttonFont);
            TextBox stoneCount = new TextBox(buttonFont);
            stoneLabel.TextBoxInfo = ControlConstants.RESOURCE_LABEL_STONE;
            stoneCount.TextBoxInfo = ControlConstants.RESOURCE_COUNTER_STONE;
            stoneLabel.TextAlignment = TextBox.TextAlign.Left;
            stoneCount.TextAlignment = TextBox.TextAlign.Right;
            _toReturn.Add(stoneLabel);
            _toReturn.Register(stoneCount, GameCode.Resource.ResourceType.Stone);

            TextBox oreLabel = new TextBox(buttonFont);
            TextBox oreCount = new TextBox(buttonFont);
            oreLabel.TextBoxInfo = ControlConstants.RESOURCE_LABEL_ORE;
            oreCount.TextBoxInfo = ControlConstants.RESOURCE_COUNTER_ORE;
            oreLabel.TextAlignment = TextBox.TextAlign.Left;
            oreCount.TextAlignment = TextBox.TextAlign.Right;
            _toReturn.Add(oreLabel);
            _toReturn.Register(oreCount, GameCode.Resource.ResourceType.Ore);

            TextBox metalLabel = new TextBox(buttonFont);
            TextBox metalCount = new TextBox(buttonFont);
            metalLabel.TextBoxInfo = ControlConstants.RESOURCE_LABEL_METAL;
            metalCount.TextBoxInfo = ControlConstants.RESOURCE_COUNTER_METAL;
            metalLabel.TextAlignment = TextBox.TextAlign.Left;
            metalCount.TextAlignment = TextBox.TextAlign.Right;
            _toReturn.Add(metalLabel);
            _toReturn.Register(metalCount, GameCode.Resource.ResourceType.Metal);
            #endregion

            #region ScrollBox

            UIBox scrollBox = new UIBox(Content, ControlConstants.BUILD_SCROLLBOX);
            _toReturn.Add(scrollBox);
            Button scrollBoxUpArrow = new Button(Content, ControlConstants.BUILD_SCROLLBOX_UP);
            _toReturn.Add(scrollBoxUpArrow);
            Button scrollBoxDownArrow = new Button(Content, ControlConstants.BUILD_SCROLLBOX_DOWN);
            _toReturn.Add(scrollBoxDownArrow);
            UIBox scrollBoxSlider = new UIBox(Content, ControlConstants.BUILD_SCROLLBOX_SLIDER);
            _toReturn.Add(scrollBoxSlider);

            #endregion

            return _toReturn;
        }

        private GameData InitializeGameData()
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

            GameData toReturn = new GameData();
            Data.Initialize(town, buildGrid);
            return Data;
        }
    }
}
