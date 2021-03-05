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
            public StructureData(int width, int height)
            {
                this.width = width;
                this.height = height;
            }
            public int width;
            public int height;
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
