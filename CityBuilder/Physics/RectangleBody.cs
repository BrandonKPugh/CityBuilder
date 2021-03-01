using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CityBuilder
{
    class RectangleBody : CollisionBody
    {
        public Vector2 Size { get; set; }

        public RectangleBody(Vector2 Position, Vector2 Size)
        {
            this.Position = Position;
            Shape = ShapeType.Rectangle;
            this.Size = Size;
            id = NextId();
            bodies.Add(id, this);
        }

        public RectangleBody(Rectangle rect)
        {
            this.Position = new Vector2(rect.X,  rect.Y);
            Shape = ShapeType.Rectangle;
            this.Size = new Vector2(rect.Width, rect.Height);
            id = NextId();
            bodies.Add(id, this);
        }

        public override Rectangle Region()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}
