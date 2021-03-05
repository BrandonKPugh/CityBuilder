using System;
using System.Collections.Generic;
using System.Text;
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
                this.x = x_percent;
                this.y = y_percent;
                this.width = width_percent;
                this.height = height_percent;
            }
            public int TilesWide { get { return tilesX; } }
            public int TilesHigh { get { return tilesY; } }

            public int X { get { return (int)(this.x * Config.GAME_WIDTH); } }
            public int Y { get { return (int)(this.y * Config.GAME_HEIGHT); } }
            public int Width { get { return (int)(this.width * Config.GAME_WIDTH); } }
            public int Height { get { return (int)(this.height * Config.GAME_HEIGHT); } }
            public Rectangle Rect { get { return new Rectangle(X, Y, Width, Height); } }
        }

        // GRID_INFO(tilesWide, tilesHigh, x, y, width, height);
        public static GRID_INFO BUILD_GRID = new GRID_INFO(10, 10, 0.05625f, .1f, .45f, .8f);

        public static Color GRID_COLOR = new Color(30, 30, 30);
    }
}
