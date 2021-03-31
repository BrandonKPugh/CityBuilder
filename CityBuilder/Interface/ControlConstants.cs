using Microsoft.Xna.Framework;
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

        public struct CARD_INFO
        {
            // Button info is created with Text, and four floats (representing percentages of the screen's width/height. 0.1f = 10% from the edge and 0.5f = the center of the screen)
            // BUTTON_INFO(Text, X, Y, Width, Height)
            private int x, y, width, height;
            private string texture_name;
            public CARD_INFO(string textureName, float x_percent, float y_percent, float width_percent, float height_percent)
            {
                this.texture_name = textureName;
                this.x = (int)(x_percent * Config.GAME_WIDTH);
                this.y = (int)(y_percent * Config.GAME_HEIGHT);
                this.width = (int)(width_percent * Config.GAME_WIDTH);
                this.height = (int)(height_percent * Config.GAME_HEIGHT);
            }
            public CARD_INFO(string textureName, int x, int y, int width, int height)
            {
                this.texture_name = textureName;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
            public int X { get { return this.x; } }
            public int Y { get { return this.y; } }
            public int Width { get { return this.width; } }
            public int Height { get { return this.height; } }
            public string Texture_Name { get { return texture_name; } }
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

        public static Color STRUCTURE_CARD_PLACEABLE = Color.White;
        public static Color STRUCTURE_CARD_NOT_PLACEABLE = Color.Gray;
        
        public static Vector2 STRUCTURE_CARD_SIZE = new Vector2(326, 188);
        public static Vector2 STRUCTURE_CARD_POSITION = new Vector2(4, 4);
        public static Vector2 STRUCTURE_CARD_TITLE_SIZE = new Vector2(314, 48);
        public static Vector2 STRUCTURE_CARD_TITLE_POSITION = new Vector2(6, 6);
        public static string STRUCTURE_CARD_TITLE_FONT = "DebugFont";
        public static Color STRUCTURE_CARD_TITLE_COLOR = Color.Black;
        public static string STRUCTURE_CARD_RESOURCE_FONT = "DebugFont";
        public static string STRUCTURE_CARD_TEXTURE = "StructureCard";

        #endregion
    }
}
