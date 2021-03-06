﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityBuilder.Interface
{
    public class BorderBox : UI_Component
    {
        private Texture2D _texture;
        private int _penWeight;
        public int PenWeight { get { if (_penWeight > Math.Min(Size.X/2, Size.Y/2)) return (int)(Math.Min(Size.X/2, Size.Y/2) + 1); else return _penWeight; } set { _penWeight = value; } }
        public Color Color { get; set; }
        public ControlConstants.BORDERBOX_INFO BorderBoxInfo { set { Position = new Vector2(value.X, value.Y); Size = new Vector2(value.Width, value.Height); Color = value.Color; PenWeight = value.Weight; } }
        public Rectangle Location { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); } set { this.Position = new Vector2(value.X, value.Y);  this.Size = new Vector2(value.Width, value.Height); } }

        public BorderBox(Texture2D texture)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Top
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, PenWeight), Color);
            // Left
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)PenWeight, (int)Size.Y), Color);
            // Right
            spriteBatch.Draw(_texture, new Rectangle((int)(Position.X + Size.X - PenWeight), (int)Position.Y, PenWeight, (int)Size.Y), Color);
            // Bottom
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)(Position.Y + Size.Y - PenWeight), (int)Size.X, PenWeight), Color);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void SetPosition(Rectangle dest, int padding)
        {
            int x = dest.X - padding;
            int y = dest.Y - padding;
            int w = dest.Width + padding * 2;
            int h = dest.Height + padding * 2;
            this.Position = new Vector2(x, y);
            this.Size = new Vector2(w, h);
        }
    }
}
