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
