using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CityBuilder
{
    public class Sprite
    {
        private const int FRAME_RATE = 60;

        public int PixelWidth;
        public int PixelHeight;
        private int columns;
        private int rows;
        private List<List<int>> sheetIndex;
        private SpriteSheet spriteSheet;
        private SpriteEffects flip;
        public Color TextureColor = Color.White;
        private List<List<Rectangle>> sourceRects;
        //public TimeSpan timer;
        //public float AnimationSpeed;
        //public int Frame = 0;
        public float Depth = 0.5f;

        public Sprite(SpriteSheet sheet, int pixelWidth, int pixelHeight, int imagesX, int imagesY, List<List<int>> frames, SpriteEffects toFlip = SpriteEffects.None)
        {
            spriteSheet = sheet;
            PixelWidth = pixelWidth;
            PixelHeight = pixelHeight;
            columns = imagesX;
            rows = imagesY;
            sheetIndex = frames;
            flip = toFlip;
            //timer = new TimeSpan(0);
            //AnimationSpeed = 0.5f;
        }

        private Sprite(Sprite oldSprite)
        {
            spriteSheet = oldSprite.spriteSheet;
            PixelWidth = oldSprite.PixelWidth;
            PixelHeight = oldSprite.PixelHeight;
            columns = oldSprite.columns;
            rows = oldSprite.rows;
            sheetIndex = oldSprite.sheetIndex;
            flip = oldSprite.flip;
            this.LoadContent();
        }

        /// <summary>
        /// 0 <= Rotation < 2pi
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="destination"></param>
        /// <param name="rotation"></param>
        public void Draw(SpriteBatch spriteBatch, Rectangle destination, float rotation = 0.0f)
        {
            if (rotation == 0.0f)
            {
                List<Rectangle> sources = sourceRects[0];

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        Rectangle source = sources[y * columns + x];
                        Rectangle region = destination;
                        Rectangle dest = new Rectangle(region.X + ((region.Width / columns) * x), region.Y + ((region.Height / rows) * y), region.Width / columns, region.Height / rows);
                        spriteBatch.Draw(spriteSheet.sheetTexture, dest, source, TextureColor, rotation, new Vector2(0), flip, Depth);
                    }
                }
            }
            /*
             * This code was intended to be for structure rotation, but I've since decided not to
             * implement this functionality. This code is only partially complete.
             */
            else
            {
                List<Rectangle> sources = sourceRects[0];

                //float aDir = (destination.Width / columns);
                //float bDir = (destination.Height / rows);

                float width = destination.Width / columns;
                float height = destination.Height / rows;
                float diag = (float)Math.Sqrt(Math.Pow(width / 2f, 2f) + Math.Pow(height / 2f, 2f));

                float cos = (float)(Math.Cos(rotation) * width);
                float sin = (float)(Math.Sin(rotation) * height);

                float diagCos = (float)Math.Round(Math.Cos(7 * Math.PI / 4 - rotation) * diag, 1);
                float diagSin = (float)Math.Round(Math.Sin(7 * Math.PI / 4 - rotation) * diag, 1);

                Vector2 aVec = new Vector2(cos, sin);
                Vector2 bVec = new Vector2(sin, cos);

                for (int b = 0; b < rows; b++)
                {
                    for (int a = 0; a < columns; a++)
                    {
                        Rectangle source = sources[b * columns + a];
                        float translateX = (float)Math.Round((diagCos * -1) + (width / 2), 0);
                        float translateY = (float)Math.Round((diagSin) + (height / 2), 0);
                        if (translateX != 0 || translateY != 0)
                        {

                        }
                        Rectangle dest = new Rectangle((int)(destination.X + (aVec.X * a) + (bVec.X * b) + translateX + 0.5f), (int)(destination.Y + (aVec.Y * a) + (bVec.Y * b) + translateY + 0.5f), (int)width, (int)height);

                        spriteBatch.Draw(spriteSheet.sheetTexture, dest, source, TextureColor, rotation, new Vector2(0), flip, Depth);
                    }
                }
            }
            //*/
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, SpriteAnimationData animationData)
        {
            int frame = animationData.Frame;
            List<Rectangle> sources = sourceRects[frame];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Rectangle source = sources[y * columns + x];
                    Rectangle region = destination;
                    Rectangle dest = new Rectangle(region.X + ((region.Width / columns) * x), region.Y + ((region.Height / rows) * y), region.Width / columns, region.Height / rows);
                    spriteBatch.Draw(spriteSheet.sheetTexture, dest, source, TextureColor, 0f, new Vector2(0), flip, Depth);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            Color old = TextureColor;
            TextureColor = color;
            Draw(spriteBatch, rect);
            //TextureColor = color;
            TextureColor = old;
        }

        /*
        public void Draw(SpriteBatch spriteBatch, Rectangle rect, int frame)
        {
            List<Rectangle> sources = sourceRects[frame];
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Rectangle source = sources[y * columns + x];
                    Rectangle region = rect;
                    Rectangle dest = new Rectangle(region.X + ((region.Width / columns) * x), region.Y + ((region.Height / rows) * y), region.Width / columns, region.Height / rows);
                    spriteBatch.Draw(spriteSheet.sheetTexture, dest, source, TextureColor, 0f, new Vector2(0), flip, Depth);
                }
            }
        }
        */

        public void LoadContent()
        {

            // Currently Sprite.Draw() renders multiple textures at a time for larger sprites. 
            // This could be an issue in large-sprite or high-object-count games.
            // Resolve here be checking to see if a sprite is split into too many textures.
            // Basically each 'tile' within the spritesheet is broken up into a separate texture, and these textures are stored within sourceRects.
            // By making the 'source' rectangles larger (combining nearby rectangles) within this List, 
            // the sprite would only need to call SpriteBatch.Draw() once per object, rather than multiple draws.

            sourceRects = new List<List<Rectangle>>();
            int eachImageX = (PixelWidth / columns);
            int eachImageY = (PixelHeight / rows);
            for (int i = 0; i < sheetIndex.Count; i++)
            {
                sourceRects.Add(new List<Rectangle>());
                List<int> list = sheetIndex[i];
                for (int j = 0; j < list.Count; j++)
                {
                    int pos = list[j];
                    int x = pos % spriteSheet.Columns;
                    int y = pos / spriteSheet.Columns;
                    sourceRects[i].Add(new Rectangle(spriteSheet.Offset + x * (eachImageX + spriteSheet.Gutter), spriteSheet.Offset + y * (eachImageY + spriteSheet.Gutter), eachImageX, eachImageY));
                }
            }

            /*
            sourceRects = new List<List<Rectangle>>();
            int eachImageX = (PixelWidth / columns);
            int eachImageY = (PixelHeight / rows);
            for (int i = 0; i < sheetIndex.Count; i++)
            {
                sourceRects.Add(new List<Rectangle>());
                List<int> list = sheetIndex[i];
                for (int j = 0; j < list.Count; j++)
                {
                    int pos = list[j];
                    int x = pos % spriteSheet.Columns;
                    int y = pos / spriteSheet.Columns;
                    sourceRects[i].Add(new Rectangle(spriteSheet.Offset + x * (eachImageX + spriteSheet.Gutter), spriteSheet.Offset + y * (eachImageY + spriteSheet.Gutter), eachImageX, eachImageY));
                }
            }
            */
        }

        public Sprite Copy()
        {
            return new Sprite(this);
        }

        public int Frames
        {
            get
            {
                return sourceRects.Count;
            }
        }

        /*
        public void Update(GameTime gameTime)
        {
            // Could optionally account for gameTime delay here to potentially skip animation frames when stuttering
            timer = timer.Add(gameTime.ElapsedGameTime);
            if (timer.TotalMilliseconds > sourceRects.Count * MillisecondsPerFrame)
            {
                timer = new TimeSpan(0);
            }
        }

        private float MillisecondsPerFrame
        {
            get
            {
                return (int)((1000f / AnimationSpeed) / FRAME_RATE);
            }
            set
            {
                AnimationSpeed = (1000f / FRAME_RATE) / value;
            }
        }

        public void ResetAnimation()
        {
            timer = new TimeSpan(0);
        }
        */
    }
}