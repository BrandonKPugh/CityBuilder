using System;
using System.Collections.Generic;
using System.Text;
using CityBuilder.GameCode;
using Microsoft.Xna.Framework;

namespace CityBuilder
{
    public static class Config
    {
        public static int GAME_WIDTH = 1600;
        public static int GAME_HEIGHT = 800;
        public static int VIEWPORT_WIDTH = 1600;
        public static int VIEWPORT_HEIGHT = 800;

        public static string SHEET_CONFIG_FILE_NAME = "Content\\InitialSheetConfig.txt";

        public const float TICK_INTERVAL = 1000 / 60f;

        public struct GRID_INFO
        {
            private float x, y, width, height;
            private int tilesX, tilesY;

            public GRID_INFO(int tilesWide, int tilesHigh, float x_percent, float y_percent, float width_percent, float height_percent)
            {
                this.tilesX = tilesWide;
                this.tilesY = tilesHigh;
                this.x = x_percent * Config.GAME_WIDTH;
                this.y = y_percent * Config.GAME_HEIGHT;
                this.width = width_percent * Config.GAME_WIDTH;
                this.height = height_percent * Config.GAME_HEIGHT;
            }
            public GRID_INFO(int tilesWide, int tilesHigh, int x, int y, int width, int height)
            {
                this.tilesX = tilesWide;
                this.tilesY = tilesHigh;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
            public int TilesWide { get { return tilesX; } }
            public int TilesHigh { get { return tilesY; } }

            public int X { get { return (int)(this.x); } }
            public int Y { get { return (int)(this.y); } }
            public int Width { get { return (int)(this.width); } }
            public int Height { get { return (int)(this.height); } }
            public Rectangle Rect { get { return new Rectangle(X, Y, Width, Height); } }
        }

        public struct TOWN_INFO
        {

        }

        // GRID_INFO(tilesWide, tilesHigh, x, y, width, height);
        //public static GRID_INFO BUILD_GRID = new GRID_INFO(20, 20, 0.1f, .1f, 0.4f, .8f);
        public static GRID_INFO BUILD_GRID = new GRID_INFO(32, 32, 20, 20, 760, 760);

        public static Color GRID_COLOR = new Color(30, 30, 30);

        public static int MAX_RESOURCE_VALUE = 99999999;
        public static int INITIAL_RESOURCE_VALUE_WOOD = 500;
        public static int INITIAL_RESOURCE_VALUE_STONE = 200;
        public static int INITIAL_RESOURCE_VALUE_ORE = 50;
        public static int INITIAL_RESOURCE_VALUE_METAL = 20;

        public static int CAPITOL_RESOURCE_ADD_WOOD = 400;
        public static int CAPITOL_RESOURCE_ADD_STONE = 200;
        public static int CAPITOL_RESOURCE_ADD_ORE = 50;
        public static int CAPITOL_RESOURCE_ADD_METAL = 25;

        public static int WAREHOUSE_RESOURCE_ADD_WOOD = 150;
        public static int WAREHOUSE_RESOURCE_ADD_STONE = 100;
        public static int WAREHOUSE_RESOURCE_ADD_ORE = 50;
        public static int WAREHOUSE_RESOURCE_ADD_METAL = 50;

        public static string STRUCTURE_TEXTURE_CAPITOL = "capitol";
        public static string STRUCTURE_TEXTURE_FORGE = "forge";
        public static string STRUCTURE_TEXTURE_HOUSE = "house";
        public static string STRUCTURE_TEXTURE_LUMBERMILL = "lumbermill";
        public static string STRUCTURE_TEXTURE_MINE = "mine";
        public static string STRUCTURE_TEXTURE_WAREHOUSE = "warehouse";
    }
}
