﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Interface
{
    public static class ControlConstants
    {
        #region STRUCTS
        
        public struct BUTTON_INFO
        {
            // Button info is created with Text, and four floats (representing percentages of the screen's width/height. 0.1f = 10% from the edge and 0.5f = the center of the screen)
            // BUTTON_INFO(Text, X, Y, Width, Height)
            private int x, y, width, height;
            private string text;
            private string texture_name;
            private string font_name;
            public BUTTON_INFO(string textureName, string fontName, string text, float x_percent, float y_percent, float width_percent, float height_percent)
            {
                this.texture_name = textureName;
                this.font_name = fontName;
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
                this.text = text;
            }
            public BUTTON_INFO(string textureName, string fontName, string text, int x, int y, int width, int height)
            {
                this.texture_name = textureName;
                this.font_name = fontName;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.text = text;
            }
            public int X { get { return this.x; }}
            public int Y { get { return this.y; }}
            public int Width { get { return this.width; }}
            public int Height{ get { return this.height; }}
            public string Text { get { return text; }}
            public string Texture_Name { get { return texture_name; } }
            public string Font_Name { get { return font_name; } }
        }

        public struct TEXTBOX_INFO
        {
            private int x, y, width, height;
            private string text;
            private Color color;
            public TEXTBOX_INFO(string text, Color color, float x_percent, float y_percent, float width_percent, float height_percent)
            {
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
                this.text = text;
                this.color = color;
            }
            public TEXTBOX_INFO(string text, Color color, int x, int y, int width, int height)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.text = text;
                this.color = color;
            }
            public int X { get { return this.x; } }
            public int Y { get { return this.y; } }
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public string Text { get { return text; } }
            public Color Color { get { return color; } }
        }

        public struct UIBOX_INFO
        {
            private int x, y, width, height;
            private Color color;
            private string texture_name;
            public UIBOX_INFO(string texture_name, float x_percent, float y_percent, float width_percent, float height_percent, Color color, int alpha = 255)
            {
                this.texture_name = texture_name;
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
                if (alpha < 255)
                {
                    Vector4 v = color.ToVector4();
                    v.W = alpha;
                    color = new Color(v);
                }
                this.color = color;
            }
            public UIBOX_INFO(string texture_name, int x, int y, int width, int height, Color color, int alpha = 255)
            {
                this.texture_name = texture_name;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                if (alpha < 255)
                {
                    Vector4 v = color.ToVector4();
                    v.W = alpha;
                    color = new Color(v);
                }
                this.color = color;
            }

            public UIBOX_INFO(string texture_name, Color color, int alpha = 255)
            {
                this.texture_name = texture_name;
                this.x = 0;
                this.y = 0;
                this.width = 0;
                this.height = 0;
                if (alpha < 255)
                {
                    Vector4 v = color.ToVector4();
                    v.W = alpha;
                    color = new Color(v);
                }
                this.color = color;
            }
            public int X { get { return this.x; } }
            public int Y { get { return this.y; } }
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public Color Color { get { return color; } }
            public string Texture_Name { get { return texture_name; } }
        }

        public struct BORDERBOX_INFO
        {
            private int x, y, width, height;
            private Color color;
            private int weight, padding;
            public BORDERBOX_INFO(int penWeight, Color color, float x_percent, float y_percent, float width_percent, float height_percent, int padding)
            {
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
                this.weight = penWeight;
                this.color = color;
                this.padding = padding;
            }
            public BORDERBOX_INFO(int penWeight, Color color, int x, int y, int width, int height, int padding)
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.weight = penWeight;
                this.color = color;
                this.padding = padding;
            }

            public BORDERBOX_INFO(int penWeight, Color color, int padding)
            {
                this.x = 0;
                this.y = 0;
                this.width = 0;
                this.height = 0;
                this.weight = penWeight;
                this.color = color;
                this.padding = padding;
            }
            public int X { get { return this.x; } }
            public int Y { get { return this.y; } }
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public int Weight { get { return weight; } }
            public Color Color { get { return color; } }
            public int Padding { get { return padding; } }
        }

        public struct PROGRESSBAR_INFO
        {
            private int x, y, width, height;
            private string text;
            private Color backColor;
            private Color frontColor;
            private int penWeight;
            public PROGRESSBAR_INFO(float x_percent, float y_percent, float width_percent, float height_percent, Color backColor, Color frontColor, int penWeight, string text = "")
            {
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
                this.text = text;
                this.backColor = backColor;
                this.frontColor = frontColor;
                this.penWeight = penWeight;
            }
            public PROGRESSBAR_INFO(int x, int y, int width, int height, Color backColor, Color frontColor, int penWeight, string text = "")
            {
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.text = text;
                this.backColor = backColor;
                this.frontColor = frontColor;
                this.penWeight = penWeight;
            }
            public int X { get { return this.x; } }
            public int Y { get { return this.y; } }
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public string Text { get { return text; } }
            public Color FrontColor { get { return frontColor; } }
            public Color BackColor { get { return backColor; } }
            public Rectangle Location { get { return new Rectangle(X, Y, Width, Height); } }
            public int PenWeight { get { return penWeight; } }
        }

        #endregion

        #region GENERAL

        public const string BUTTON_TEXTURE = "Button";
        public const string BUTTON_FONT = "DebugFont";
        public static Color BUTTON_PENCOLOR = Color.Black;
        public static Color BUTTON_BACKCOLOR = Color.White;
        public static Color PROGRESSBUTTON_BACKCOLOR = Color.DarkGray;
        public static Color PROGRESSBUTTON_FRONTCOLOR = Color.White;
        public static Color PROGRESSBUTTON_SELECTEDBACKCOLOR = Color.SlateGray;
        public static Color PROGRESSBUTTON_SELECTEDFRONTCOLOR = Color.LightBlue;
        public static Color BUTTON_HOVERING = Color.Gray;
        public const float BUTTON_PADDING_RATIO = 0.90f;
        public static Color BUTTON_SELECTED = Color.SkyBlue;
        public static Color BAR_PENCOLOR = Color.Black;
        public static Color TEXTBOX_TEXTCOLOR = Color.Black;
        
        public static BORDERBOX_INFO TOOLTIP_BOX_SIZE = new BORDERBOX_INFO(1000, Color.White, 0f, 0f, 0.075f, 0.05f, 0);
        public static TEXTBOX_INFO TOOLTIP_TEXT = new TEXTBOX_INFO("", Color.Black, 0f, 0f, 0.075f, 0.05f);

        #endregion

        #region MAINMENU
        /*
        public static TEXTBOX_INFO MAINMENU_TITLE = new TEXTBOX_INFO("Slower Than Sound", Color.LightSkyBlue, 0.3f, 0.05f, 0.4f, 0.2f);
        public static BUTTON_INFO MAINMENU_NEWGAME = new BUTTON_INFO("New Game", 0.35f, 0.3f, 0.3f, 0.15f);
        public static BUTTON_INFO MAINMENU_TUTORIAL = new BUTTON_INFO("Tutorial", 0.35f, 0.5f, 0.3f, 0.15f);
        public static BUTTON_INFO MAINMENU_QUITGAME = new BUTTON_INFO("Quit Game", 0.35f, 0.7f, 0.3f, 0.15f);
        //*/
        #endregion

        #region PAUSEMENU
        /*
        public static BUTTON_INFO PAUSEMENU_RESUMEGAME = new BUTTON_INFO("Resume Game", 0.35f, 0.3f, 0.3f, 0.15f);
        public static BUTTON_INFO PAUSEMENU_QUITGAME = new BUTTON_INFO("Exit to Menu", 0.35f, 0.5f, 0.3f, 0.15f);
        //*/
        #endregion

        #region COMBATMODE
        /*
        public static Color COMBATMODE_ROOMHEALTHCOLOR = new Color(255, 0, 0);
        public const int COMBATMODE_ROOMHEALTHALPHA = 50;
        public const int COMBATMODE_ROOMFLASHINGFRAMES = 30;
        public static BORDERBOX_INFO COMBATMODE_GRIDBOX = new BORDERBOX_INFO(3, Color.Black, 5);

        public static TEXTBOX_INFO COMBATMODE_TITLE = new TEXTBOX_INFO("Combat Mode", Color.LightSkyBlue, 0.625f, 0.05f, 0.25f, 0.05f);
        public static TEXTBOX_INFO PRIMARY_TEXTBOX = new TEXTBOX_INFO("Metal:", Color.Black, 0.3f, 0.025f, 0.075f, 0.05f);
        public static TEXTBOX_INFO PRIMARY_TEXTBOX_VALUE = new TEXTBOX_INFO("n/a", Color.Black, 0.375f, 0.025f, 0.075f, 0.05f);
        public static PROGRESSBAR_INFO PLAYER_POWERBAR= new PROGRESSBAR_INFO(0.3f, 0.025f, 0.2f, 0.05f, Color.DarkGray, Color.White, 3, "Power: 0 / 0");

        public static PROGRESSBAR_INFO PLAYER_HEALTHBAR = new PROGRESSBAR_INFO(0.05f, 0.025f, 0.2f, 0.05f, Color.DarkGray, Color.White, 3, "Ship Health");

        private const float COMBATMODE_CANVAS_WIDTH = .38125f;
        private const float COMBATMODE_CANVAS_HEIGHT = 0.675f;
        private const float COMBATMODE_CANVAS_X = 0.5625f;
        private const float COMBATMODE_CANVAS_Y = 0.125f;
        private const float COMBATMODE_SHIP_RATIO = COMBATMODE_CANVAS_WIDTH * 14f / 64f;
        private const float COMBATMODE_COMPONENT_RATIO = COMBATMODE_CANVAS_WIDTH * 27f / 64f;
        private const float COMBATMODE_RESEARCH_RATIO = COMBATMODE_CANVAS_WIDTH * 22f / 64f;
        private const float COMBATMODE_GAP_RATIO = (COMBATMODE_CANVAS_WIDTH - (COMBATMODE_SHIP_RATIO + COMBATMODE_COMPONENT_RATIO + COMBATMODE_RESEARCH_RATIO)) / 2;

        public static BORDERBOX_INFO COMBATMODE_CANVAS = new BORDERBOX_INFO(3, Color.Black, COMBATMODE_CANVAS_X, COMBATMODE_CANVAS_Y, COMBATMODE_CANVAS_WIDTH, COMBATMODE_CANVAS_HEIGHT, 5);

        private const float COMBATMODE_SPACINGX = 1 / 64f;
        private const float COMBATMODE_SPACINGY = 1 / 64f;
        private const int COMBATMODE_BUTTON_COUNT = 6;
        private const float COMBATMODE_BUTTON_X = COMBATMODE_CANVAS_WIDTH - COMBATMODE_SPACINGX * 2;
        private const float COMBATMODE_BUTTON_Y = (COMBATMODE_CANVAS_HEIGHT - (COMBATMODE_SPACINGY * (COMBATMODE_BUTTON_COUNT + 1))) / COMBATMODE_BUTTON_COUNT;
        private static int COMBATMODE_buttonCounter = 0;

        public static BUTTON_INFO COMBATMODE_TARGETENEMYSHIP = new BUTTON_INFO("Enemy Hull", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * COMBATMODE_buttonCounter, COMBATMODE_BUTTON_X, COMBATMODE_BUTTON_Y);
        public static BUTTON_INFO COMBATMODE_TARGETSTORAGES = new BUTTON_INFO("Material Storage", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * ++COMBATMODE_buttonCounter, COMBATMODE_BUTTON_X, COMBATMODE_BUTTON_Y);
        public static BUTTON_INFO COMBATMODE_TARGETWEAPONS = new BUTTON_INFO("Weapon Systems", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * ++COMBATMODE_buttonCounter, COMBATMODE_BUTTON_X, COMBATMODE_BUTTON_Y);
        public static BUTTON_INFO COMBATMODE_TARGETPOWERGEN = new BUTTON_INFO("Power Generation", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * ++COMBATMODE_buttonCounter, COMBATMODE_BUTTON_X, COMBATMODE_BUTTON_Y);
        public static BUTTON_INFO COMBATMODE_TARGETPOWERSTORAGE = new BUTTON_INFO("Power Storage", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * ++COMBATMODE_buttonCounter, COMBATMODE_BUTTON_X, COMBATMODE_BUTTON_Y);
        
        public static BUTTON_INFO COMBATMODE_FIREWEAPON = new BUTTON_INFO("Fire Weapon", COMBATMODE_CANVAS_X + COMBATMODE_SPACINGX, COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * ++COMBATMODE_buttonCounter, (COMBATMODE_BUTTON_X / 2f) - (COMBATMODE_SPACINGX / 2), COMBATMODE_BUTTON_Y);
        public static BUTTON_INFO COMBATMODE_REPAIRROOM = new BUTTON_INFO("Repair Room", COMBATMODE_CANVAS_X + (COMBATMODE_CANVAS_WIDTH / 2) + (COMBATMODE_SPACINGX / 2), COMBATMODE_CANVAS_Y + COMBATMODE_SPACINGY + (COMBATMODE_SPACINGY + COMBATMODE_BUTTON_Y) * COMBATMODE_buttonCounter, (COMBATMODE_BUTTON_X / 2f) - (COMBATMODE_SPACINGX / 2), COMBATMODE_BUTTON_Y);


        public static BUTTON_INFO COMBATMODE_BUILDMODE = new BUTTON_INFO("Flee Battle", 0.625f, 0.825f, 0.25f, 0.125f);
        //*/
        #endregion

        #region BUILDMODE

        public static TEXTBOX_INFO RESOURCE_COUNTER_WOOD = new TEXTBOX_INFO(Config.INITIAL_RESOURCE_VALUE_WOOD.ToString(), TEXTBOX_TEXTCOLOR, 820, 20, 150, 40);
        public static TEXTBOX_INFO RESOURCE_LABEL_WOOD = new TEXTBOX_INFO("WOOD", TEXTBOX_TEXTCOLOR, 985, 20, 200, 40);
        public static TEXTBOX_INFO RESOURCE_COUNTER_STONE = new TEXTBOX_INFO(Config.INITIAL_RESOURCE_VALUE_STONE.ToString(), TEXTBOX_TEXTCOLOR, 820, 80, 150, 40);
        public static TEXTBOX_INFO RESOURCE_LABEL_STONE = new TEXTBOX_INFO("STONE", TEXTBOX_TEXTCOLOR, 985, 80, 200, 40);
        public static TEXTBOX_INFO RESOURCE_COUNTER_ORE = new TEXTBOX_INFO(Config.INITIAL_RESOURCE_VALUE_ORE.ToString(), TEXTBOX_TEXTCOLOR, 820, 140, 150, 40);
        public static TEXTBOX_INFO RESOURCE_LABEL_ORE = new TEXTBOX_INFO("ORE", TEXTBOX_TEXTCOLOR, 985, 140, 200, 40);
        public static TEXTBOX_INFO RESOURCE_COUNTER_METAL = new TEXTBOX_INFO(Config.INITIAL_RESOURCE_VALUE_METAL.ToString(), TEXTBOX_TEXTCOLOR, 820, 200, 150, 40);
        public static TEXTBOX_INFO RESOURCE_LABEL_METAL = new TEXTBOX_INFO("METAL", TEXTBOX_TEXTCOLOR, 985, 200, 200, 40);

        public static BUTTON_INFO BUILD_SCROLLBOX_UP = new BUTTON_INFO("ButtonUp48x48", null, null, 1152, 400, 48, 48);
        public static BUTTON_INFO BUILD_SCROLLBOX_DOWN = new BUTTON_INFO("ButtonDown48x48", null, null, 1152, 728, 48, 48);
        public static UIBOX_INFO BUILD_SCROLLBOX_SLIDER = new UIBOX_INFO("Slider48x24", 1152, 450, 48, 24, Color.White);
        public static UIBOX_INFO BUILD_SCROLLBOX = new UIBOX_INFO("ScrollBox384x384", 820, 396, 384, 384, Color.White);

        #endregion
        #region COMPONENTBUILD
        /*
        public static BUTTON_INFO BUILDMODE_COMPONENTBUILD = new BUTTON_INFO("Components", BUILDMODE_CANVAS_X + BUILDMODE_SHIP_RATIO + BUILDMODE_GAP_RATIO, 0.125f, BUILDMODE_COMPONENT_RATIO, .06875f);

        private const float _SPACINGX = 1 / 64f;
        private const float _SPACINGY = 1 / 64f;
        private const int _BUTTON_COUNT = 7;
        private const float BUTTON_X = BUILDMODE_CANVAS_WIDTH - _SPACINGX * 2;
        private const float BUTTON_Y = (BUILDMODE_CANVAS_HEIGHT - (_SPACINGY * (_BUTTON_COUNT + 1))) / _BUTTON_COUNT;
        private static int _buttonCounter = 0;
        public static BUTTON_INFO PLACE_COMPONENT_WEAPON = new BUTTON_INFO("Place Weapon", BUILDMODE_CANVAS_X + _SPACINGX, BUILDMODE_CANVAS_Y + _SPACINGY, BUTTON_X, BUTTON_Y);
        public static BUTTON_INFO PLACE_COMPONENT_STORAGE = new BUTTON_INFO("Place Storage", BUILDMODE_CANVAS_X + _SPACINGX, BUILDMODE_CANVAS_Y + _SPACINGY + (_SPACINGY + BUTTON_Y) * ++_buttonCounter, BUTTON_X, BUTTON_Y);
        public static BUTTON_INFO PLACE_COMPONENT_GENERATOR = new BUTTON_INFO("Place Generator", BUILDMODE_CANVAS_X + _SPACINGX, BUILDMODE_CANVAS_Y + _SPACINGY + (_SPACINGY + BUTTON_Y) * ++_buttonCounter, BUTTON_X, BUTTON_Y);
        public static BUTTON_INFO PLACE_COMPONENT_BATTERY = new BUTTON_INFO("Place Battery", BUILDMODE_CANVAS_X + _SPACINGX, BUILDMODE_CANVAS_Y + _SPACINGY + (_SPACINGY + BUTTON_Y) * ++_buttonCounter, BUTTON_X, BUTTON_Y);


        public static BUTTON_INFO DELETE_COMPONENT = new BUTTON_INFO("Delete Components", BUILDMODE_CANVAS_X + _SPACINGX, BUILDMODE_CANVAS_Y + _SPACINGY + (_SPACINGY + BUTTON_Y) * ++_buttonCounter, BUTTON_X, BUTTON_Y);
        //*/
        #endregion
        #region ROOM
        /*
        public static BUTTON_INFO BUILDMODE_SHIP = new BUTTON_INFO("Ship", BUILDMODE_CANVAS_X + BUILDMODE_SHIP_RATIO + BUILDMODE_COMPONENT_RATIO + BUILDMODE_GAP_RATIO * 2, 0.125f, BUILDMODE_RESEARCH_RATIO, .06875f);

        private const float BUILDMODEROOMS_SPACINGX = 1 / 64f;
        private const float BUILDMODEROOMS_SPACINGY = 1 / 64f;
        private const int BUILDMODEROOMS_BUTTON_COUNT = 4;
        private const float BUILDMODEROOMSBUTTON_X = BUILDMODE_CANVAS_WIDTH - BUILDMODEROOMS_SPACINGX * 2;
        private const float BUILDMODEROOMSBUTTON_Y = (BUILDMODE_CANVAS_HEIGHT - (BUILDMODEROOMS_SPACINGY * (BUILDMODEROOMS_BUTTON_COUNT + 1))) / BUILDMODEROOMS_BUTTON_COUNT;
        private static int BUILDMODEROOMS_buttonCounter = 0;

        public static BUTTON_INFO CREATE_ROOM = new BUTTON_INFO("Create Room", BUILDMODE_CANVAS_X + BUILDMODEROOMS_SPACINGX, BUILDMODE_CANVAS_Y + BUILDMODEROOMS_SPACINGY + (BUILDMODEROOMS_SPACINGY + BUILDMODEROOMSBUTTON_Y) * ++BUILDMODEROOMS_buttonCounter, BUILDMODEROOMSBUTTON_X, BUILDMODEROOMSBUTTON_Y);
        public static BUTTON_INFO DELETE_ROOM = new BUTTON_INFO("Delete Room", BUILDMODE_CANVAS_X + BUILDMODEROOMS_SPACINGX, BUILDMODE_CANVAS_Y + BUILDMODEROOMS_SPACINGY + (BUILDMODEROOMS_SPACINGY + BUILDMODEROOMSBUTTON_Y) * ++BUILDMODEROOMS_buttonCounter, BUILDMODEROOMSBUTTON_X, BUILDMODEROOMSBUTTON_Y);

        public static TEXTBOX_INFO ROOM_INFO_TEXTBOX = new TEXTBOX_INFO("Select a room", Color.Black, BUILDMODE_CANVAS_X, BUILDMODE_CANVAS_Y, BUILDMODE_CANVAS_WIDTH, BUILDMODE_CANVAS_HEIGHT);
        public static UIBOX_INFO ROOM_INFO_BOX = new UIBOX_INFO();
        public const int ROOM_INFO_BOX_ALPHA = 75;
        #endregion


        public static TEXTBOX_INFO BUILDMODE_TITLE = new TEXTBOX_INFO("Build Mode", Color.LightSkyBlue, 0.625f, 0.05f, 0.25f, 0.05f);
        public static BORDERBOX_INFO BUILDMODE_TITLEBOX = new BORDERBOX_INFO(3, Color.Black, 5);

        // BORDERBOX_INFO(penWeight, color, padding)
        // Position/size is later set using BorderBox.SetPosition() so that it is aligned with the grid.
        public static BORDERBOX_INFO BUILDMODE_GRIDBOX = new BORDERBOX_INFO(3, Color.Black, 5);

        //*/

        #endregion

        #region REVIEW
        /*
        private const int _STATSCOUNT = 2;
        private const float _STATSX = 0.3f;
        private const float _STATSY = 0.2f;
        private const float _STATSW = 0.4f;
        private const float _STATSH = 0.5f;
        private const float _STATS_RATIO = 0.75f;

        private static float _STATS_PERH = _STATSH / _STATSCOUNT;
        private static int _STATS_COUNTER = 0;

        public static TEXTBOX_INFO REVIEW_METAL_GATHERED_TEXT = new TEXTBOX_INFO("Metal Collected: ", Color.White, _STATSX, _STATSY + (_STATS_PERH * _STATS_COUNTER), _STATSW * _STATS_RATIO, _STATS_PERH);
        public static TEXTBOX_INFO REVIEW_METAL_GATHERED_VALUE = new TEXTBOX_INFO("0", Color.White, _STATSX + (_STATSW * _STATS_RATIO), _STATSY + (_STATS_PERH * _STATS_COUNTER++), _STATSW * (1f -_STATS_RATIO), _STATS_PERH);


        public static BUTTON_INFO REVIEW_BUILD_BUTTON = new BUTTON_INFO("Build Phase", .3f, .75f, .4f, 0.4f * 16f / 9f / 4f);
        //*/
        #endregion

        #region TUTORIAL 
        /*
        public const string TUTORIAL_TEXTURE1 = "TutorialScreen1";
        public const string TUTORIAL_TEXTURE2 = "TutorialScreen2";
        public const string TUTORIAL_TEXTURE3 = "TutorialScreen3";
        public static UIBOX_INFO TUTORIAL_TEXTURE_BOX = new UIBOX_INFO(0.05f, 0.05f * 16f / 9f, 0.9f, 1 - (0.15f * 16f / 9f), Color.White);
        public static BUTTON_INFO EXIT_TUTORIAL = new BUTTON_INFO("Menu", 0.4f, 0.85f, 0.2f, 0.1f);
        public static BUTTON_INFO NEXT_TUTORIAL = new BUTTON_INFO("Next", 0.6f, 0.85f, 0.2f, 0.1f);
        public static BUTTON_INFO PREVIOUS_TUTORIAL = new BUTTON_INFO("Previous", 0.2f, 0.85f, 0.2f, 0.1f);
        //*/
        #endregion
    }
}
