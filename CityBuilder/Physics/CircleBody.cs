using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace CityBuilder
{
    public class CircleBody : CollisionBody
    {
        public float Radius { get; set; }

        public CircleBody(Vector2 Position, float Radius)
        {
            this.Position = Position;
            Shape = ShapeType.Circle;
            this.Radius = Radius;
            ID = NextId();
            Bodies.Add(ID, this);
        }

        public override Rectangle Region()
        {
            return new Rectangle((int)(Position.X - Radius), (int)(Position.Y - Radius), (int)(Radius * 2), (int)(Radius * 2));
        }
    }
}
