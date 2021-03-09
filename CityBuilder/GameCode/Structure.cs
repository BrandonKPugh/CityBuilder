using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityBuilder
{
    public class Structure : Object
    {
        public StructureData Data;

        public struct StructureData
        {
            public StructureData(int width, int height, int x1, int y1)
            {
                this.width = width;
                this.height = height;
                this.X1 = x1;
                this.Y1 = y1;
            }
            public int width;
            public int height;
            public int X1;
            public int Y1;
            public int X2 { get { return X1 + width - 1; } }
            public int Y2 { get { return Y1 = height - 1; } }
        }

        public Structure(Game1 game, CollisionBody collision, StructureData data) : base(game, collision)
        {
            this.Data = data;
            if(collision.Shape == CollisionBody.ShapeType.Rectangle)
            {
                RectangleBody rect = ((RectangleBody)this.Collision);
                rect.Size = new Vector2(collision.Region().Width * data.width, collision.Region().Height * data.height);
            }
        }

        public Structure(Game1 game, Grid buildGrid, StructureData data)
        {
            RectangleBody rect = new RectangleBody(buildGrid.TileToPixelRect(data.X1, data.Y1));
            this.Collision = rect;
            rect.Size = new Vector2(rect.Region().Width * data.width, rect.Region().Height * data.height);
            this.Data = data;

            Game = game;
            Collision = rect;
            Collision.Parent = this;

            Velocity = new Vector2(0f);
            Acceleration = new Vector2(0f);
        }

        public void RotateRight()
        {
            throw new NotImplementedException();
        }

        public void RotateLeft()
        {
            throw new NotImplementedException();
        }
    }
}
